using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace Bootcamp2015.AmazingRace.Base.ServiceInterfaces
{
    public interface IMobileService
    {
        MobileServiceClient ServiceClient { get; }
        void Initialize();
    }
}
