using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Bootcamp2015.AmazingRace.Base;

namespace Bootcamp2015.AmazingRace.ViewModels
{
    public class MainPageViewModel : Screen
    {
        private INavigationService navigationService;

        public MainPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        private ICommand command;
        public ICommand Command
        {
            get
            {
                return new DelegateCommand(() => GoToPage());
            }
        }

        public void GoToPage()
        {
            this.navigationService.NavigateToViewModel<RegistrationPageViewModel>();
        }

    }
}
