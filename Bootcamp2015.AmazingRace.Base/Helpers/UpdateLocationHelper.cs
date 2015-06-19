using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;
using Caliburn.Micro;

namespace Bootcamp2015.AmazingRace.Base.Helpers
{
    public static class UpdateLocationHelper
    {
        private static Geolocator locator = new Geolocator() { ReportInterval = 1000 };
        private static IDataService dataService;
        private static ISettingsService settingsService;


        static UpdateLocationHelper()
        {
            dataService = IoC.Get<IDataService>();
            settingsService = IoC.Get<ISettingsService>();
        }

        public async static Task UpdateLocation()
        {
            var pos = await locator.GetGeopositionAsync();
            UpdateMyLocation(pos);
        }

        private static void UpdateMyLocation(Geoposition p)
        {
            var pos = new Geopoint(p.Coordinate.Point.Position);
            var raceId = settingsService.GetValueOrDefault<string>("raceId", string.Empty);
            dataService.PostTeamLocation(raceId, pos.Position.Latitude, pos.Position.Longitude);
        }
    }
}