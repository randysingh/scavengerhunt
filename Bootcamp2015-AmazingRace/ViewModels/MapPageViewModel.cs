using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Bootcamp2015.AmazingRace.ViewModels
{
    public class MapPageViewModel : Screen
    {
        private Geolocator _locator;

        public MapPageViewModel()
        {
            _locator = new Geolocator() { ReportInterval = 1000 };
        }
    }
}
