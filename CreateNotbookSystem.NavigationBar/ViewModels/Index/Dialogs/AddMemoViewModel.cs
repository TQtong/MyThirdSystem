using CreateNotbookSystem.NavigationBar.Commo;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.NavigationBar.ViewModels.Index.Dialogs
{
    public class AddMemoViewModel : IDialogHostAware
    {
        public string DialogHostName { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public AddMemoViewModel()
        {
            SaveCommand = new DelegateCommand(Save);

            CancelCommand = new DelegateCommand(Cancel);
        }

        #region 方法
        public void OnDialogOpend(IDialogParameters parameters)
        {
        }

        private void Save()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                DialogParameters param = new DialogParameters();
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.OK, param));
            }
        }

        private void Cancel()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.No));
            }
        }
        #endregion
    }
}
