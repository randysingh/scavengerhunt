using Caliburn.Micro;
using Bootcamp2015.AmazingRace.Base.Models;
using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;
using System.Collections.Generic;
using System.Windows.Input;
using Bootcamp2015.AmazingRace.Base;

namespace Bootcamp2015.AmazingRace.ViewModels
{
    public class LeaderboardPageViewModel : Screen
    {
        private Race _race;
        private IDataService dataService;
        private INavigationService navigationService;

        public string Parameter { get; set; }

        public Race Race
        {
            get { 
                return _race; 
            }
            set 
            { 
                _race = value;
                NotifyOfPropertyChange<Race>(() => Race);
            }
        }

        public ICommand NextClueCommand
        {
            get
            {
                return new DelegateCommand(() => this.NextClue());
            }
        }

        public LeaderboardPageViewModel(IDataService dataService, INavigationService navigationService)
        {
            this.dataService = dataService;
            this.navigationService = navigationService;
        }

        protected async override void OnInitialize()
        {
            base.OnInitialize();
            this.Race = await this.dataService.GetRace(Parameter);
        }

        protected override void OnActivate()
        {
            base.OnActivate();
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
        }


        private void NextClue()
        {
            this.navigationService.NavigateToViewModel<CluePageViewModel>();
        }

    }
}
