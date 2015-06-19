using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bootcamp2015.AmazingRace.Models
{
    public class Profile
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public IEnumerable<Team> teams { get; set; }
    }

}
