using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TouristWeb0
{
    public partial class CompetitionTeam
    {
        public int CompetitionTeamId { get; set; }
        [Display(Name = "Змагання")]
        public int CompetitionId { get; set; }
        [Display(Name = "Команда")]
        public int TeamId { get; set; }
        [Display(Name = "Статус заявки")]
        public int? AdmittedId { get; set; }
        [Display(Name = "Результат")]
        public TimeSpan? ResultTime { get; set; }
        [Display(Name = "Штрафи")]
        public int? Penalty { get; set; }
        [Display(Name = "Місце")]
        public int? Position { get; set; }
        [Display(Name = "Статус заявки")]

        public virtual Admition Admitted { get; set; }
        [Display(Name = "Змагання")]
        public virtual Competition Competition { get; set; }
        [Display(Name = "Команда")]
        public virtual Team Team { get; set; }
    }
}
