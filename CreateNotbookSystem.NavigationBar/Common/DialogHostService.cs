﻿using MaterialDesignThemes.Wpf;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CreateNotbookSystem.NavigationBar.Commo
{
    /// <summary>
    /// 对话主机服务（自定义，可以附加在任何指定的dialoghost上弹窗窗口）
    /// </summary>
    public class DialogHostService : DialogService, IDialogHostService
    {
        private readonly IContainerExtension containerExtension;

        public DialogHostService(IContainerExtension containerExtension) : base(containerExtension)
        {
            this.containerExtension = containerExtension;
        }

        public async Task<IDialogResult> ShowDialogAsync(string name, IDialogParameters parameters, string dialogHostName = "Root")
        {
            if (parameters == null)
            {
                parameters = new DialogParameters();
            }

            //从容器当中去除弹出窗口的实例
            var content = containerExtension.Resolve<object>(name);

            //验证实例的有效性 
            if (!(content is FrameworkElement dialogContent))
            {
                throw new NullReferenceException("A dialog's content must be a FrameworkElement");
            }

            if (dialogContent is FrameworkElement view && view.DataContext is null && ViewModelLocator.GetAutoWireViewModel(view) is null)
            {
                ViewModelLocator.SetAutoWireViewModel(view, true);
            }

            if (!(dialogContent.DataContext is IDialogHostAware viewModel))
            {
                throw new NullReferenceException("A dialog's ViewModel must implement the IDialogAware interface");
            }

            viewModel.DialogHostName = dialogHostName;

            //md带的方法，用于生成弹窗
            DialogOpenedEventHandler eventHandler = (sender, eventArgs) =>
            {
                if (viewModel is IDialogHostAware aware)
                {
                    aware.OnDialogOpend(parameters);
                }
                eventArgs.Session.UpdateContent(content);
            };

            return (IDialogResult)await DialogHost.Show(dialogContent, viewModel.DialogHostName, eventHandler);
        }
    }
}