using CreateNotbookSystem.NavigationBar.ViewModels.Backlog;
using CreateNotbookSystem.NavigationBar.ViewModels.Index;
using CreateNotbookSystem.NavigationBar.ViewModels.Memo;
using CreateNotbookSystem.NavigationBar.ViewModels.Settings;
using CreateNotbookSystem.NavigationBar.Views.Backlog;
using CreateNotbookSystem.NavigationBar.Views.Index;
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
            containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>();
            containerRegistry.RegisterForNavigation<BacklogView, BacklogViewModel>();
            containerRegistry.RegisterForNavigation<MemoView, MemoViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
        }
    }
}
