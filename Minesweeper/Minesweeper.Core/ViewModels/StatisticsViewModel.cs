using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace Minesweeper.Core
{
    /// <summary>
    /// ViewModel for the Statistics Window
    /// </summary>
    public class StatisticsViewModel : BindableBase, IDialogAware
    {
        #region Public Properties

        /// <summary>
        /// Title of the window
        /// </summary>
        public string Title { get; set; } = "Statistics";

        #endregion

        #region Public Events

        /// <summary>
        /// Requests to close the dialog
        /// </summary>
        public event Action<IDialogResult> RequestClose;

        #endregion

        #region Dialog Methods

        /// <summary>
        /// Checks if the dialog can be closed
        /// </summary>
        /// <returns></returns>
        public bool CanCloseDialog()
        {
            return true;
        }

        /// <summary>
        /// Runs after the dialog has been closed
        /// </summary>
        public void OnDialogClosed()
        {
        }

        /// <summary>
        /// runs after dialog has been opened
        /// </summary>
        /// <param name="parameters">the parameters passed in with the dialog</param>
        public void OnDialogOpened(IDialogParameters parameters)
        {
        }

        #endregion

    }
}
