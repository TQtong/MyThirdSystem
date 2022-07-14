using CreateNotbookSystem.Common.Models;
using CreateNotbookSystem.NavigationBar.Extensions;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.NavigationBar.ViewModels
{
    public class NavigationBaseViewModel : BindableBase, INavigationAware
    {
        private readonly IContainerProvider container;

        public readonly IEventAggregator aggregator;

        public NavigationBaseViewModel(IContainerProvider container)
        {
            this.container = container;
            aggregator = container.Resolve<IEventAggregator>();
        }

        /// <summary>
        /// 每次重新导航的时候，是否重用之前的实例：true 重用.
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns></returns>
        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        /// <summary>
        /// 切换导航栏时，判断是否可以切换：true 可以.
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <param name="continuationCallback"></param>
        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        /// <summary>
        /// 接收点击导航传过来的参数
        /// </summary>
        /// <param name="navigationContext"></param>
        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        /// <summary>
        /// 通知窗口的状态
        /// </summary>
        /// <param name="IsOpen"></param>
        public void UpdateLoading(bool IsOpen)
        {
            aggregator.UpdateLoading(new UpdateModel()
            {
                IsOpen = IsOpen
            });
        }
    }
}
