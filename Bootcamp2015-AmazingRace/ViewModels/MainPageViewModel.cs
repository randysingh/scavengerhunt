using System.Windows.Input;
using Windows.ApplicationModel.Activation;
using Bootcamp2015.AmazingRace.Base;
using Bootcamp2015.AmazingRace.Base.Helpers;
using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;
using Bootcamp2015.AmazingRace.Helpers;
using Caliburn.Micro;
using Microsoft.WindowsAzure.MobileServices;

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
            PasswordVaultHelper.PutGooglePasswordToPasswordVault(result);

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
