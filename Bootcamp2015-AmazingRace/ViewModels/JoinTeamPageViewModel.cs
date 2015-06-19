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
    public class JoinTeamPageViewModel : Screen
    {
        private INavigationService _navigationService;

        public ICommand GotoLeaderboardCommand
        {
            get
            {
                return  new DelegateCommand(o => OnGotoLeaderboardPage());
            }
        }


        public JoinTeamPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            //GotoJoinTeamCommand = new DelegateCommand(o => OnGotoJoinTeamPage());
        }

        protected void OnGotoLeaderboardPage()
        {
            _navigationService.NavigateToViewModel<LeaderboardPageViewModel>();
        }
    }
}
