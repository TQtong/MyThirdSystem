using CreateNotbookSystem.Common.DbContent.Dto;
using CreateNotbookSystem.Common.Models;
using CreateNotbookSystem.NavigationBar.Service;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.NavigationBar.ViewModels.Backlog
{
    public class BacklogViewModel : NavigationBaseViewModel
    {
        #region 属性
        /// <summary>
        /// 显示隐藏侧边内容
        /// </summary>
        public DelegateCommand OpenSideWindow { get; private set; }

        private ObservableCollection<BacklogDto> backlogModels;
        /// <summary>
        /// 待办事项
        /// </summary>
        public ObservableCollection<BacklogDto> BacklogModels
        {
            get => backlogModels;
            set
            {
                backlogModels = value;
                RaisePropertyChanged();
            }
        }

        private bool isRightDrawerOpen;

        /// <summary>
        /// 是否显示侧边栏
        /// </summary>
        public bool IsRightDrawerOpen
        {
            get => isRightDrawerOpen;
            set
            {
                isRightDrawerOpen = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 字段
        private readonly IBacklogService service;
        #endregion

        #region 构造函数
        public BacklogViewModel(IBacklogService service, IContainerProvider container) : base(container)
        {
            OpenSideWindow = new DelegateCommand(Open);
            BacklogModels = new ObservableCollection<BacklogDto>();
            this.service = service;
        }


        #endregion

        #region 方法
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            GetDataAsync();
        }

        /// <summary>
        /// 获取数据库中待办事项数据
        /// </summary>
        private async void GetDataAsync()
        {
            UpDateLoading(true);


            BacklogModels.Clear();

            var backlog = await service.GetAllAsync(new Common.Parameter.QueryParameter()
            {
                PageIndex = 0,
                PageSize = 100,
                Search = "string"
            });

            if (backlog.Status)
            {
                foreach (var item in backlog.Result.Items)
                {
                    BacklogModels.Add(item);
                }
            }

            UpDateLoading(false);

        }

        /// <summary>
        /// 显示隐藏侧边内容
        /// </summary>
        private void Open()
        {

            IsRightDrawerOpen = true;

        }

        #endregion




    }
}
