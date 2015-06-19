using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Security.Credentials;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Bootcamp2015.AmazingRace.Base.Models;
using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;
using Bootcamp2015.AmazingRace.Base.Services;
using Caliburn.Micro;
using Microsoft.WindowsAzure.MobileServices;

namespace Bootcamp2015.AmazingRace.Base.Helpers
{
    public static class UpdateLocationHelper
    {
        private static Geolocator locator = new Geolocator() { ReportInterval = 1000 };
        

        public async static Task UpdateLocation()
        {
            var pos = await locator.GetGeopositionAsync();
            UpdateMyLocation(pos);
        }

        private async static void UpdateMyLocation(Geoposition p)
        {
            var pos = new Geopoint(p.Coordinate.Point.Position);

            var vault = new PasswordVault();
            PasswordCredential passCred = null;

            try
            {
                passCred = vault.FindAllByResource(MobileServiceAuthenticationProvider.Google.ToString()).FirstOrDefault();
            }
            catch 
            {
                
            }

            passCred.RetrievePassword();

            var mobileServiceClinet = new MobileServiceClient(ApplicationConstants.MobileServicesUri, ApplicationConstants.MobileServicesAppKey);
            var mobileServiceUser = new MobileServiceUser(passCred.UserName);

            mobileServiceUser.MobileServiceAuthenticationToken = passCred.Password;

            mobileServiceClinet.CurrentUser = mobileServiceUser;

            var profile = await mobileServiceClinet.InvokeApiAsync<Profile>("profile", HttpMethod.Get, null);
        }
    }
}