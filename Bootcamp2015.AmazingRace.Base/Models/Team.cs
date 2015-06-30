using System;

namespace Bootcamp2015.AmazingRace.Base.Models
{
    public class Team
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Rank { get; set; }
        public int Points { get; set; }
        public DateTime LastModified { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}