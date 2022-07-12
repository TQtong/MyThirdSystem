using CreateNotbookSystem.Common.Models;
using CreateNotbookSystem.Common.Models.Managers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.NavigationBar.ViewModels.Settings
{
    public class SettingsViewModel : BindableBase
    {
        #region 属性
        private ObservableCollection<MenuBarModel> menuBars;
        /// <summary>
        /// 菜单栏
        /// </summary>
        public ObservableCollection<MenuBarModel> MenuBarModels
        {
            get { return menuBars; }
            set 
            { 
                menuBars = value; RaisePropertyChanged(); 
            }
        }

        private MenuBarModel selectedItem;
        /// <summary>
        /// 用户选择的菜单内容
        /// </summary>
        public MenuBarModel SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                RaisePropertyChanged();
                if (selectedItem != null)
                {
                    Navigate(SelectedItem);
                }
            }
        }
        #endregion

        #region 字段
        /// <summary>
        /// 导航接口
        /// </summary>
        private readonly IRegionManager regionManager;
        #endregion

        #region 命令

        #endregion

        #region 构造函数
        public SettingsViewModel(IRegionManager regionManager)
        {
            MenuBarModels = new ObservableCollection<MenuBarModel>();
            this.regionManager = regionManager;
            CreateMenuBar();
        }

        #endregion
        #region 方法
        void CreateMenuBar()
        {
            MenuBarModels.Add(new MenuBarModel() { Icon = "Palette", Title = "个性化", NameSpace = "IndividuationView" });
            MenuBarModels.Add(new MenuBarModel() { Icon = "Cog", Title = "系统设置", NameSpace = "SystemSettingsView" });
            MenuBarModels.Add(new MenuBarModel() { Icon = "Information", Title = "关于更多", NameSpace = "AboutView" });
        }

        /// <summary>
        /// 切换导航栏
        /// </summary>
        /// <param name="obj"></param>
        private void Navigate(MenuBarModel obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.NameSpace))
            {
                return;
            }

            regionManager.Regions[PrismManager.SettingsViewRegionName].RequestNavigate(obj.NameSpace);
        }
        #endregion

    }
}
