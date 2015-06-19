using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp2015.AmazingRace.Base.Services
{
    public class MobileService : IMobileService
    {
        private MobileServiceClient client;

        public Microsoft.WindowsAzure.MobileServices.MobileServiceClient ServiceClient
        {
            get
            {
                return this.client;
            }
        }

        public void Initialize()
        {
            if (this.client == null)
            {
                this.client = new MobileServiceClient(ApplicationConstants.MobileServicesUri, ApplicationConstants.MobileServicesAppKey);
            }


        }
    }
}
