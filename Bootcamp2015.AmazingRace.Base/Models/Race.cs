using System;
using System.Collections.Generic;

namespace Bootcamp2015.AmazingRace.Base.Models
{
    public class Race
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<Team> Teams { get; set; }
    }
}