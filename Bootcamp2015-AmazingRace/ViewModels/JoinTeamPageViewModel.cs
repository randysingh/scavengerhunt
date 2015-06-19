using Bootcamp2015.AmazingRace.Common;
using Bootcamp2015.AmazingRace.Helpers;
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

        private String _teamCode;
        public String TeamCode
        {
            get { return _teamCode; }
            set { _teamCode = value; NotifyOfPropertyChange(); }
        }

        public ICommand GotoLeaderboardCommand
        {
            get { return  new DelegateCommand(o => OnGotoLeaderboardPage()); }
        }

        public JoinTeamPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        protected void OnGotoLeaderboardPage()
        {
            MobileServiceHelper.JoinTeam(this._teamCode);
            _navigationService.NavigateToViewModel<LeaderboardPageViewModel>();
        }
    }
}
