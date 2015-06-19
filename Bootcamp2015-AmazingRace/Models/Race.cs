using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bootcamp2015.AmazingRace.Models
{
    public class Race
    {
        public string id { get; set; }
        public string name { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public IEnumerable<Team> teams { get; set; }
    }
}
