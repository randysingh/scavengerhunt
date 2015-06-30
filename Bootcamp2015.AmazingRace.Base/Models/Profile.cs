using System.Collections.Generic;

namespace Bootcamp2015.AmazingRace.Base.Models
{
    public class Profile
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public IEnumerable<Team> Teams { get; set; }
    }
}