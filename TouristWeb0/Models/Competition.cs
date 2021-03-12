using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TouristWeb0
{
    public partial class Competition
    {
        public Competition()
        {
            CompetitionTeams = new HashSet<CompetitionTeam>();
            ObstacleCompetitions = new HashSet<ObstacleCompetition>();
        }

        public int CompetitionId { get; set; }
        [Display(Name="Назва")]
        public string Name { get; set; }
        [Display(Name = "Місце Проведення")]
        public string Place { get; set; }
        [Display(Name = "Стартовий внесок")]
        public decimal StartTax { get; set; }
        [Display(Name = "Час початку")]
        public DateTime StartTime { get; set; }
        [Display(Name = "Клас дистанції")]
        public int IdComplexity { get; set; }
        [Display(Name = "Тип дистанції")]
        public int IdType { get; set; }
        [Display(Name = "Клас дистанції")]

        public virtual Complexity IdComplexityNavigation { get; set; }
        [Display(Name = "Тип дистанції")]
        public virtual Type IdTypeNavigation { get; set; }
        public virtual ICollection<CompetitionTeam> CompetitionTeams { get; set; }
        public virtual ICollection<ObstacleCompetition> ObstacleCompetitions { get; set; }
    }
}
