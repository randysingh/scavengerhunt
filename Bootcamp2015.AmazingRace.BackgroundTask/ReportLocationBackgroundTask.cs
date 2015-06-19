using System;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Bootcamp2015.AmazingRace.Base.Helpers;

namespace Bootcamp2015.AmazingRace.BackgroundTask
{
    public class ReportLocationBackgroundTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();

            //TODO await?
            await UpdateLocationHelper.UpdateLocation();

            deferral.Complete();
        }
    }
}
