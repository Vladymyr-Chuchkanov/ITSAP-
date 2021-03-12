using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TouristWeb0
{
    public partial class ObstacleCompetition
    {
        public ObstacleCompetition()
        {
            Results = new HashSet<Result>();
        }

        public int ObstacleCompetitionId { get; set; }
        [Display(Name="Етап")]
        public int ObstacleId { get; set; }
        [Display(Name = "Змагання")]
        public int CompetitionId { get; set; }
        [Display(Name = "Порядок проходження")]
        public int? ObstaclePosition { get; set; }
        [Display(Name = "Змагання")]
        public virtual Competition Competition { get; set; }
        [Display(Name = "Етап")]
        public virtual Obstacle Obstacle { get; set; }
        [Display(Name = "Результат")]
        public virtual ICollection<Result> Results { get; set; }
    }
}
