using Bootcamp2015.AmazingRace.Base;
using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;
using Bootcamp2015.AmazingRace.Helpers;
using Bootcamp2015.AmazingRace.Views;
using Caliburn.Micro;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
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
        private const string PROVIDER_GOOGLE = "Google";
        private const string PROVIDER_FACEBOOK = "Facenook";

        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;
        private readonly ISettingsService _settingsService;

        private string provider;
        private MobileServiceUser user;

        private IMobileServiceClient _serviceClient;

        #region Bindable properties

        public ICommand GoogleLogin { get; set; }
        public ICommand FacebookLogin { get; set; }

        #endregion

        public MainPageViewModel(INavigationService navigationService,
            IDataService dataService, ISettingsService settingsService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
            _settingsService = settingsService;

            _serviceClient = _dataService.MobileServiceClient();

            GoogleLogin = new DelegateCommand(() => DoGoogleLogin());
            FacebookLogin = new DelegateCommand(() => DoFacebookLogin());
        }

        #region Identity Provider authentication

        private async Task<bool> Authenticate(MobileServiceAuthenticationProvider providerType)
        {
            while (user == null)
            {
                if (_settingsService.HaveValue(provider))
                {
                    user = _settingsService.GetDeserializedValueOrDefault<MobileServiceUser>(provider);
                }

                if (user != null)
                {
                    _serviceClient.CurrentUser = user;

                    return true;
                }
                else
                {
                    try
                    {
                        user = await _serviceClient.LoginAsync(providerType);

                        _settingsService.SetSerializedValue<MobileServiceUser>(provider, user);

                        return true;
                    }
                    catch (InvalidOperationException)
                    {
                        return false;
                    }
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
            provider = PROVIDER_GOOGLE;

            if (await AuthenticateGoogle())
                _navigationService.NavigateToViewModel<JoinTeamPageViewModel>();
        }

        private async void DoFacebookLogin()
        {
            provider = PROVIDER_FACEBOOK;

            if (await AuthenticateFacebook())
                _navigationService.NavigateToViewModel<JoinTeamPageViewModel>();
        }

        #endregion

        public void ContinueWebAuthentication(WebAuthenticationBrokerContinuationEventArgs args)
        {
            if (args.Kind == ActivationKind.WebAuthenticationBrokerContinuation)
            {
                _serviceClient.LoginComplete(args as WebAuthenticationBrokerContinuationEventArgs);
            }
        }
    }
}
