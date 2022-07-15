using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.NavigationBar.Commo
{
    /// <summary>
    /// 自定义弹窗接口（默认IDialogService弹窗不支持附加载窗口上）
    /// </summary>
    public interface IDialogHostService : IDialogService
    {
        Task<IDialogResult> ShowDialogAsync(string name, IDialogParameters parameters, string dialogHostName = "Root");
    }
}
