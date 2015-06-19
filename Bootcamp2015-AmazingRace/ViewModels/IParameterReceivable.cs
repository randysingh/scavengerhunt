using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp2015.AmazingRace.ViewModels
{
    /*
     * Implement in viewmodels to get payloads from a view
     */
    public interface IParameterReceivable<T>
    {
        void ProcessPayload(T payload);
    }
}
