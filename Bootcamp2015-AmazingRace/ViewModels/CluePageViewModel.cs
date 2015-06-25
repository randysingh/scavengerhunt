using Bootcamp2015.AmazingRace.Base;
using Bootcamp2015.AmazingRace.Base.Models;
using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;
using Bootcamp2015.AmazingRace.Helpers;
using Bootcamp2015.AmazingRace.Views;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Activation;
using Windows.Storage.Pickers;

namespace Bootcamp2015.AmazingRace.ViewModels
{
    public class CluePageViewModel : Screen, IFileOpenPickerContinuable
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;
        private readonly ISettingsService _settingsService;

        public ICommand ViewMap { get; set; }
        public ICommand UploadPhoto { get; set; }
        public ICommand Skip { get; set; }

        public string ClueText { get; set; }

        public CluePageViewModel(INavigationService navigationService,
            IDataService dataService, ISettingsService settingsService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
            _settingsService = settingsService;

            ViewMap = new DelegateCommand(() => ShowMap());
            UploadPhoto = new DelegateCommand(() => OnFilePick());
            Skip = new DelegateCommand(() => GetNextClueAsync());

            _settingsService.SetValue<int>("INDEX", 0);
            InitRaceAsync();
            GetNextClueAsync();
        }

        private void OnFilePick()
        {
            var filePicker = new FileOpenPicker();
            filePicker.FileTypeFilter.Add("*");
            filePicker.ContinuationData["test"] = "this is me";

            filePicker.PickSingleFileAndContinue();
        }

        public void ContinueFileOpenPicker(FileOpenPickerContinuationEventArgs args)
        {
            //args.Files.Cound

            // POST photo
            //_dataService.SubmitClueResponseAsync();

            // Go back to leaderboards
            _navigationService.NavigateToViewModel<LeaderboardPageViewModel>();
        }

        private void ShowMap()
        {
            _navigationService.Navigate(typeof(MapPage));
        }

        private async void InitRaceAsync()
        {
            // Get & save the current race
            IEnumerable<Race> races = await _dataService.GetRacesAsync();
            Race thisRace = races.First<Race>();
            _settingsService.SetSerializedValue<Race>("RACE", thisRace);
        }

        /// <summary>
        /// Gets the next clue and displays its description to the user
        /// </summary>
        private async void GetNextClueAsync()
        {
            // Get the race and team for their IDs, and the skip index
            Race thisRace = _settingsService.GetDeserializedValueOrDefault<Race>("RACE");
            Team thisTeam = _settingsService.GetDeserializedValueOrDefault<Team>("TEAM");
            int index = _settingsService.GetValueOrDefault<int>("INDEX");

            // Update the team with the "NextClueId" field from the api call
            thisTeam = await _dataService.GetTeamAsync(thisRace.Id, thisTeam.Id, index);

            // Get the clue with "NextClueId" from the team
            Clue thisClue = await _dataService.GetClueAsync(thisTeam.NextClueId);

            // Update the clue and skip index
            _settingsService.SetSerializedValue<Clue>("CLUE", thisClue);
            _settingsService.SetValue<int>("INDEX", index + 1);

            // Update the view
            ClueText = thisClue.Description;
            NotifyOfPropertyChange(() => ClueText);
        }
    }
}
