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
    public class LeaderboardPageViewModel : Screen, INotifyPropertyChangedEx
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;
        private readonly ISettingsService _settingsService;

        private ObservableCollection<Team> _leaderboard = new ObservableCollection<Team>();
        public ObservableCollection<Team> Leaderboard {
            get { return _leaderboard; }
            set
            {
                _leaderboard = value;
                NotifyOfPropertyChange(() => Leaderboard);
            }
        }

        public Team CurrentTeam { get; set; }

        public ICommand JoinTeam { get; set; }

        public string TeamName
        {
            get { return CurrentTeam.Name; }
        }

        public LeaderboardPageViewModel(INavigationService navigationService,
            IDataService dataService, ISettingsService settingsService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
            _settingsService = settingsService;

            JoinTeam = new DelegateCommand(() => GoToNextClue());

            // Get current team
            CurrentTeam = _settingsService.GetDeserializedValueOrDefault<Team>("TEAM");

            // Get race info (leaderboards)
            //GetLeaderboards();
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
