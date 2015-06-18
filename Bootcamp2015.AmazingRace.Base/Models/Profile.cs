using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp2015.AmazingRace.Base.Models
{
    public class Profile
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
