using Caliburn.Micro;
using Bootcamp2015.AmazingRace.Base.Models;
using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;
using System.Threading.Tasks;
using System.Text;
using System.Windows.Input;
using Bootcamp2015.AmazingRace.Base;

namespace Bootcamp2015.AmazingRace.ViewModels
{
    public class JoinTheTeamPageViewModel : Screen
    {
        private Profile currentUser;
        private readonly IDataService dataService;
        private readonly INavigationService navigationService;

        public Profile CurrentUser
        {
            get { return this.currentUser; }
            set
            {
                this.currentUser = value;
                this.NotifyOfPropertyChange<Profile>(() => this.CurrentUser);
            }
        }

        public string WelcomeMessage
        {
            get {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"Welcome, ");
                if(this.CurrentUser != null)
                {
                    sb.Append(this.CurrentUser.DisplayName ?? "Bryan");
                }
                else
                {
                    sb.Append("Bryan");
                }
                sb.Append(@"!");
                return sb.ToString();
            }
        }

        private ICommand joinCommand;

        public ICommand JoinCommand
        {
            get
            {
                return new DelegateCommand(() => this.Join());
            }
        }

        public JoinTheTeamPageViewModel(IDataService dataService, INavigationService navigationService)
        {
            this.dataService = dataService;
            this.navigationService = navigationService;
        }

        protected async override void OnInitialize()
        {
            base.OnInitialize();

            this.CurrentUser = await this.dataService.GetProfile();
        }

        protected override void OnActivate()
        {
            base.OnActivate();
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
        }

        private void Join()
        {
            //joind the team
            //run background task
            //move to Leaderboard

            this.navigationService.NavigateToViewModel<LeaderboardPageViewModel>();
        }
    }
}
