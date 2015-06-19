using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;
using Caliburn.Micro;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Activation;
using Windows.Security.Credentials;
using Bootcamp2015.AmazingRace.Base;
using Bootcamp2015.AmazingRace.Helpers;

namespace Bootcamp2015.AmazingRace.ViewModels
{
    public class MainPageViewModel : Screen, IWebAuthenticationContinuable
    {
        private IMobileService mobileService;
        private INavigationService navigationService;

        public MainPageViewModel(ISettingsService settingsService, IMobileService mobileService, INavigationService navigationService)
        {
            this.mobileService = mobileService;
            this.navigationService = navigationService;
        }

        private ICommand loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                return new DelegateCommand(() => this.Login());
            }
        }

        public async void Login()
        {
            this.mobileService.Initialize();
            var result = await this.mobileService.ServiceClient.LoginAsync(MobileServiceAuthenticationProvider.Google);
            var passwordCredetials = new PasswordCredential(MobileServiceAuthenticationProvider.Google.ToString(), result.UserId, result.MobileServiceAuthenticationToken);
            var vault = new PasswordVault();
            vault.Add(passwordCredetials);
            this.navigationService.NavigateToViewModel<JoinTheTeamPageViewModel>();
        }

        public void ContinueWebAuthentication(WebAuthenticationBrokerContinuationEventArgs args)
        {
            if (args.Kind == ActivationKind.WebAuthenticationBrokerContinuation)
            {
                this.mobileService.ServiceClient.LoginComplete(args as WebAuthenticationBrokerContinuationEventArgs);
            }

        }
    }
}
