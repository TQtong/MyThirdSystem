using CreateNotbookSystem.Common.DbContent.Dto;
using CreateNotbookSystem.Common.Models;
using CreateNotbookSystem.NavigationBar.Service;
using CreateNotbookSystem.NavigationBar.ViewModels.BaseViewModels;
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
using System.Windows;

namespace CreateNotbookSystem.NavigationBar.ViewModels.Backlog
{
    public class BacklogViewModel : NavigationBaseViewModel
    {
        #region 属性
        private ObservableCollection<BacklogDto> backlogs;
        /// <summary>
        /// 待办事项
        /// </summary>
        public ObservableCollection<BacklogDto> Backlogs
        {
            get => backlogs;
            set
            {
                backlogs = value;
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

        private BacklogDto currentDto;
        /// <summary>
        /// 编辑选中/新增时对象
        /// </summary>
        public BacklogDto CurrentDto
        {
            get => currentDto;
            set
            {
                currentDto = value;
                RaisePropertyChanged();
            }
        }

        private string search;
        /// <summary>
        /// 搜索条件
        /// </summary>
        public string Search
        {
            get => search;
            set
            {
                search = value;
                RaisePropertyChanged();
            }
        }

        private int selectedIndex;
        /// <summary>
        /// 下拉列表选中状态值
        /// </summary>
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                RaisePropertyChanged();
            }
        }


        #endregion

        #region 字段
        private readonly IBacklogService service;
        #endregion

        #region 命令

        /// <summary>
        /// 命令集合体
        /// </summary>
        public DelegateCommand<string> ExecuteCommand { get; private set; }

        /// <summary>
        /// 选择待办事项
        /// </summary>
        public DelegateCommand<BacklogDto> SelectedCommand { get; private set; }

        /// <summary>
        /// 删除选中的待办事项
        /// </summary>
        public DelegateCommand<BacklogDto> DeletedCommand { get; private set; }
        #endregion

        #region 构造函数
        public BacklogViewModel(IBacklogService service, IContainerProvider container) : base(container)
        {
            Backlogs = new ObservableCollection<BacklogDto>();

            ExecuteCommand = new DelegateCommand<string>(Execute);
            SelectedCommand = new DelegateCommand<BacklogDto>(Select);
            DeletedCommand = new DelegateCommand<BacklogDto>(Delete);

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
        /// 命令集合体
        /// </summary>
        /// <param name="obj"></param>
        private void Execute(string obj)
        {
            switch (obj)
            {
                case "新增": Add(); break;
                case "查询": GetDataAsync(); break;
                case "保存": Save(); break;
            }
        }



        /// <summary>
        /// 添加待办
        /// </summary>
        private void Add()
        {
            CurrentDto = new BacklogDto();
            IsRightDrawerOpen = true;
        }

        /// <summary>
        /// 获取数据库中待办事项数据
        /// </summary>
        private async void GetDataAsync()
        {
            UpdateLoading(true);

            Backlogs.Clear();

            int? status = SelectedIndex == 0 ? null : SelectedIndex == 2 ? 1 : 0;

            var backlog = await service.GetAllFilterAsync(new Common.Parameter.BacklogQueryParameter()
            {
                PageIndex = 0,
                PageSize = 100,
                Search = Search,
                Status = status
            });

            if (backlog.Status)
            {
                foreach (var item in backlog.Result.Items)
                {
                    Backlogs.Add(item);
                }
            }

            UpdateLoading(false);
            Search = null;
        }

        /// <summary>
        /// 选择待办事项数据
        /// </summary>
        /// <param name="obj"></param>
        private async void Select(BacklogDto obj)
        {
            try
            {
                UpdateLoading(true);
                var backlog = await service.GetFirstOfDefaultAsync(obj.Id);
                if (backlog.Status)
                {
                    CurrentDto = backlog.Result;
                    IsRightDrawerOpen = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                UpdateLoading(false);
            }
        }

        private async void Save()
        {
            if (string.IsNullOrWhiteSpace(CurrentDto.Title) ||
                string.IsNullOrWhiteSpace(CurrentDto.Content))
            {
                return;
            }

            UpdateLoading(true);

            try
            {
                if (CurrentDto.Id > 0)
                {
                    var result = await service.UpdateAsync(CurrentDto);
                    if (result.Status)
                    {
                        var backlog = Backlogs.FirstOrDefault(t => t.Id == CurrentDto.Id);
                        if (backlog != null)
                        {
                            backlog.Title = CurrentDto.Title;
                            backlog.Content = CurrentDto.Content;
                            backlog.Status = CurrentDto.Status;
                        }
                    }
                    IsRightDrawerOpen = false;
                }
                else
                {
                    var result = await service.AddAsync(CurrentDto);
                    if (result.Status)
                    {
                        Backlogs.Add(result.Result);
                        IsRightDrawerOpen = false;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                UpdateLoading(false);
            }
        }

        /// <summary>
        /// 删除选中的待办事项
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private async void Delete(BacklogDto obj)
        {
            var result = await service.DeleteAsync(obj.Id);
            if (result.Status)
            {
                var backlog = Backlogs.FirstOrDefault(t => t.Id == obj.Id);
                if (backlog != null)
                {
                    Backlogs.Remove(backlog);
                }
            }
        }

        #endregion
    }
}
