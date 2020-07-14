using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Minesweeper.Core
{
    public class SettingsViewModel : BindableBase, IDialogAware
    {
        #region Private Members

        private bool continueSavedGames;
        private bool showTips;
        private bool saveOnExit;
        private bool playSounds;
        private bool displayAnimations;
        private bool allowQuestionMarks;

        #endregion

        #region Public Properties

        public ObservableCollection<DifficultyData> AllDifficulties { get; set; } = new ObservableCollection<DifficultyData>();

        /// <summary>
        /// Stores if the animation should be displayed
        /// </summary>
        public bool DisplayAnimations
        {
            get { return displayAnimations; }
            set { SetProperty(ref displayAnimations, value); }
        }

        /// <summary>
        /// Stores if the sounds should be played
        /// </summary>
        public bool PlaySounds
        {
            get { return playSounds; }
            set { SetProperty(ref playSounds, value); }
        }

        /// <summary>
        /// Stores if the game should always be saved on exit
        /// </summary>
        public bool SaveOnExit
        {
            get { return saveOnExit; }
            set { SetProperty(ref saveOnExit, value); }
        }

        /// <summary>
        /// Stores if the game should always continue old saved games
        /// </summary>
        public bool ContinueSavedGames
        {
            get { return continueSavedGames; }
            set { SetProperty(ref continueSavedGames, value); }
        }

        /// <summary>
        /// Stores if the game should show the tips to the player
        /// </summary>
        public bool ShowTips
        {
            get { return showTips; }
            set { SetProperty(ref showTips, value); }
        }

        /// <summary>
        /// Stores if question marks are allowed in the game
        /// </summary>
        public bool AllowQuestionMarks
        {
            get { return allowQuestionMarks; }
            set { SetProperty(ref allowQuestionMarks, value); }
        }

        public string Title { get; set; } = "Settings";

        #endregion

        #region Public DelegateCommands

        public DelegateCommand OKCommand => new DelegateCommand(SaveSettings);

        public DelegateCommand CancelCommand => new DelegateCommand(CloseDialogWithoutSave);

        #endregion

        #region Public Events

        /// <summary>
        /// Requests to close the dialog
        /// </summary>
        public event Action<IDialogResult> RequestClose;

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SettingsViewModel()
        {
            foreach(var difficulty in Settings.AllDifficulties)
            {
                this.AllDifficulties.Add(difficulty);
            }

            DisplayAnimations = Settings.DisplayAnimations;
            PlaySounds = Settings.PlaySounds;
            SaveOnExit = Settings.SaveOnExit;
            ContinueSavedGames = Settings.ContinueSavedGames;
            ShowTips = Settings.ShowTips;
            AllowQuestionMarks = Settings.AllowQuestionMark;
        }

        #endregion
        
        #region Dialog Methods

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }

        #endregion

        /// <summary>
        /// Closes the dialog without making any changes to the settings
        /// </summary>
        private void CloseDialogWithoutSave()
        {
            RequestClose.Invoke(new DialogResult(ButtonResult.Cancel));
        }

        /// <summary>
        /// Saves the settings
        /// </summary>
        private void SaveSettings()
        {
            Settings.SaveSettings(DisplayAnimations, PlaySounds, SaveOnExit, ContinueSavedGames, ShowTips, AllowQuestionMarks, AllDifficulties.Where(diff=> diff.IsSelectedDifficilty == true).ToList()[0].Level);

            RequestClose.Invoke(new DialogResult(ButtonResult.OK));
        }
    }
}