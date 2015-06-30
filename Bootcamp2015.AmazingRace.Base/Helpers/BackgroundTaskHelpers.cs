using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace Bootcamp2015.AmazingRace.Base.Helpers
{
    public class BackgroundTaskHelpers
    {
        private const string TaskEntry = "Bootcamp2015.AmazingRace.BackgroundTask.ReportLocationBackgroundTask";
        private const string TaskName = "Amazing race task";

        public static async Task<bool> BackgroundTaskRegister()
        {
            //Check if the task is already registered
            if (BackgroundTaskRegistration.AllTasks.Any(t => t.Value.Name == TaskEntry))
                return true;

            //Request access -> return false if denied
            var res = await BackgroundExecutionManager.RequestAccessAsync();
            if (res == BackgroundAccessStatus.Denied)
                return false;

            //set up the trigger
            var trigger = new TimeTrigger(15, false);

            //Create the task 
            var task = new BackgroundTaskBuilder
            {
                Name = TaskName,
                TaskEntryPoint = TaskEntry,
            };
            task.SetTrigger(trigger);

            //Register the task
            task.Register();
            return true;
        }

        public static void BackgroundTaskRemove()
        {
            var task = BackgroundTaskRegistration.AllTasks.FirstOrDefault(t => t.Value.Name == TaskEntry);
            if (task.Value != null)
            {
                task.Value.Unregister(true);
            }
        }

    }
}