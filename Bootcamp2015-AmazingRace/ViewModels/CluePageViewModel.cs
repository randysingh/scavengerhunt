using Bootcamp2015.AmazingRace.Base;
using Bootcamp2015.AmazingRace.Base.Models;
using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;
using Caliburn.Micro;
using System.Windows.Input;
using System.Linq;

namespace Bootcamp2015.AmazingRace.ViewModels
{
    public class CluePageViewModel : Screen
    {
        private INavigationService navigationService;
        private IDataService dataService;

        public CluePageViewModel(INavigationService navigationService, IDataService dataService)
        {
            this.navigationService = navigationService;
            this.dataService = dataService;
        }

        public ICommand BackToLeaderboardCommand
        {
            get
            {
                return new DelegateCommand(() => this.BackToLeaderboard());
            }
        }

        private async void BackToLeaderboard()
        {
            var races = await dataService.GetRaceList();
            Race currentRace = races.FirstOrDefault();
            string raceId = currentRace.Id;
            this.navigationService.NavigateToViewModel<LeaderboardPageViewModel>(raceId);
        }
        
    }
}
