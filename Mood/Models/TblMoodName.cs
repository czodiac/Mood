using System;
using System.Collections.Generic;

namespace Mood.Models
{
    public partial class TblMoodName
    {
        public TblMoodName()
        {
            TblMoods = new HashSet<TblMood>();
        }

        public int MoodId { get; set; }
        public string MoodName { get; set; } = null!;
        public int Weight { get; set; }

        public virtual ICollection<TblMood> TblMoods { get; set; }
    }
}
