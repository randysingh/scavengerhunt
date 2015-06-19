using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bootcamp2015.AmazingRace.Models
{
    public class RaceStatus
    {
        public string teamId { get; set; }
        public string name { get; set; }
        public int rank { get; set; }
        public int points { get; set; }
        public string nextClueId { get; set; }
    }
}
