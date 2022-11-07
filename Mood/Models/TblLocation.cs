using System;
using System.Collections.Generic;

namespace Mood.Models
{
    public partial class TblLocation
    {
        public TblLocation()
        {
            TblMoods = new HashSet<TblMood>();
        }

        public int LocationId { get; set; }
        public string LocationName { get; set; } = null!;
        public int DistanceXaxis { get; set; }
        public int DistanceYaxis { get; set; }

        public virtual ICollection<TblMood> TblMoods { get; set; }
    }
}
