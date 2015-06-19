using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;
using Caliburn.Micro;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp2015.AmazingRace.ViewModels
{
    public class MainPageViewModel : Screen
    {
        private IMobileService mobileService;
        
        public MainPageViewModel(ISettingsService settingsService, IMobileService mobileService)
        {
            this.mobileService = mobileService;
        }

        public async void Login()
        {
            this.mobileService.Initialize();
            await this.mobileService.ServiceClient.LoginAsync(MobileServiceAuthenticationProvider.Google);

            //client.CurrentUser 
        }
        
    }
}
