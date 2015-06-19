using Bootcamp2015.AmazingRace.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml.Media;

namespace Bootcamp2015.AmazingRace.ViewModels 
{
    public class MapPageViewModel : Screen, IParameterReceivable<Clue>
    {
        private Geolocator _locator;
        private Geopoint _mapcenter;
        private Geopoint _cluelocation;
        private CoreDispatcher _dispatcher;
        private INavigationService _navigationService;

         public MapPageViewModel (INavigationService navigationService)
        {
            _navigationService = navigationService;
            _locator = new Geolocator() { ReportInterval = 10000 };

            Pins = new ObservableCollection<PinViewModel>();
            
            
        }

        public ObservableCollection<PinViewModel> Pins { get; set; }

        public async Task Refresh()
        {
            var pos = await _locator.GetGeopositionAsync();
            UpdateMyLocation(pos);
            UpdateClueLocation();
        }

        public Clue Clue { get; set; }

        public void ProcessPayload(Clue payload)
        {
            // here is your received item
            Clue = payload; //save payload
            var geopos = new BasicGeoposition();
            Double.TryParse(Clue.latitude, out geopos.Latitude);
            Double.TryParse(Clue.longitude, out geopos.Longitude);
            geopos.Altitude = 120.0;
            _cluelocation = new Geopoint(geopos);
            Refresh();
        }

        public Geolocator Locator
        {
            get
            {
                return _locator;
            }
            set
            {
                _locator = value;
            }
        }

        public Geopoint MapCenter { get; set; }

        

        protected override void OnActivate()
        {
            _locator.PositionChanged += OnLocatorPositionChanged;
            base.OnActivate();
        }

        protected override void OnDeactivate(bool close)
        {
            _locator.PositionChanged -= OnLocatorPositionChanged;
            base.OnDeactivate(close);
        }

        private async void OnLocatorPositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            Caliburn.Micro.Execute.OnUIThread(() => UpdateMyLocation(args.Position));
        }

        public void UpdateMyLocation(Geoposition p)
        {
            var pos = p.Coordinate.Point;
            MapCenter = pos;
            NotifyOfPropertyChange(() => MapCenter);
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

        public void UpdateClueLocation()
        {
            MapCenter = _cluelocation;
            NotifyOfPropertyChange(() => MapCenter);
            var pin = new PinViewModel
            {
                Text = "clue location",
                Color = new SolidColorBrush(Colors.Red),
                Location = _cluelocation
            };

            if (Pins.Count > 1)
            {
                Pins.RemoveAt(1);
            }
            Pins.Insert(1, pin);
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
}
