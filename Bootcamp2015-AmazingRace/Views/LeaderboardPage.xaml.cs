using Bootcamp2015.AmazingRace.Base.Helpers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace Bootcamp2015.AmazingRace.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LeaderboardPage : Page
    {
        public LeaderboardPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void OnRegisterBackgroundTask(object sender, RoutedEventArgs e)
        {
            await BackgroundTaskHelpers.BackgroundTaskRegister();
        }

        private void OnUnregisterBackgroundTask(object sender, RoutedEventArgs e)
        {
            BackgroundTaskHelpers.BackgroundTaskRemove();
        }
    }
}
