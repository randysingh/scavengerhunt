﻿using Bootcamp2015.AmazingRace.Common;
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

        #region Properties and Members

        private INavigationService _navigationService;

        // Underlying clue object
        private Clue _clue;
        public Clue Clue
        {
            get { return _clue; }
            set { 
                _clue = value;
                NotifyOfPropertyChange();  // When the clue is changed update all the views
            }
        }

        #endregion

        #region Commands

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

        #endregion

        #region Constructor

        public CluePageViewModel (INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        #endregion

        #region Methods

        public void ProcessPayload(Clue payload)
        {
            // here is your received item
            Clue = payload; //save payload
        }

        #region FilePicker Methods

        protected void OnTakePicture()
        {
            var filePicker = new FileOpenPicker();
            filePicker.FileTypeFilter.Add("*");
            filePicker.ContinuationData["test"] = "this is me";
            filePicker.PickSingleFileAndContinue();
        }

        // Called after user selects a file
        public void ContinueFileOpenPicker(FileOpenPickerContinuationEventArgs args)
        {           
            Clue newClue = new Clue() { Description = args.Files.First<StorageFile>().Name };
            Clue = newClue;
        }

        #endregion

        protected void OnGotoCluePage()
        {
            _navigationService.NavigateToViewModel<CluePageViewModel>();
        }

        #endregion
    }
}
