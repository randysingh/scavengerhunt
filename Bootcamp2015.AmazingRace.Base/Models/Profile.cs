﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp2015.AmazingRace.Base.Models
{
    public class Profile
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "displayName")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "teams")]
        public IEnumerable<Team> Teams { get; set; }
    }
}
