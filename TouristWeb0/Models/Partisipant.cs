using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TouristWeb0
{
    public partial class Partisipant
    {
        public Partisipant()
        {
            RankPartisipants = new HashSet<RankPartisipant>();
            Results = new HashSet<Result>();
            TeamPartisipants = new HashSet<TeamPartisipant>();
        }

        public int ParticipantId { get; set; }
        [Display(Name = "ПІБ")]
        public string Name { get; set; }
        [Display(Name = "Дата народження")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Стать")]
        public int IdSex { get; set; }
        [Display(Name = "Номер телефону")]
        public int? PhoneNumber { get; set; }
        [Display(Name = "Роль")]
        public int IdRole { get; set; }
        [Display(Name = "Страховка")]
        public byte[] FileInsurance { get; set; }
        [Display(Name = "Роль")]

        public virtual Role IdRoleNavigation { get; set; }
        [Display(Name = "Стать")]
        public virtual Sex IdSexNavigation { get; set; }
        [Display(Name = "Розряд")]
        public virtual ICollection<RankPartisipant> RankPartisipants { get; set; }
        [Display(Name = "Резльтат")]
        public virtual ICollection<Result> Results { get; set; }
        [Display(Name = "Команда")]
        public virtual ICollection<TeamPartisipant> TeamPartisipants { get; set; }
    }
}
