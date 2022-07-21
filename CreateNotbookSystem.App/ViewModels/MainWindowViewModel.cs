using CreateNotbookSystem.App.Common;
using CreateNotbookSystem.Common.DbContent;
using CreateNotbookSystem.Common.Models;
using CreateNotbookSystem.Common.Models.Managers;
using CreateNotbookSystem.NavigationBar.Event;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.App.ViewModels
{
    public class MainWindowViewModel : BindableBase, IConfigureService
    {
        #region 属性
        private ObservableCollection<MenuBarModel>  menuBars;

        /// <summary>
        /// 左侧菜单集合
        /// </summary>
        public ObservableCollection<MenuBarModel> MenuBars
        {
            get => menuBars;
            set
            {
                menuBars = value;
                RaisePropertyChanged();
            }
        }

        private bool isChecked;
        /// <summary>
        /// 是否点击了按钮(此属性用两个作用，第一是判断togglebutton是否被点击，第二个是控制侧边栏的显示隐藏)
        /// </summary>
        public bool IsChecked
        {
            get => isChecked;
            set
            {
                isChecked = value;
                RaisePropertyChanged();
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

        private string name;
        /// <summary>
        /// 获取登录的用户名
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                name = value;
                RaisePropertyChanged();
            }
        }


        #endregion

        #region 字段
        /// <summary>
        /// 导航接口
        /// </summary>
        private readonly IRegionManager regionManager;

        /// <summary>
        /// 导航日志
        /// </summary>
        private IRegionNavigationJournal journal;

        /// <summary>
        /// 容器
        /// </summary>
        private readonly IContainerProvider container;

        /// <summary>
        /// 事件聚合器
        /// </summary>
        private readonly IEventAggregator aggregator;
        #endregion

        #region 命令
        /// <summary>
        /// 下一步
        /// </summary>
        public DelegateCommand GoForwardCommand { get; private set; }

        /// <summary>
        /// 上一步
        /// </summary>
        public DelegateCommand GoBackCommand { get; private set; }

        /// <summary>
        /// 返回首页
        /// </summary>
        public DelegateCommand HomeCommand { get; private set; }

        /// <summary>
        /// 退出当前账号
        /// </summary>
        public DelegateCommand ExitCommand { get; private set; }
        #endregion

        public MainWindowViewModel(IRegionManager regionManager, IContainerProvider container)
        {
            MenuBars = new ObservableCollection<MenuBarModel>();

            GoForwardCommand = new DelegateCommand(GoForward);
            GoBackCommand = new DelegateCommand(GoBack);
            HomeCommand = new DelegateCommand(Home);
            ExitCommand = new DelegateCommand(Exit);

            this.aggregator = container.Resolve<IEventAggregator>();

            aggregator.GetEvent<UserNameEvent>().Subscribe(GetName);

            this.regionManager = regionManager;
            this.container = container;
            this.IsChecked = false;
        }

        #region 方法
        /// <summary>
        /// 导航栏初始化
        /// </summary>
        private void CreateMenuBars()
        {
            MenuBars.Add(new MenuBarModel()
            {
                Icon = "HomeAssistant",
                Title = "首页",
                NameSpace = "IndexView"
            });
            MenuBars.Add(new MenuBarModel()
            {
                Icon = "Notebook",
                Title = "待办事项",
                NameSpace = "BacklogView"
            });
            MenuBars.Add(new MenuBarModel()
            {
                Icon = "FileDocument",
                Title = "备忘录",
                NameSpace = "MemoView"
            });
            MenuBars.Add(new MenuBarModel()
            {
                Icon = "CogOutline",
                Title = "设置",
                NameSpace = "SettingsView"
            });
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
            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(obj.NameSpace, callback =>
            {
                if ((bool)callback.Result)
                {
                    journal = callback.Context.NavigationService.Journal;
                }
            });

            IsChecked = false;//隐藏侧边栏
        }

        /// <summary>
        /// 前进
        /// </summary>
        private void GoForward()
        {
            if (journal != null && journal.CanGoForward)
            {
                journal.GoForward();
            }
        }

        /// <summary>
        /// 后退
        /// </summary>
        private void GoBack()
        {

            if (journal != null && journal.CanGoBack)
            {
                journal.GoBack();
            }
        }

        /// <summary>
        /// 返回首页
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void Home()
        {
            var result = MenuBars.FirstOrDefault(x => x.Title == "首页");
            Navigate(result);
            SelectedItem = null;
        }

        /// <summary>
        /// 配置首页初始化参数
        /// </summary>
        public void Configure()
        {
            CreateMenuBars();

            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate("IndexView");
        } 

        /// <summary>
        /// 退出当前账号
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void Exit()
        {
            App.ExitAccout(container);
        }

        /// <summary>
        /// 登录成功获取用户名
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void GetName(string obj)
        {
            Name = obj;
        }
        #endregion

    }
}
