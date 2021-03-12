using System;
using System.Collections.Generic;

#nullable disable

namespace TouristWeb0
{
    public partial class Role
    {
        public Role()
        {
            Partisipants = new HashSet<Partisipant>();
        }

        public int RolesId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Partisipant> Partisipants { get; set; }
    }
}
