using System;
using System.Collections.Generic;

#nullable disable

namespace TouristWeb0
{
    public partial class Complexity
    {
        public Complexity()
        {
            Competitions = new HashSet<Competition>();
        }

        public int ComplexityId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Competition> Competitions { get; set; }
    }
}
