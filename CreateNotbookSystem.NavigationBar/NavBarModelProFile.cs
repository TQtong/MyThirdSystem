using CreateNotbookSystem.NavigationBar.ViewModels;
using CreateNotbookSystem.NavigationBar.ViewModels.Backlog;
using CreateNotbookSystem.NavigationBar.ViewModels.Index;
using CreateNotbookSystem.NavigationBar.ViewModels.Index.Dialogs;
using CreateNotbookSystem.NavigationBar.ViewModels.Memo;
using CreateNotbookSystem.NavigationBar.ViewModels.Settings;
using CreateNotbookSystem.NavigationBar.Views;
using CreateNotbookSystem.NavigationBar.Views.Backlog;
using CreateNotbookSystem.NavigationBar.Views.Index;
using CreateNotbookSystem.NavigationBar.Views.Index.Dialogs;
using CreateNotbookSystem.NavigationBar.Views.Memo;
using CreateNotbookSystem.NavigationBar.Views.Settings;
using Prism.Ioc;
using Prism.Modularity;

namespace CreateNotbookSystem.NavigationBar
{
    public class NavBarModelProFile : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //左侧边栏导航
            containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>();
            containerRegistry.RegisterForNavigation<BacklogView, BacklogViewModel>();
            containerRegistry.RegisterForNavigation<MemoView, MemoViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();

            //设置中的侧边导航栏
            containerRegistry.RegisterForNavigation<IndividuationView, IndividuationViewModel>();
            containerRegistry.RegisterForNavigation<SystemSettingsView, SystemSettingsViewModel>();
            containerRegistry.RegisterForNavigation<AboutView, AboutViewModel>();

            //注册弹窗（自定义弹窗，只要添加到容器里就行，不管用什么方法）
            containerRegistry.RegisterForNavigation<AddBacklogView, AddBacklogViewModel>();
            containerRegistry.RegisterForNavigation<AddMemoView, AddMemoViewModel>();
            containerRegistry.RegisterForNavigation<MessageView, MessageViewModel>();
        }
    }
}
