using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Bootcamp2015.AmazingRace.Base.Services
{
    public class MessageDialogService : IMessageDialogService
    {
        public async Task ShowAsync(string message, string caption)
        {
            var md = new MessageDialog(message, caption);
            await md.ShowAsync();
            return;
        }
    }
}
