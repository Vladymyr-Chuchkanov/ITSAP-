using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TouristWeb0
{
    public partial class Team
    {
        public Team()
        {
            CompetitionTeams = new HashSet<CompetitionTeam>();
            TeamPartisipants = new HashSet<TeamPartisipant>();
        }

        public int TeamId { get; set; }
        [Display(Name = "Назва")]
        public string Name { get; set; }
        [Display(Name = "Заявка")]
        public byte[] FileDocument { get; set; }
        [Display(Name = "Коментар")]
        public string Comment { get; set; }
        [Display(Name = "Змагання")]

        public virtual ICollection<CompetitionTeam> CompetitionTeams { get; set; }
        [Display(Name = "Учасник")]
        public virtual ICollection<TeamPartisipant> TeamPartisipants { get; set; }
    }
}
