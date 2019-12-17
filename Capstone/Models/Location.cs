using System.Collections.Generic;

namespace Capstone.Models
{
    public class Location
    {
        public Location()
        {

        }
        public int LocationId { get; set; }
        public int Zip { get; set; }
        public string City { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
