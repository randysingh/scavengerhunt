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

        private ObservableCollection<Team> _teams = new ObservableCollection<Team>();

        public ICommand JoinTeam { get; set; }

        public List<Team> Leaderboard
        {
            get
            {
                return _teams.OrderBy(e => e.Points).ToList();
            }
        }

        public LeaderboardPageViewModel(INavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;

            JoinTeam = new DelegateCommand(() => GoToNextClue());
        }

        private void GoToNextClue()
        {
            _navigationService.Navigate(typeof(CluePage));
        }
    }
}
