using System;
using System.Collections.Generic;

namespace Mood.Models
{
    public partial class TblUser
    {
        public TblUser()
        {
            TblMoods = new HashSet<TblMood>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;

        public virtual ICollection<TblMood> TblMoods { get; set; }
    }
}
