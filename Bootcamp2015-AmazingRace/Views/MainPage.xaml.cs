using Bootcamp2015.AmazingRace.Base;
using Bootcamp2015.AmazingRace.Helpers;
using Microsoft.WindowsAzure.MobileServices;
using System;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Navigation;


namespace Bootcamp2015.AmazingRace.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : IWebAuthenticationContinuable
    {
        public static MobileServiceClient _mobileServiceClient;
        public MainPage()
        {
            _mobileServiceClient = new MobileServiceClient(Connections.MobileServicesUri, Connections.MobileServicesAppKey);
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        /// 
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private async void GoogleLoginClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var result = await _mobileServiceClient.LoginAsync(MobileServiceAuthenticationProvider.Google);
        }

        public void ContinueWebAuthentication(Windows.ApplicationModel.Activation.WebAuthenticationBrokerContinuationEventArgs args)
        {
            if (args.Kind == ActivationKind.WebAuthenticationBrokerContinuation)
            {
                _mobileServiceClient.LoginComplete(args as WebAuthenticationBrokerContinuationEventArgs);
            }
            LoginPrompt.Text = "Thank you for logging in. Please press Continue to proceed.";
            GoogleLogin.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void GoToRegistration(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegistrationPage));
        }
    }
}
