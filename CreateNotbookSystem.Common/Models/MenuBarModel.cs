using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.Common.Models
{
    /// <summary>
    /// 左侧菜单对象
    /// </summary>
    public class MenuBarModel : BindableBase
    {
        private string icon;
        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon
        {
            get => icon;
            set
            {
                icon = value;
                RaisePropertyChanged();
            }
        }

        private string title;
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Title
        {
            get => title;
            set
            {
                title = value;
                RaisePropertyChanged();
            }
        }

        private string nameSpace;
        /// <summary>
        /// 菜单命名空间
        /// </summary>
        public string NameSpace
        {
            get => nameSpace;
            set
            {
                nameSpace = value;
                RaisePropertyChanged();
            }
        }

    }
}
