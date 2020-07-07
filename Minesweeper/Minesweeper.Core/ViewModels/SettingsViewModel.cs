using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

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

        #region Public Events

        public event Action<IDialogResult> RequestClose;

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

    }
}