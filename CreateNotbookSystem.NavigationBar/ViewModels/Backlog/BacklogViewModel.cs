using CreateNotbookSystem.Common.DbContent.Dto;
using CreateNotbookSystem.Common.Models;
using CreateNotbookSystem.NavigationBar.Service;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.NavigationBar.ViewModels.Backlog
{
    public class BacklogViewModel : BindableBase
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
        public BacklogViewModel(IBacklogService service)
        {
            OpenSideWindow = new DelegateCommand(Open);
            BacklogModels = new ObservableCollection<BacklogDto>();
            this.service = service;
            CreateTaskBar();
        }

        #endregion

        #region 方法
        private async void CreateTaskBar()
        {
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
