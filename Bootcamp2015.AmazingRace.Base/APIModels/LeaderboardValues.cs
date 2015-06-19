using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp2015.AmazingRace.Base.APIModels
{
    public class LeaderboardValues
    {
        public string id;
        public string name;
        public IEnumerable<Team> teams;

        public class Team
        {
            public string id;
            public string name;
            public string imageUri;
            public string rank;
            public string points;
            public string latitute;
            public string longitude;
        }
    }
}
