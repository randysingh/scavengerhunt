using Caliburn.Micro;
using Bootcamp2015.AmazingRace.Base.Models;
using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;
using System.Threading.Tasks;
using System.Text;

namespace Bootcamp2015.AmazingRace.ViewModels
{
    public class JoinTheTeamPageViewModel : Screen
    {
        private Profile _currentUser;

        private IDataService dataService;

        public Profile CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                NotifyOfPropertyChange<Profile>(() => CurrentUser);
            }
        }

        public string WelcomeMessage
        {
            get {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"Welcome, ");
                if(this.CurrentUser != null)
                {
                    sb.Append(CurrentUser.DisplayName ?? "Bryan");
                }
                else
                {
                    sb.Append("Bryan");
                }
                sb.Append(@"!");
                return sb.ToString();
            }
        }

        public JoinTheTeamPageViewModel(IDataService dataService)
        {
            this.dataService = dataService;
        }

        protected async override void OnInitialize()
        {
            base.OnInitialize();

            CurrentUser = await dataService.GetProfile();
        }

        protected override void OnActivate()
        {
            base.OnActivate();
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
        }      
    }
}
