using System;
using System.Collections.Generic;

#nullable disable

namespace TouristWeb0
{
    public partial class RankPartisipant
    {
        public int RankPartisipantId { get; set; }
        public int RankId { get; set; }
        public int PartisipantId { get; set; }
        public DateTime DateOfAchievement { get; set; }

        public virtual Partisipant Partisipant { get; set; }
        public virtual Rank Rank { get; set; }
    }
}
