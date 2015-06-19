using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bootcamp2015.AmazingRace.Common
{
    public class DelegateCommand : ICommand
    {
        Action<object> _exectute;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<object> exectute)
        {
            _exectute = exectute;
        }

        public void Execute(object parameter)
        {
            _exectute(parameter);
        }
    }
}
