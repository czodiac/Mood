using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mood.Models;
using System.Collections.Generic;
using System.Security.Claims;
using static Mood.Models.TblMood;

namespace Mood.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MoodController : ControllerBase {
        private readonly MoodDBContext _db;

        public MoodController(MoodDBContext context) {
            _db = context;
        }

        [HttpGet("HeartBeat")]
        public async Task<IActionResult> HeartBeat() {
            return Ok("Service running");
        }

        [HttpPost("PostMood")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PostMood(int UserId, int MoodId, int LocationId) {
            // Assumptions:
            // - User can visit the same place multiple times.
            // - User can visit the same place but the mood may differ.

            if (UserId < 1 || MoodId < 1 || LocationId < 1)
                return BadRequest("UserId, MoodId and LocationId have to be greater than 0.");

            try {
                TblMood mood = new TblMood();
                mood.UserId = UserId;
                mood.MoodId = MoodId;
                mood.LocationId = LocationId;
                _db.Add(mood);
                await _db.SaveChangesAsync();
            } catch (Exception e) {
                return BadRequest(e);
            }
            return Ok();
        }

        [HttpGet("GetMoodFrequency")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetMoodFrequency(int UserId) {
            // Assumptions: Return mood frequency(count) for all locations. If this user has never visited the location, the count will be 0.

            if (UserId < 1)
                return BadRequest("UserId has to be greater than 0.");

            var moods = await _db.TblMoods.ToListAsync(); // Get all mood records.
            var moodsForUserId = moods.Where(x => x.UserId == UserId); // Filter mood records by UserId
            var moodNames = await _db.TblMoodNames.ToListAsync(); // Get all mood names/weight.
            var locations = await _db.TblLocations.ToListAsync(); // Get all locations.

            Dictionary<int, int> happyScoreForEachLoc = new Dictionary<int, int>();
            List<LocationMood> result = new List<LocationMood>();
            foreach (TblLocation l in locations) {
                LocationMood lm = new LocationMood();
                lm.LocationID = l.LocationId;
                lm.LocationName = l.LocationName;

                List<MoodFrequency> mf = new List<MoodFrequency>();
                foreach (TblMoodName m in moodNames) {
                    mf.Add(new MoodFrequency(m.MoodId, m.MoodName, 0));
                }

                var moodsForThisLoc = moodsForUserId.Where(x => x.LocationId == l.LocationId);
                foreach (TblMood mood in moodsForThisLoc) {
                    //mf.Find()   
                    MoodFrequency tempMF = mf.Find(x => x.MoodID == mood.MoodId);
                    if (tempMF != null) 
                        tempMF.Count += 1;
                }
                lm.Mood = mf;
                result.Add(lm);
            }

            if (result.Count() == 0)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("GetClosestHappyLocation")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetClosestHappyLocation(int UserId, int LocationId) {
            // Assumptions:
            // - If distance is the same, return the location with higher happy score.
            // - In this 2 dimensional space where X and Y axis exist, one can only move left, right, top or bottom. He can't move diagonally.

            if (UserId < 1 || LocationId < 1)
                return BadRequest("UserId and LocationId have to be greater than 0.");

            var moods = await _db.TblMoods.ToListAsync(); // Get all mood records.
            var moodsForUserId = moods.Where(x => x.UserId == UserId); // Filter mood records by UserId
            var moodNames = await _db.TblMoodNames.ToListAsync(); // Get all mood names/weight.
            var locations = await _db.TblLocations.ToListAsync(); // Get all locations.

            var location = locations.Where(x => x.LocationId == LocationId).FirstOrDefault();
            if (location == null ) {
                return NotFound(string.Format("LocationId {0} was not found in DB.", LocationId));
            }
            TblLocation currentLocation = (location != null) ? (TblLocation)location : new TblLocation();
            int currentLocationX = currentLocation.DistanceXaxis;
            int currentLocationY = currentLocation.DistanceYaxis;

            
            Dictionary<int, int> distanceFromCurrentLoc = new Dictionary<int, int>();
            Dictionary<int, int> happyScoreForEachLoc = new Dictionary<int, int>();
            foreach (TblLocation l in locations) {
                if (l.LocationId != LocationId) {
                    // For every location excluding LocationId, find distance from LocationId.
                    int distance = Math.Abs(currentLocationX - l.DistanceXaxis) + Math.Abs(currentLocationY - l.DistanceYaxis);
                    distanceFromCurrentLoc.Add(l.LocationId, distance); // Distance from LocationId to this location.

                    // For each location, find happy score
                    int happyScore = 0;
                    var moodsForThisLoc = moodsForUserId.Where(x => x.LocationId == l.LocationId);
                    foreach (TblMood mood in moodsForThisLoc) {
                        // Calculate total mood score for this location.
                        int weight = moodNames.Where(x => x.MoodId == mood.MoodId).FirstOrDefault().Weight;
                        happyScore += weight;
                    }
                    happyScoreForEachLoc.Add(l.LocationId, happyScore);
                }
            }

            // Get those locations where happy score is greater than 0.
            Dictionary<int, int> positiveHappyScoreLocations = happyScoreForEachLoc.Where(score => score.Value > 0).ToDictionary(pair => pair.Key, pair => pair.Value);
            // Find user's happy score for each location.
            int tempClosestDistnce = int.MaxValue;
            int tempMaxHappyScore = 0;
            int resultLocationId = 0;
            foreach (var loc in positiveHappyScoreLocations) {
                // For each happy score location, find its distance.
                int distance = distanceFromCurrentLoc[loc.Key];
                if (distance < tempClosestDistnce) {
                    tempClosestDistnce = distance;
                    resultLocationId = loc.Key;
                    tempMaxHappyScore = loc.Value;
                } else if (distance == tempClosestDistnce) {
                    // This location's distance is the same as the previous shortest distance location. Update location only if this location's happy score is higher.
                    if (tempMaxHappyScore < loc.Value) {
                        resultLocationId = loc.Key;
                        tempMaxHappyScore = loc.Value;
                    }
                }
            }

            var result = from l in locations
                           where l.LocationId == resultLocationId
                           select new { LocationID = l.LocationId, LocationName = l.LocationName };
            if (result.Count() == 0)
                return NotFound();

            return Ok(result);
        }

    }
}
