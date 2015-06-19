using Caliburn.Micro;
using Bootcamp2015.AmazingRace.Base.Models;
using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;
using System.Collections.Generic;

namespace Bootcamp2015.AmazingRace.ViewModels
{
    public class LeaderboardPageViewModel : Screen
    {
        private Race _race;

        public Race Race
        {
            get { return _race; }
            set 
            { 
                _race = value;
                NotifyOfPropertyChange<Race>(() => Race);
            }
        }

        public LeaderboardPageViewModel()
        {
            CreateTestRace();
        }

        private void CreateTestRace()
        {
            HashSet<Team> teams = new HashSet<Team>();
            Team team1 = new Team();
            team1.Name = "Shutterbugs";
            teams.Add(team1);
            Team team2 = new Team();
            team2.Name = "Awesome Team";
            teams.Add(team2);
            Team team3 = new Team();
            team3.Name = "Cool Team";
            teams.Add(team3);

            _race = new Race();
            _race.Id = "sample string 1";
            _race.Name = "Test Race";
            _race.Teams = teams;
        }
        
    }
}
