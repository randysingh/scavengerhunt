using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Bootcamp2015.AmazingRace.ViewModels
{
    public class MapPageViewModel : Screen
    {
        public ObservableCollection<PinViewModel> Pins { get; set; }

        private Geolocator _locator;

        public MapPageViewModel()
        {
            Pins = new ObservableCollection<PinViewModel>();

            // Locator
            if (_locator == null)
            {
                _locator = new Geolocator() { ReportInterval = 1000 };
            }

            if (_locator != null)
            {
                _locator.PositionChanged += OnLocatorPositionChanged;
                Refresh();
            }
        }

        public string MapToken { get { return ApplicationConstants.MapToken; } }

        private Geopoint _currentPos;
        public Geopoint CurrentPosition
        {
            get { return _currentPos; }
            set { _currentPos = value; }
        }

        private async void OnLocatorPositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            //await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            //{
            //    UpdateMyLocation(args.Position);
            //});
        }

        private async Task Refresh()
        {
            var pos = await _locator.GetGeopositionAsync();
            UpdateMyLocation(pos);
        }

        private void UpdateMyLocation(Geoposition p)
        {
            var pos = new Geopoint(p.Coordinate.Point.Position);
            CurrentPosition = pos;

            var pin = new PinViewModel
            {
                Text = "Current location",
                Color = new SolidColorBrush(Colors.Green),
                Location = pos
            };

            if (Pins.Count > 0)
            {
                Pins.RemoveAt(0);
            }
            Pins.Insert(0, pin);
        }
    }

    public class PinViewModel
    {
        public string Text { get; set; }
        public Brush Color { get; set; }

        public Geopoint Location { get; set; }
        public Point Anchor { get { return new Point() { X = 0.1, Y = 1 }; } }
    }
}
