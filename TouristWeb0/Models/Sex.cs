using System;
using System.Collections.Generic;

#nullable disable

namespace TouristWeb0
{
    public partial class Sex
    {
        public Sex()
        {
            Partisipants = new HashSet<Partisipant>();
        }

        public int SexId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Partisipant> Partisipants { get; set; }
    }
}
