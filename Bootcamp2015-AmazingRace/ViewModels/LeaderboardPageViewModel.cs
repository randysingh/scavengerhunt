﻿using Bootcamp2015.AmazingRace.Common;
using Bootcamp2015.AmazingRace.Helpers;
using Bootcamp2015.AmazingRace.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bootcamp2015.AmazingRace.ViewModels
{
    public class LeaderboardPageViewModel : Screen
    {
        private INavigationService _navigationService;

        public ICommand GotoClueCommand
        {
            get { return  new DelegateCommand(o => OnGotoCluePage()); }
        }

        public LeaderboardPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        protected async void OnGotoCluePage()
        {

            Clue _clue = await MobileServiceHelper.GetNextClue();
            // Pass in the clue to the view
            _navigationService.NavigateToViewModel<CluePageViewModel>(_clue);

            //TODO: Get real clue
        }
    }
}
