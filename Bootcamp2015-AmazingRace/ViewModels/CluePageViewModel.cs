using Bootcamp2015.AmazingRace.Base;
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

        public CluePageViewModel(INavigationService navigationService,
            IDataService dataService, ISettingsService settingsService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
            _settingsService = settingsService;

            ViewMap = new DelegateCommand(() => ShowMap());
            UploadPhoto = new DelegateCommand(() => OnFilePick());
            Skip = new DelegateCommand(() => GetNextClue());
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

        private void GetNextClue()
        {
            // Change content
        }
    }
}
