using Bootcamp2015.AmazingRace.Base;
using Bootcamp2015.AmazingRace.Base.Helpers;
using Microsoft.WindowsAzure.MobileServices;
using Windows.ApplicationModel.Background;

namespace Bootcamp2015.AmazingRace.BackgroundTask
{
    public sealed class BTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();

            var credentials = PasswordVaultHelper.GetPasswordCredential();
            var user = PasswordVaultHelper.GetUser();
            var client = new MobileServiceClient(
                Connections.MobileServicesUri, 
                Connections.MobileServicesAppKey
            );
            client.CurrentUser = user;

            LocationHelper.Start();

            deferral.Complete();
        }
    }
}
