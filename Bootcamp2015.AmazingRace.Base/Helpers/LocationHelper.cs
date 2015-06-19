using Bootcamp2015.AmazingRace.Base.Models;
using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Windows.Devices.Geolocation;
using Windows.Security.Credentials;

namespace Bootcamp2015.AmazingRace.Base.Helpers
{
    public static class LocationHelper
    {
        private static Geolocator _locator;

        private static PasswordCredential credentials;
        private static MobileServiceClient client;

        //private static Team team = _settingsService.GetDeserializedValueOrDefault<Team>("TEAM");

        public static void Start()
        {
            if (credentials == null)
            {
                credentials = PasswordVaultHelper.GetPasswordCredential();

                if (credentials != null)
                {
                    var user = PasswordVaultHelper.GetUser();
                    client = new MobileServiceClient(Connections.MobileServicesUri, Connections.MobileServicesAppKey);
                    client.CurrentUser = user;

                    if (_locator == null)
                    {
                        _locator = new Geolocator() { ReportInterval = 1000 };
                        //_locator.getGeopositionAsync();
                        _locator.PositionChanged += OnLocatorPositionChanged;
                    }
                }
            }
        }

        private static Geopoint _currentPos;
        public static Geopoint CurrentPosition
        {
            get { return _currentPos; }
            set { _currentPos = value; }
        }

        private static void OnLocatorPositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            // The new location
            var pos = new Geopoint(args.Position.Coordinate.Point.Position);

            // Set current position
            CurrentPosition = pos;

            // POST it
            client.InvokeApiAsync("updatelocation", HttpMethod.Post,
                new Dictionary<string, string>() { 
                    { "raceId", "" },
                    { "latitude", pos.Position.Latitude.ToString() },
                    { "longitude", pos.Position.Longitude.ToString() }
                });
        }
    }
}
