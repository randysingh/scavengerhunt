using Bootcamp2015.AmazingRace.Base;
using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;
using Bootcamp2015.AmazingRace.Views;
using Caliburn.Micro;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bootcamp2015.AmazingRace.ViewModels
{
    public class JoinTeamPageViewModel : Screen
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;
        
        public string TeamCode { get; set; }

        public ICommand JoinTeam { get; set; }

        public JoinTeamPageViewModel(INavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;

            JoinTeam = new DelegateCommand(() => JoinTeamAction());
        }

        private async void JoinTeamAction()
        {
            var result = await _dataService.JoinTeamAsync(TeamCode);

            _navigationService.Navigate(typeof(LeaderboardPage));
        }
    }
}
