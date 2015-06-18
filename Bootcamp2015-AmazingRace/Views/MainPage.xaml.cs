using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;
using Bootcamp2015.AmazingRace.ViewModels;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;  


namespace Bootcamp2015.AmazingRace.Views
{
    /// <summary>
    /// THe main login page
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            this.DataContext = new MainPageViewModel();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private MobileServiceUser user;
        
        private async Task<bool> Authenticate(MobileServiceAuthenticationProvider provider)
        {
            while (user == null)
            {
                string message;
                try
                {
                    user = await App.MobileService
                        .LoginAsync(MobileServiceAuthenticationProvider.Facebook);
                    message =
                        string.Format("You are now logged in - {0}", user.UserId);

                    return true;
                }
                catch (InvalidOperationException)
                {
                    message = "You must log in. Login Required";
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

        private async void btnGoogle_Click(object sender, RoutedEventArgs e)
        {
            if (await AuthenticateGoogle())
                Frame.Navigate(typeof(JoinTeamPage));
        }

        private async void btnFacebook_Click(object sender, RoutedEventArgs e)
        {
            if (await AuthenticateFacebook())
                Frame.Navigate(typeof(JoinTeamPage));
        }
    }
}
