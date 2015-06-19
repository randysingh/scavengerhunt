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
            public string Description { get; set; }
            [JsonProperty("location")]
            public string Location { get; set; }

            [JsonProperty("id")]
            public string ClueId { get; set; }
        }
    
}
