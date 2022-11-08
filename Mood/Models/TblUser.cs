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
        public string? Password { get; set; }
        public string? EmailAddress { get; set; }
        public string? Role { get; set; }
        public string? Surname { get; set; }
        public string? GivenName { get; set; }

        public virtual ICollection<TblMood> TblMoods { get; set; }
    }
}
