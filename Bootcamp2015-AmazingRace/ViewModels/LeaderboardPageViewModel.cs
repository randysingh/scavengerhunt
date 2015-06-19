using Bootcamp2015.AmazingRace.Base;
using Bootcamp2015.AmazingRace.Base.Models;
using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;
using Bootcamp2015.AmazingRace.Views;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bootcamp2015.AmazingRace.ViewModels
{
    public class LeaderboardPageViewModel : Screen
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;
        private readonly ISettingsService _settingsService;

        public ObservableCollection<Team> Leaderboard { get; set; }

        // For Caliburn's passing in object
        public Team CurrentTeam { get; set; }
        public Team Parameter
        {
            set
            {
                CurrentTeam = value;
            }
        }

        public ICommand JoinTeam { get; set; }

        public string TeamName
        {
            get
            {
                return "TEAM NAME";
                //return _currentTeam.Name; 
            }
        }

        public LeaderboardPageViewModel(INavigationService navigationService,
            IDataService dataService, ISettingsService settingsService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
            _settingsService = settingsService;

            JoinTeam = new DelegateCommand(() => GoToNextClue());

            // Get current team
            //_currentTeam = _settingsService.GetDeserializedValueOrDefault<Team>("TEAM");

            // Get race info (leaderboards)
            Task.Run(() => GetLeaderboards()).Wait();
        }

        private async void GetLeaderboards()
        {
            Race race = await _dataService.GetRaceAsync("test_race");
            Leaderboard = new ObservableCollection<Team>(race.Teams.OrderBy(x => x.Rank));
        }

        private void GoToNextClue()
        {
            // TODO: hide button if no clues

            _navigationService.Navigate(typeof(CluePage));
        }
    }
}
