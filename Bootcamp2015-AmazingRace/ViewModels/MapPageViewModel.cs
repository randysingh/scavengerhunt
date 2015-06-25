using Bootcamp2015.AmazingRace.Base.Helpers;
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

        public MapPageViewModel()
        {
            Pins = new ObservableCollection<PinViewModel>();

            UpdateMyLocation(CurrentPosition);
        }

        public string MapToken { get { return ApplicationConstants.MapToken; } }

        public Geopoint CurrentPosition
        {
            get { return LocationHelper.CurrentPosition; }
        }

        private void UpdateMyLocation(Geopoint pos)
        {
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
