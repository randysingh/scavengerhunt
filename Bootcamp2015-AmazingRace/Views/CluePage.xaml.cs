using Bootcamp2015.AmazingRace.Base.APIModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Bootcamp2015.AmazingRace.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CluePage : Page
    {
        public CluePage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            Clue currentClue = await App.Api.getClue();
            ClueDescription.Text = currentClue.description;
        }

        private void LogoutClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async void ViewClick(object sender, RoutedEventArgs e)
        {
            // make api call to get clue
            
            Frame.Navigate(typeof(MapPage));
        }

        private void PhotoClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void SkipClick(object sender, RoutedEventArgs e)
        {
            // api call to GET api/clue/{id} with a new clue id
            // make sure only unsolved clues displayed
        }
    }
}
