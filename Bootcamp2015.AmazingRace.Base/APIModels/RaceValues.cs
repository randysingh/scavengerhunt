using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp2015.AmazingRace.Base.APIModels
{
    public class RaceValues
    {
        public IEnumerable<Race> races;
        public class Race
        {
            public string id;
            public string name;
        }
    }
}
