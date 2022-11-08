using Microsoft.EntityFrameworkCore;
using Mood.Controllers;
using Mood.Models;
using static Mood.Models.TblMood;

namespace MoodNunitTest {
    public class Tests {

        private MoodDBContext _context;
        private MoodController _moodController;

        [SetUp]
        public void Setup() {
            // Create DB context
            var dbContextOptions = new DbContextOptionsBuilder<MoodDBContext>().UseSqlServer("Server=.;Database=MoodDB;Integrated Security=True;");
            _context = new MoodDBContext(dbContextOptions.Options);
            _moodController = new MoodController(_context);
        }

        [Test]
        public async Task Get_All_Moods() {
            var moods = await _moodController.GetMoodFrequency(2);

            LocationMood m = new LocationMood();

            Assert.Pass();
        }
        
    }
}