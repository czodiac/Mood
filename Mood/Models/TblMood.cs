using System;
using System.Collections.Generic;

namespace Mood.Models
{
    public partial class TblMood
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MoodId { get; set; }
        public int LocationId { get; set; }

        public virtual TblLocation Location { get; set; } = null!;
        public virtual TblMoodName Mood { get; set; } = null!;
        public virtual TblUser User { get; set; } = null!;

        public class LocationMood {
            public int LocationID { get; set; }
            public string LocationName { get; set; }
            public List<MoodFrequency> Mood { get; set; }
        }
        public class MoodFrequency {
            public int MoodID { get; set; }
            public string MoodName { get; set; }
            public int Count { get; set; }

            public MoodFrequency(int MoodID, string MoodName, int Count) {
                this.MoodID = MoodID;
                this.MoodName = MoodName;
                this.Count = Count;
            }
        }
    }
}
