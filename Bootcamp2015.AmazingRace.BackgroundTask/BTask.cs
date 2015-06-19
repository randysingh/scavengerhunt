using Bootcamp2015.AmazingRace.Base.Helpers;
using Windows.ApplicationModel.Background;

namespace Bootcamp2015.AmazingRace.BackgroundTask
{
    public sealed class BTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();

            LocationHelper.Start();

            deferral.Complete();
        }
    }
}
