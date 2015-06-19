using Bootcamp2015.AmazingRace.Base;
using Bootcamp2015.AmazingRace.Common;
using Bootcamp2015.AmazingRace.Helpers;
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
        private INavigationService _navigationService;
        private MobileServiceClient _mobileServiceClient;

        public ICommand GotoJoinTeamCommand
        {
            get{return  new DelegateCommand(o => OnGotoJoinTeamPage());}
        }


        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            
            _mobileServiceClient = new MobileServiceClient(Connections.MobileServicesUri, Connections.MobileServicesAppKey);

        }

        protected void OnGotoJoinTeamPage()
        {
            Login();
        }

        private async void Login()
        {
            var result = await _mobileServiceClient.LoginAsync(MobileServiceAuthenticationProvider.Google);
        }

        public void ContinueWebAuthentication(WebAuthenticationBrokerContinuationEventArgs args)
        {
            if (args.Kind == ActivationKind.WebAuthenticationBrokerContinuation)
            {
                _mobileServiceClient.LoginComplete(args as WebAuthenticationBrokerContinuationEventArgs);
                _navigationService.NavigateToViewModel<JoinTeamPageViewModel>();
            }

        }
    }
}
