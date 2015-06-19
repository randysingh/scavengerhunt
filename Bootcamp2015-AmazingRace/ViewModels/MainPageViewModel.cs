using Bootcamp2015.AmazingRace.Common;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bootcamp2015.AmazingRace.ViewModels
{
    public class MainPageViewModel : Screen
    {
        private INavigationService _navigationService;

        public ICommand GotoJoinTeamCommand
        {
            get{return  new DelegateCommand(o => OnGotoJoinTeamPage());}
        }


        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

        }

        protected void OnGotoJoinTeamPage()
        {
            _navigationService.NavigateToViewModel<JoinTeamPageViewModel>();
        }
    }
}
