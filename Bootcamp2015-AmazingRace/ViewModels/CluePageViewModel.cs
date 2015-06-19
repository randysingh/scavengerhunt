using Bootcamp2015.AmazingRace.Common;
using Bootcamp2015.AmazingRace.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace Bootcamp2015.AmazingRace.ViewModels
{
    public class CluePageViewModel : Screen, IParameterReceivable<Clue>, Helpers.IFileOpenPickerContinuable
    {
        private INavigationService _navigationService;

        private Clue _clue;
        public Clue Clue
        {
            get { return _clue; }
            set { 
                _clue = value;
                NotifyOfPropertyChange();
            }
        }

        public ICommand TakePictureCommand
        {
            get
            {
                return new DelegateCommand(o => OnTakePicture());
            }
        }

        public ICommand GotoClueCommand
        {
            get
            {
               return  new DelegateCommand(o => OnGotoCluePage());
            }
        }


        public CluePageViewModel (INavigationService navigationService)
        {
            _navigationService = navigationService;

            //GotoJoinTeamCommand = new DelegateCommand(o => OnGotoJoinTeamPage());
        }

        protected void OnGotoCluePage()
        {
            _navigationService.NavigateToViewModel<CluePageViewModel>();
        }

        public void ProcessPayload(Clue payload)
        {
            // here is your received item
            Clue = payload; //save payload
        }



        protected void OnTakePicture()
        {
            var filePicker = new FileOpenPicker();
            filePicker.FileTypeFilter.Add("*");
            filePicker.ContinuationData["test"] = "this is me";

            filePicker.PickSingleFileAndContinue();
        }

        public void ContinueFileOpenPicker(FileOpenPickerContinuationEventArgs args)
        {
            
            Clue newClue = new Clue() { Description = args.Files.First<StorageFile>().Name };
            Clue = newClue;
            
        }
    }
}
