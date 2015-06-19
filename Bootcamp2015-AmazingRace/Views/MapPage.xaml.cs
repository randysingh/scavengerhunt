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
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Bootcamp2015.AmazingRace.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapPage : Page
    {
        private Geolocator _locator;

        public MapPage()
        {
            this.InitializeComponent();

            _locator = new Geolocator() { ReportInterval = 1000 };
            Pins = new ObservableCollection<PinViewModel>();

            this.DataContext = this;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Refresh();
          //  _locator.PositionChanged += OnLocatorPositionChanged;
        }


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
           // _locator.PositionChanged -= OnLocatorPositionChanged;
        }

        private async void OnLocatorPositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                UpdateMyLocation(args.Position);
            });
        }

        public ObservableCollection<PinViewModel> Pins { get; set; }


        private async void OnRefreshClick(object sender, RoutedEventArgs e)
        {
            await Refresh();
        }

        private async void OnAddPinClick(object sender, RoutedEventArgs e)
        {
            var pin = new PinViewModel
            {
                Text = "point",
                Color = new SolidColorBrush(Colors.Red),
                Location = _map.Center,
                CanGetDirections = true,
            };

            Pins.Add(pin);
        }

        private async Task Refresh()
        {
            var pos = await _locator.GetGeopositionAsync();
            UpdateMyLocation(pos);
        }

        private void UpdateMyLocation(Geoposition p)
        {
            var pos = new Geopoint(p.Coordinate.Point.Position);
            _map.Center = pos;

            var pin = new PinViewModel
            {
                Text = "current location",
                Color = new SolidColorBrush(Colors.Green),
                Location = pos
            };

            if (Pins.Count > 0)
            {
                Pins.RemoveAt(0);
            }
            Pins.Insert(0, pin);
        }

        #region Directions

        private void OnDirectionsClick(object sender, RoutedEventArgs e)
        {
            var pin =  ((sender as FrameworkElement).DataContext) as PinViewModel;
            ShowDirectionsExternal(pin);
        }

        private async void ShowDirectionsInApp(PinViewModel pin)
        {
            var routeResult = await MapRouteFinder.GetDrivingRouteAsync(
                            Pins[0].Location,
                            pin.Location,
                            MapRouteOptimization.Time,
                            MapRouteRestrictions.None);

            var viewOfRoute = new MapRouteView(routeResult.Route);
            viewOfRoute.RouteColor = Colors.Yellow;
            viewOfRoute.OutlineColor = Colors.Black;
            _map.Routes.Clear();
            _map.Routes.Add(viewOfRoute);
            await _map.TrySetViewBoundsAsync(routeResult.Route.BoundingBox, new Thickness(10), MapAnimationKind.Bow);
        }

        private async void ShowDirectionsExternal(PinViewModel pin)
        {
            var latitude = pin.Location.Position.Latitude;
            var longitude = pin.Location.Position.Longitude;
            string name = "My next clue";

            // Assemble the Uri to launch.
            Uri uri = new Uri("ms-drive-to:?destination.latitude=" + latitude + "&destination.longitude=" + longitude + "&destination.name=" + name);

            // Launch the Uri.
            var success = await Windows.System.Launcher.LaunchUriAsync(uri);
        }

        #endregion

    }

    public class PinViewModel
    {
        public string Text { get; set; }
        public Brush Color { get; set; }

        public bool CanGetDirections { get; set; }
        public Geopoint Location { get; set; }
        public Point Anchor { get { return new Point() { X = 0.1, Y = 1 }; } }
    }
}
