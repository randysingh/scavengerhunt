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

            [JsonProperty("longitude")]
            public string longitude { get; set; }

            [JsonProperty("latitude")]
            public string latitude { get; set; }

            [JsonProperty("id")]
            public string id { get; set; }

            [JsonProperty("range")]
            public string range { get; set; }
        }
    
}
