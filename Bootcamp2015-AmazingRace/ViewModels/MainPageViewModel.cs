using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;
using Caliburn.Micro;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Activation;
using Bootcamp2015.AmazingRace.Base;
using Bootcamp2015.AmazingRace.Helpers;

namespace Bootcamp2015.AmazingRace.ViewModels
{
    public class MainPageViewModel : Screen, IWebAuthenticationContinuable
    {
        private IMobileService mobileService;

        public MainPageViewModel(ISettingsService settingsService, IMobileService mobileService)
        {
            this.mobileService = mobileService;
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

            //client.CurrentUser 
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
