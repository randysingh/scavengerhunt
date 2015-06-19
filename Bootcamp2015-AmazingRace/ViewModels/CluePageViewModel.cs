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
        private INavigationService _navigationService;
        private IDataService _dataService;

        public ICommand ViewMap { get; set; }
        public ICommand UploadPhoto { get; set; }
        public ICommand Skip { get; set; }

        public CluePageViewModel(INavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;

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
        }

        private void ShowMap()
        {
            _navigationService.Navigate(typeof(MapPage));
        }

        private void GetNextClue()
        {

        }
    }
}
