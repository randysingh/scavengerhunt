using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp2015.AmazingRace.Base.ServiceInterfaces
{
    public interface IMessageDialogService
    {
        Task ShowAsync(string message, string caption);
    }
}
