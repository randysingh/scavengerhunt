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

        private ObservableCollection<Team> _teams = new ObservableCollection<Team>();

        private Team _currentTeam;

        public ICommand JoinTeam { get; set; }

        public string TeamName { get { return _currentTeam.Name; } }

        public List<Team> Leaderboard
        {
            get
            {
                return _teams.OrderBy(e => e.Points).ToList();
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
            _currentTeam = _settingsService.GetDeserializedValueOrDefault<Team>("TEAM");

            // Get race info (leaderboards)
            //Race race = _dataService.GetRaceAsync(currentTeam.);
        }

        private void GoToNextClue()
        {
            _navigationService.Navigate(typeof(CluePage));
        }
    }
}
