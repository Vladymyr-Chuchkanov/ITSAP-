using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TouristWeb0
{
    public partial class TeamPartisipant
    {
        public int TeamPartisipantId { get; set; }
        [Display(Name = "Учасник")]
        public int PartisipantId { get; set; }
        [Display(Name = "Команда")]
        public int TeamId { get; set; }
        [Display(Name = "Брав участь")]
        public byte[] Participated { get; set; }
        [Display(Name = "Учасник")]

        public virtual Partisipant Partisipant { get; set; }
        [Display(Name = "Команда")]
        public virtual Team Team { get; set; }
    }
}
