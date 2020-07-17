using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace Minesweeper.Core
{
    public class SaveGameViewModel : BindableBase, IDialogAware
    {
        public string Title => "Exit Game";

        public event Action<IDialogResult> RequestClose;

        public DelegateCommand SaveGameCommand => new DelegateCommand(delegate () 
        {
            RequestClose.Invoke(new DialogResult(ButtonResult.Yes));
        });

        public DelegateCommand NoSaveGameCommand => new DelegateCommand(delegate ()
        {
            RequestClose.Invoke(new DialogResult(ButtonResult.No));
        });

        public DelegateCommand ContinueGameCommand => new DelegateCommand(delegate() 
        {
            RequestClose.Invoke(new DialogResult(ButtonResult.Cancel));
        });

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
    }
}
