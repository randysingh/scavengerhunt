using Caliburn.Micro;
using Bootcamp2015.AmazingRace.Base.Models;
using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;
using System.Threading.Tasks;
using System.Text;
using System.Windows.Input;
using System.Linq;
using Bootcamp2015.AmazingRace.Base;
using Bootcamp2015.AmazingRace.Base.Helpers;
using System;

namespace Bootcamp2015.AmazingRace.ViewModels
{
    public class JoinTheTeamPageViewModel : Screen
    {
        private readonly IDataService dataService;
        private readonly INavigationService navigationService;

        private Profile currentUser;
        private string teamCode;

        public Profile CurrentUser
        {
            get { return this.currentUser; }
            set
            {
                this.currentUser = value;
                this.NotifyOfPropertyChange(() => this.CurrentUser);
                this.NotifyOfPropertyChange(() => this.WelcomeMessage);
            }
        }

        public string WelcomeMessage
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"Welcome, ");
                if (this.CurrentUser != null)
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

        public ICommand JoinCommand
        {
            get
            {
                return new DelegateCommand(() => this.Join());
            }
        }

        public string TeamCode
        {
            get { return this.teamCode; }
            set
            {
                this.teamCode = value;
                this.NotifyOfPropertyChange(() => this.TeamCode);
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

        private async void Join()
        {
            //join the team
            try
            {
                var team = await this.dataService.PostJoinTeam(this.teamCode);
            }
            catch (Exception e)
            {
                TeamCode = "Invalid Code.";
                return;
            }

            await BackgroundTaskHelpers.BackgroundTaskRegister();

            var races = await dataService.GetRaceList();
            Race currentRace = races.FirstOrDefault();
            string raceId = currentRace.Id;

            this.navigationService.NavigateToViewModel<LeaderboardPageViewModel>(raceId);
        }
    }
}
