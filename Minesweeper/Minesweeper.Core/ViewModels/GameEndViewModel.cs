using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace Minesweeper.Core
{
    /// <summary>
    /// ViewModel for the GameEnd dialog
    /// </summary>
    public class GameEndViewModel : BindableBase, IDialogAware
    {
        #region Private Members

        private string title; 
        private string winLoseMessage;
        private decimal timeTaken;
        private decimal bestTime;
        private int gamesPlayed;
        private int gamesWon;
        private int winPercentage;
        private string currentDate;

        #endregion

        #region Public Properties

        /// <summary>
        /// The Title of the Dialog
        /// </summary>
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        /// <summary>
        /// Stores the message for the win or lose
        /// </summary>
        public string WinLoseMessage
        {
            get { return winLoseMessage; }
            set { SetProperty(ref winLoseMessage, value); }
        }

        /// <summary>
        /// Stores the time spent by the user on the current game
        /// </summary>
        public decimal TimeTaken
        {
            get { return timeTaken; }
            set { SetProperty(ref timeTaken, value); }
        }

        /// <summary>
        /// Stores the shortest time taken by the user to clear the stage
        /// </summary>
        public decimal BestTime
        {
            get { return bestTime; }
            set { SetProperty(ref bestTime, value); }
        }

        /// <summary>
        /// Stores the number of total games played till now
        /// </summary>
        public int GamesPlayed
        {
            get { return gamesPlayed; }
            set { SetProperty(ref gamesPlayed, value); }
        }

        /// <summary>
        /// Stores the number of total games won till now
        /// </summary>
        public int GamesWon
        {
            get { return gamesWon; }
            set { SetProperty(ref gamesWon, value); }
        }
        

        /// <summary>
        /// Win percentage on the current difficulty
        /// </summary>
        public int WinPercentage
        {
            get { return winPercentage; }
            set { SetProperty(ref winPercentage, value); }
        }
        
        /// <summary>
        /// Stores the date on which  this dialog was opened
        /// </summary>
        public string CurrentDate
        {
            get { return currentDate; }
            set { SetProperty(ref currentDate, value); }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public GameEndViewModel()
        {
            DateTime date = DateTime.Now;
            CurrentDate = $"{date.Day}/{date.Month}/{date.Year}";
        }

        #endregion

        #region Public DelegateCommands

        public DelegateCommand PlayAgainCommand => new DelegateCommand(PlayAgain);

        public DelegateCommand CancelCommand => new DelegateCommand(QuitGame);

        #endregion

        #region Public Events

        /// <summary>
        /// Requests to close the dialog
        /// </summary>
        public event Action<IDialogResult> RequestClose;

        #endregion

        #region DialogMethods

        /// <summary>
        /// Checks if the dialog can be closed
        /// </summary>
        /// <returns></returns>
        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        /// <summary>
        /// uses the parameters that were sent
        /// </summary>
        /// <param name="parameters"></param>
        public void OnDialogOpened(IDialogParameters parameters)
        {
            var isWin = Convert.ToBoolean(parameters.GetValue<string>("isWin"));

            if (!isWin)
            {
                Title = "You Lost";
                WinLoseMessage = "You Lost! Better luck next time.";
            }
            else
            {
                Title = "You Won";
                WinLoseMessage = "Congratulations, you won the game";
            }
        }

        #endregion

        #region Event Methods

        /// <summary>
        /// Closes the dialog and sends the callback to the game to play again
        /// </summary>
        private void PlayAgain()
        {
            RequestClose.Invoke(new DialogResult(ButtonResult.OK));
        }

        /// <summary>
        /// Closes the dialog and sends the callback to the game to quit the game
        /// </summary>
        private void QuitGame()
        {
            RequestClose.Invoke(new DialogResult(ButtonResult.Abort));
        }

        #endregion
    }
}