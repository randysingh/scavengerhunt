using Bootcamp2015.AmazingRace.Base.Models;
using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;
using Microsoft.WindowsAzure.MobileServices;
using System.Linq;
using Windows.Devices.Geolocation;
using Windows.Security.Credentials;

namespace Bootcamp2015.AmazingRace.Base.Helpers
{
    public static class LocationHelper
    {
        private static Geolocator _locator;
        private static IDataService _dataService;
        private static ISettingsService _settingsService;

        private static Team team = _settingsService.GetDeserializedValueOrDefault<Team>("TEAM");
        
        public static void Start() {
            PasswordVault vault = new PasswordVault();
            PasswordCredential credential = vault.FindAllByResource(MobileServiceAuthenticationProvider.Google.ToString()).FirstOrDefault();

            if (credential != null)
            {
                MobileServiceUser user = new MobileServiceUser(credential.UserName);
                credential.RetrievePassword();
                user.MobileServiceAuthenticationToken = credential.Password;

                if (_locator == null)
                {
                    _locator = new Geolocator() { ReportInterval = 1000 };
                    //_locator.getGeopositionAsync();
                    _locator.PositionChanged += OnLocatorPositionChanged;
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
            //_dataService.UpdateLocationAsync(team.Id, pos.Position.Latitude, pos.Position.Longitude);
        }
    }
}
