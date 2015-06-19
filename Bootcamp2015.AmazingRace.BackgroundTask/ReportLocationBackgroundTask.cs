using Windows.ApplicationModel.Background;
using Bootcamp2015.AmazingRace.Base.Helpers;
using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;

namespace Bootcamp2015.AmazingRace.BackgroundTask
{
    public sealed class ReportLocationBackgroundTask : IBackgroundTask
    {
        private readonly IDataService dataService;
        private readonly ISettingsService settingsService;

        
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();

            //TODO await?
            await UpdateLocationHelper.UpdateLocation();

            deferral.Complete();
        }
    }
}
