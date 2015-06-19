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
            team1.Points = 150;
            teams.Add(team1);
            Team team2 = new Team();
            team2.Name = "Awesome Team";
            team2.Points = 100;
            teams.Add(team2);
            Team team3 = new Team();
            team3.Name = "Cool Team";
            team3.Points = 200;
            teams.Add(team3);
            Team team4 = new Team();
            team4.Name = "Awesome Team";
            team4.Points = 100;
            teams.Add(team4);
            Team team5 = new Team();
            team5.Name = "Cool Team";
            team5.Points = 200;
            teams.Add(team5);
            Team team6 = new Team();
            team6.Name = "Cool Team";
            team6.Points = 200;
            teams.Add(team6);
            Team team7 = new Team();
            team7.Name = "Cool Team";
            team7.Points = 200;
            teams.Add(team7);
            _race = new Race();
            _race.Id = "sample string 1";
            _race.Name = "Test Race";
            _race.Teams = teams;
        }
        
    }
}
