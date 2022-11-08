using Microsoft.AspNetCore.Mvc;
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

        [TestCase(-1, 1, 1)]
        [TestCase(1, -1, 1)]
        [TestCase(1, 1, -1)]
        [TestCase(-1, -1, 1)]
        [TestCase(1, -1, -1)]
        [TestCase(-1, 1, -1)]
        [TestCase(1, 1, -1)]
        [TestCase(-1, 1, 1)]
        [TestCase(-1, -1, -1)]
        public async Task PostMood_Invalid_Input_Case(int UserId, int MoodId, int LocationId) {
            var moods = await _moodController.PostMood(UserId, MoodId, LocationId);

            string result = Newtonsoft.Json.JsonConvert.SerializeObject(moods);

            string expected = "{\"Value\":\"UserId, MoodId and LocationId have to be greater than 0.\",\"Formatters\":[],\"ContentTypes\":[],\"DeclaredType\":null,\"StatusCode\":400}";

            Assert.AreEqual(result, expected);
        }

        [Test]
        public async Task PostMood_Valid_Input_Case1() {
            const int UserId = 3;
            const int MoodId = 2;
            const int LocationId = 1;

            var moods = await _moodController.PostMood(UserId, MoodId, LocationId);

            string result = Newtonsoft.Json.JsonConvert.SerializeObject(moods);

            string expected = "{\"StatusCode\":200}";

            Assert.AreEqual(result, expected);
        }

        [Test]
        public async Task PostMood_Valid_Input_Case2() {
            const int UserId = 100000;
            const int MoodId = 2;
            const int LocationId = 1;

            string result = String.Empty;
            try {
                var moods = await _moodController.PostMood(UserId, MoodId, LocationId);
                result = Newtonsoft.Json.JsonConvert.SerializeObject(moods);
            } catch (Exception) {
                // Foreign key constraint DB error occured.
            }

            Assert.AreEqual(result, string.Empty);
        }

        [Test]
        public async Task PostMood_Valid_Input_Case3() {
            const int UserId = 1;
            const int MoodId = 100000;
            const int LocationId = 1;

            string result = String.Empty;
            try {
                var moods = await _moodController.PostMood(UserId, MoodId, LocationId);
                result = Newtonsoft.Json.JsonConvert.SerializeObject(moods);
            } catch (Exception) {
                // Foreign key constraint DB error occured.
            }

            Assert.AreEqual(result, string.Empty);
        }

        [Test]
        public async Task PostMood_Valid_Input_Case4() {
            const int UserId = 5;
            const int MoodId = 2;
            const int LocationId = 100000;

            string result = String.Empty;
            try {
                var moods = await _moodController.PostMood(UserId, MoodId, LocationId);
                result = Newtonsoft.Json.JsonConvert.SerializeObject(moods);
            } catch (Exception) {
                // Foreign key constraint DB error occured.
            }

            Assert.AreEqual(result, string.Empty);
        }

        [Test]
        public async Task GetClosestHappyLocation_Valid_Input_Case1() {
            const int UserID = 1;
            const int LocationID = 1;

            var moods = await _moodController.GetClosestHappyLocation(UserID, LocationID);

            string result = Newtonsoft.Json.JsonConvert.SerializeObject(moods);

            // Expected result = LocationName: C
            string expected = "{\"Value\":[{\"LocationID\":3,\"LocationName\":\"C\"}],\"Formatters\":[],\"ContentTypes\":[],\"DeclaredType\":null,\"StatusCode\":200}";

            Assert.AreEqual(result, expected);
        }

        [Test]
        public async Task GetClosestHappyLocation_Valid_Input_Case2() {
            const int UserID = 1;
            const int LocationID = 2;

            var moods = await _moodController.GetClosestHappyLocation(UserID, LocationID);

            string result = Newtonsoft.Json.JsonConvert.SerializeObject(moods);

            // Expected result = LocationName: D
            string expected = "{\"Value\":[{\"LocationID\":4,\"LocationName\":\"D\"}],\"Formatters\":[],\"ContentTypes\":[],\"DeclaredType\":null,\"StatusCode\":200}";

            Assert.AreEqual(result, expected);
        }

        [Test]
        public async Task GetClosestHappyLocation_Valid_Input_Case3() {
            const int UserID = 1;
            const int LocationID = 3;

            var moods = await _moodController.GetClosestHappyLocation(UserID, LocationID);

            string result = Newtonsoft.Json.JsonConvert.SerializeObject(moods);

            // Expected result = LocationName: A
            string expected = "{\"Value\":[{\"LocationID\":1,\"LocationName\":\"A\"}],\"Formatters\":[],\"ContentTypes\":[],\"DeclaredType\":null,\"StatusCode\":200}";

            Assert.AreEqual(result, expected);
        }

        [Test]
        public async Task GetClosestHappyLocation_Valid_Input_Case4() {
            const int UserID = 1;
            const int LocationID = 4;

            var moods = await _moodController.GetClosestHappyLocation(UserID, LocationID);

            string result = Newtonsoft.Json.JsonConvert.SerializeObject(moods);

            // Expected result = LocationName: E
            string expected = "{\"Value\":[{\"LocationID\":5,\"LocationName\":\"E\"}],\"Formatters\":[],\"ContentTypes\":[],\"DeclaredType\":null,\"StatusCode\":200}";

            Assert.AreEqual(result, expected);
        }

        [Test]
        public async Task GetClosestHappyLocation_Valid_Input_Case5() {
            const int UserID = 1;
            const int LocationID = 5;

            var moods = await _moodController.GetClosestHappyLocation(UserID, LocationID);

            string result = Newtonsoft.Json.JsonConvert.SerializeObject(moods);

            // Expected result = LocationName: A
            string expected = "{\"Value\":[{\"LocationID\":1,\"LocationName\":\"A\"}],\"Formatters\":[],\"ContentTypes\":[],\"DeclaredType\":null,\"StatusCode\":200}";

            Assert.AreEqual(result, expected);
        }

        [Test]
        public async Task GetMoodFrequency_Valid_Input_Case1() {
            const int UserID = 1;

            var moods = await _moodController.GetMoodFrequency(UserID);

            string result = Newtonsoft.Json.JsonConvert.SerializeObject(moods);

            string expected = "{\"Value\":[{\"LocationID\":1,\"LocationName\":\"A\",\"Mood\":[{\"MoodID\":1,\"MoodName\":\"Happy\",\"Count\":5},{\"MoodID\":2,\"MoodName\":\"Sad\",\"Count\":3},{\"MoodID\":3,\"MoodName\":\"Neutral\",\"Count\":1}]},{\"LocationID\":2,\"LocationName\":\"B\",\"Mood\":[{\"MoodID\":1,\"MoodName\":\"Happy\",\"Count\":3},{\"MoodID\":2,\"MoodName\":\"Sad\",\"Count\":4},{\"MoodID\":3,\"MoodName\":\"Neutral\",\"Count\":2}]},{\"LocationID\":3,\"LocationName\":\"C\",\"Mood\":[{\"MoodID\":1,\"MoodName\":\"Happy\",\"Count\":7},{\"MoodID\":2,\"MoodName\":\"Sad\",\"Count\":3},{\"MoodID\":3,\"MoodName\":\"Neutral\",\"Count\":0}]},{\"LocationID\":4,\"LocationName\":\"D\",\"Mood\":[{\"MoodID\":1,\"MoodName\":\"Happy\",\"Count\":2},{\"MoodID\":2,\"MoodName\":\"Sad\",\"Count\":1},{\"MoodID\":3,\"MoodName\":\"Neutral\",\"Count\":1}]},{\"LocationID\":5,\"LocationName\":\"E\",\"Mood\":[{\"MoodID\":1,\"MoodName\":\"Happy\",\"Count\":4},{\"MoodID\":2,\"MoodName\":\"Sad\",\"Count\":1},{\"MoodID\":3,\"MoodName\":\"Neutral\",\"Count\":0}]}],\"Formatters\":[],\"ContentTypes\":[],\"DeclaredType\":null,\"StatusCode\":200}";
            
            Assert.AreEqual(result, expected);
        }

        [Test]
        public async Task GetMoodFrequency_Valid_Input_Case2() {
            const int UserID = 5;

            var moods = await _moodController.GetMoodFrequency(UserID);

            string result = Newtonsoft.Json.JsonConvert.SerializeObject(moods);

            string expected = "{\"Value\":[{\"LocationID\":1,\"LocationName\":\"A\",\"Mood\":[{\"MoodID\":1,\"MoodName\":\"Happy\",\"Count\":0},{\"MoodID\":2,\"MoodName\":\"Sad\",\"Count\":0},{\"MoodID\":3,\"MoodName\":\"Neutral\",\"Count\":0}]},{\"LocationID\":2,\"LocationName\":\"B\",\"Mood\":[{\"MoodID\":1,\"MoodName\":\"Happy\",\"Count\":0},{\"MoodID\":2,\"MoodName\":\"Sad\",\"Count\":0},{\"MoodID\":3,\"MoodName\":\"Neutral\",\"Count\":0}]},{\"LocationID\":3,\"LocationName\":\"C\",\"Mood\":[{\"MoodID\":1,\"MoodName\":\"Happy\",\"Count\":0},{\"MoodID\":2,\"MoodName\":\"Sad\",\"Count\":0},{\"MoodID\":3,\"MoodName\":\"Neutral\",\"Count\":0}]},{\"LocationID\":4,\"LocationName\":\"D\",\"Mood\":[{\"MoodID\":1,\"MoodName\":\"Happy\",\"Count\":0},{\"MoodID\":2,\"MoodName\":\"Sad\",\"Count\":0},{\"MoodID\":3,\"MoodName\":\"Neutral\",\"Count\":0}]},{\"LocationID\":5,\"LocationName\":\"E\",\"Mood\":[{\"MoodID\":1,\"MoodName\":\"Happy\",\"Count\":0},{\"MoodID\":2,\"MoodName\":\"Sad\",\"Count\":0},{\"MoodID\":3,\"MoodName\":\"Neutral\",\"Count\":0}]}],\"Formatters\":[],\"ContentTypes\":[],\"DeclaredType\":null,\"StatusCode\":200}";

            Assert.AreEqual(result, expected);
        }

        [Test]
        public async Task GetMoodFrequency_Invalid_Input_Case() {
            const int UserID = -1;

            var moods = await _moodController.GetMoodFrequency(UserID);

            string result = Newtonsoft.Json.JsonConvert.SerializeObject(moods);

            string expected = "{\"Value\":\"UserId has to be greater than 0.\",\"Formatters\":[],\"ContentTypes\":[],\"DeclaredType\":null,\"StatusCode\":400}";

            Assert.AreEqual(result, expected);
        }
    }
}