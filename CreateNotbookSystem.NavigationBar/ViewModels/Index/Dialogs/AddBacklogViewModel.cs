using CreateNotbookSystem.Common.DbContent.Dto;
using CreateNotbookSystem.NavigationBar.Commo;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.NavigationBar.ViewModels.Index.Dialogs
{
    public class AddBacklogViewModel : BindableBase, IDialogHostAware
    {
        #region 属性
        private BacklogDto backlog;
        /// <summary>
        /// 待办事项
        /// </summary>
        public BacklogDto Backlog
        {
            get => backlog;
            set
            {
                backlog = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 弹窗名字
        /// </summary>
        public string DialogHostName { get; set; }
        #endregion

        #region 命令
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        #endregion

        public AddBacklogViewModel()
        {
            SaveCommand = new DelegateCommand(Save);

            CancelCommand = new DelegateCommand(Cancel);
        }

        #region 方法
        /// <summary>
        /// 弹窗打开时
        /// </summary>
        /// <param name="parameters"></param>
        public void OnDialogOpend(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Value"))
            {
                Backlog = parameters.GetValue<BacklogDto>("Value");
            }
            else
            {
                Backlog = new BacklogDto();
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        private void Save()
        {
            if (string.IsNullOrWhiteSpace(Backlog.Title) && string.IsNullOrWhiteSpace(Backlog.Content))
            {
                return;
            }

            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                DialogParameters param = new DialogParameters();
                param.Add("Value", Backlog);
                //返回编辑实体并返回OK
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.OK, param));
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        private void Cancel()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                //取消返回No
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.No));
            }
        }
        #endregion

    }
}
