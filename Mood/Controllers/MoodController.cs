using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mood.Models;

namespace Mood.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MoodController : ControllerBase {
        private readonly MoodDBContext _db;

        public MoodController(MoodDBContext context) {
            _db = context;
        }

        [HttpPost("PostMood")]
        /*[AutoValidateAntiforgeryToken] // Prevent CSRF*/
        public async Task<IActionResult> PostMood(int UserId, int MoodId, int LocationId) {
            if (UserId < 1 || MoodId < 1 || LocationId < 1)
                return BadRequest();

            TblMood mood = new TblMood();
            mood.UserId = UserId;
            mood.MoodId = MoodId;
            mood.LocationId = LocationId;
            _db.Add(mood);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("GetMoodFrequency")]
        public async Task<IActionResult> GetMoodFrequency(int UserId) {
            if (UserId < 1)
                return BadRequest();

            var moods = await _db.TblMoods.ToListAsync();
            var filtered = moods.Where(x => x.UserId == UserId).GroupBy(x => x.LocationId);
            if (filtered == null || filtered.Count() < 1)
                return NotFound();

            return Ok(filtered);
        }

        [HttpGet("GetClosestHappyLocation")]
        public async Task<IActionResult> GetClosestHappyLocation(int UserId, int LocationId) {
            if (UserId < 1 || LocationId < 1)
                return BadRequest();

            // Get all mood data
            var moods = await _db.TblMoods.ToListAsync();
            var moodsForUserId = moods.Where(x => x.UserId == UserId);

            // Get all mood names/weight
            var moodNames = await _db.TblMoodNames.ToListAsync();

            // Get all locations
            var locations = await _db.TblLocations.ToListAsync();
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
