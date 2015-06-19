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
    public class MainPageViewModel : Screen
    {
        private readonly INavigationService _navigationService;

        private ICommand GoogleLogin { get; set; }
        private ICommand FacebookLogin { get; set; }

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            GoogleLogin = new DelegateCommand(o => DoGoogleLogin());
            FacebookLogin = new DelegateCommand(o => DoFacebookLogin());
        }

        protected void GoToJoinTeamPage()
        {
            _navigationService.Navigate(typeof(JoinTeamPageViewModel));
        }


        #region Identity Provider Authentication

        private MobileServiceUser user;

        private async Task<bool> Authenticate(MobileServiceAuthenticationProvider provider)
        {
            while (user == null)
            {
                try
                {
                    user = await App.MobileService
                        .LoginAsync(MobileServiceAuthenticationProvider.Facebook);

                    //user.UserId;
                    //user.MobileServiceAuthenticationToken;

                    return true;
                }
                catch (InvalidOperationException)
                {
                    return false;
                }
            }

            return false;
        }

        private async Task<bool> AuthenticateFacebook()
        {
            return await Authenticate(MobileServiceAuthenticationProvider.Facebook);
        }

        private async Task<bool> AuthenticateGoogle()
        {
            return await Authenticate(MobileServiceAuthenticationProvider.Google);
        }

        #endregion

        private async void DoGoogleLogin()
        {
            if (await AuthenticateGoogle())
                _navigationService.Navigate(typeof(JoinTeamPage));
        }

        private async void DoFacebookLogin()
        {
            if (await AuthenticateFacebook())
                _navigationService.Navigate(typeof(JoinTeamPage));
        }
    }
}
