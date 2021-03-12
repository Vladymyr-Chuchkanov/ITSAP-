using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TouristWeb0
{
    public partial class Rank
    {
        public Rank()
        {
            RankPartisipants = new HashSet<RankPartisipant>();
        }

        public int RankId { get; set; }
        [Display(Name = "Розряд")]
        public string Name { get; set; }

        public virtual ICollection<RankPartisipant> RankPartisipants { get; set; }
    }
}
