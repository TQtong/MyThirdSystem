using CreateNotbookSystem.App.Common;
using CreateNotbookSystem.App.Views;
using CreateNotbookSystem.CustomControl;
using CreateNotbookSystem.NavigationBar;
using CreateNotbookSystem.NavigationBar.Service;
using DryIoc;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CreateNotbookSystem.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        /// <summary>
        /// 启动窗口
        /// </summary>
        /// <returns></returns>
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindowView>();
        }

        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="containerRegistry"></param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //注册HttpRestClient
            //先拿到指定容器后注册服务，并对构造函数设置一个默认值
            containerRegistry.GetContainer().Register<HttpRestClient>(made: Parameters.Of.Type<string>(serviceKey: "webUrl"));
            //设置根路径
            containerRegistry.GetContainer().RegisterInstance(@"http://localhost:5296/", serviceKey: "webUrl");

            //注册客户端服务
            containerRegistry.Register<IBacklogService, BacklogService>();
            containerRegistry.Register<IMemoService, MemoService>();
        }

        /// <summary>
        /// 模块引入
        /// </summary>
        /// <returns></returns>
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<NavBarModelProFile>();
            moduleCatalog.AddModule<CustomModelProFile>();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected override void OnInitialized()
        {
            var service = App.Current.MainWindow.DataContext as IConfigureService;

            if (service != null)
            {
                service.Configure();
            }

            base.OnInitialized();
        }
    }
}
