using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp2015.AmazingRace.Models
{
   
        public class Clue
        {
            [JsonProperty("description")]
            public string description { get; set; }
            [JsonProperty("location")]
            public string latitude { get; set; }
            public string longitude { get; set; }
            public int points { get; set; }
            public int range { get; set; }

            [JsonProperty("id")]
            public string id { get; set; }

        }
    
}
