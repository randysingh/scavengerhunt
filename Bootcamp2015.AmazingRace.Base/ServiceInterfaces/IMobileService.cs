using Microsoft.WindowsAzure.MobileServices;

namespace Bootcamp2015.AmazingRace.Base.ServiceInterfaces
{
    public interface IMobileService
    {
        MobileServiceClient ServiceClient { get; }
        void Initialize();
    }
}
