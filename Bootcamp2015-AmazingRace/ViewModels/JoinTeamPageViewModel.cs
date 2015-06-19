using Bootcamp2015.AmazingRace.Base;
using Bootcamp2015.AmazingRace.Base.Models;
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
using Windows.ApplicationModel.Resources;

namespace Bootcamp2015.AmazingRace.ViewModels
{
    public class JoinTeamPageViewModel : Screen
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;
        private readonly ISettingsService _settingsService;

        public string TeamCode { get; set; }

        public ICommand JoinTeam { get; set; }

        public JoinTeamPageViewModel(INavigationService navigationService, IDataService dataService, ISettingsService settingsService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
            _settingsService = settingsService;

            JoinTeam = new DelegateCommand(() => JoinTeamAction());

            LoadProfileAsync();
        }

        /// <summary>
        /// Sets the profile from the service so we can display team name later
        /// </summary>
        private async void LoadProfileAsync()
        {
            Profile profile = await _dataService.GetProfileAsync();
            _settingsService.SetSerializedValue<Profile>("profile", profile);
        }

        private async void JoinTeamAction()
        {
            string message;
            try
            {
                var result = await _dataService.JoinTeamAsync(TeamCode);
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                message = ex.Message;
            }

            _navigationService.Navigate(typeof(LeaderboardPage));
        }
    }
}
