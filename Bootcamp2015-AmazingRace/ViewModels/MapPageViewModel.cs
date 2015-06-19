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
    class MapPageViewModel : Screen
    {
        private Geolocator _locator;
        private Geopoint _mapcenter;
        private Geopoint _cluelocation;
        private CoreDispatcher _dispatcher;

        public MapPageViewModel(double latitude, double longitude)
        {
            _locator = new Geolocator() { ReportInterval = 1000 };

            Pins = new ObservableCollection<PinViewModel>();
            
            var geopos = new BasicGeoposition();
            geopos.Latitude = latitude;
            geopos.Longitude = longitude;
            geopos.Altitude = 120.0;
            _cluelocation = new Geopoint(geopos);
        }

        public ObservableCollection<PinViewModel> Pins { get; set; }

        public async Task Refresh()
        {
            var pos = await _locator.GetGeopositionAsync();
            UpdateMyLocation(pos);
            UpdateClueLocation();
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

        public Geopoint MapCenter
        {
            get
            {
                return _mapcenter;
            }
            set
            {
                _mapcenter = value;
                NotifyOfPropertyChange(() => MapCenter);
            }
        }

        public void UpdateClueLocation()
        {
            MapCenter = _cluelocation;
            //RaisePropertyChanged("MapCenter");

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

        public void UpdateMyLocation(Geoposition p)
        {
            var pos = new Geopoint(p.Coordinate.Point.Position);
            MapCenter = pos;
            
            //RaisePropertyChanged("MapCenter");

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

        /*public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }*/

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
