using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bootcamp2015.AmazingRace.Models
{
    public class Team
    {
        public string id { get; set; }
        public string name { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }
        public int rank { get; set; }
        public int points { get; set; }
    }
}
