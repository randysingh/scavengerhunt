using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace Bootcamp2015.AmazingRace.Base
{
    public class BackgroundTaskHelpers
    {
        private const string BT_ENTRY = "Bootcamp2015.AmazingRace.BackgroundTask.BTask";
        private const string BT_NAME = "Bootcamp Amazing Race BTask";

        public static async Task<bool> BackgroundTaskRegister()
        {
            // Check if the task is already registered
            if (BackgroundTaskRegistration.AllTasks.Any(t => t.Value.Name == BT_NAME))
                return true;

            // Request access -> return false if denied
            var res = await BackgroundExecutionManager.RequestAccessAsync();
            if (res == BackgroundAccessStatus.Denied)
                return false;

            // Set up the trigger
            var trigger = new TimeTrigger(15, false);

            // Create the task 
            var task = new BackgroundTaskBuilder
            {
                Name = BT_NAME,
                TaskEntryPoint = BT_ENTRY,
            };
            task.SetTrigger(trigger);

            // Register the task
            task.Register();
            return true;
        }

        public static void BackgroundTaskRemove()
        {
            var task = BackgroundTaskRegistration.AllTasks.FirstOrDefault(t => t.Value.Name == BT_NAME);
            if (task.Value != null)
            {
                task.Value.Unregister(true);
            }
        }
    }
}
