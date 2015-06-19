using Bootcamp2015.AmazingRace.Base;
using Bootcamp2015.AmazingRace.Helpers;
using Bootcamp2015.AmazingRace.Views;
using Caliburn.Micro;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Activation;

namespace Bootcamp2015.AmazingRace.ViewModels
{
    public class MainPageViewModel : Screen, IWebAuthenticationContinuable
    {
        private readonly INavigationService _navigationService;
        private MobileServiceUser user;

        #region Bindable properties

        public ICommand GoogleLogin { get; set; }
        public ICommand FacebookLogin { get; set; }

        #endregion

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            GoogleLogin = new DelegateCommand(() => DoGoogleLogin());
            FacebookLogin = new DelegateCommand(() => DoFacebookLogin());
        }

        #region Identity Provider authentication

        private async Task<bool> Authenticate(MobileServiceAuthenticationProvider provider)
        {
            while (user == null)
            {
                try
                {
                    user = await App.MobileService.LoginAsync(provider);

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

        #region Methods for handling button actions

        private async void DoGoogleLogin()
        {
            //if (await AuthenticateGoogle())
            //    _navigationService.Navigate(typeof(JoinTeamPage));

            _navigationService.Navigate(typeof(JoinTeamPage));
        }

        private async void DoFacebookLogin()
        {
            if (await AuthenticateFacebook())
                _navigationService.Navigate(typeof(JoinTeamPage));
        }

        #endregion

        public void ContinueWebAuthentication(Windows.ApplicationModel.Activation.WebAuthenticationBrokerContinuationEventArgs args)
        {
            if (args.Kind == ActivationKind.WebAuthenticationBrokerContinuation)
            {
                App.MobileService.LoginComplete(args as WebAuthenticationBrokerContinuationEventArgs);
            }
        }
    }
}
