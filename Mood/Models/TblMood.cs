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

        public static bool GetMoodFrequency(int UserId) {
            bool result = false;
            return result;
        }
    }
}
