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

        [HttpGet("GetMoodFrequency")]
        /*[HttpGet("GetMoodFrequency/{UserId}")]*/
        public async Task<IActionResult> GetMoodFrequency(int UserId) {
            Models.TblMood.GetMoodFrequency(UserId);
            if (UserId < 1)
                return BadRequest();

            var product = await _db.TblMoods.ToListAsync();
            if (product == null)
                return NotFound();

            return Ok(product);
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

    }
}
