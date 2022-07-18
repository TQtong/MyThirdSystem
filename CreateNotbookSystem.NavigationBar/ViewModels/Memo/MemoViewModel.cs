using CreateNotbookSystem.Common.DbContent.Dto;
using CreateNotbookSystem.Common.Models;
using CreateNotbookSystem.NavigationBar.Commo;
using CreateNotbookSystem.NavigationBar.Extensions;
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

namespace CreateNotbookSystem.NavigationBar.ViewModels.Memo
{
    public class MemoViewModel : NavigationBaseViewModel
    {
        #region 属性
        private ObservableCollection<MemoDto> memos;
        /// <summary>
        /// 待办事项
        /// </summary>
        public ObservableCollection<MemoDto> Memos
        {
            get => memos;
            set
            {
                memos = value;
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

        private MemoDto currentDto;
        /// <summary>
        /// 编辑选中/新增时对象
        /// </summary>
        public MemoDto CurrentDto
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
        #endregion

        #region 字段
        private readonly IMemoService service;

        private readonly IDialogHostService dialog;
        #endregion

        #region 命令

        /// <summary>
        /// 命令集合体
        /// </summary>
        public DelegateCommand<string> ExecuteCommand { get; private set; }

        /// <summary>
        /// 选择待办事项
        /// </summary>
        public DelegateCommand<MemoDto> SelectedCommand { get; private set; }

        /// <summary>
        /// 删除选中的待办事项
        /// </summary>
        public DelegateCommand<MemoDto> DeletedCommand { get; private set; }
        #endregion

        #region 构造函数
        public MemoViewModel(IMemoService service, IContainerProvider container) : base(container)
        {
            Memos = new ObservableCollection<MemoDto>();

            ExecuteCommand = new DelegateCommand<string>(Execute);
            SelectedCommand = new DelegateCommand<MemoDto>(Select);
            DeletedCommand = new DelegateCommand<MemoDto>(Delete);

            dialog = container.Resolve<IDialogHostService>();
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
            CurrentDto = new MemoDto();
            IsRightDrawerOpen = true;
        }

        /// <summary>
        /// 获取数据库中待办事项数据
        /// </summary>
        private async void GetDataAsync()
        {
            UpdateLoading(true);

            Memos.Clear();


            var backlog = await service.GetAllAsync(new Common.Parameter.QueryParameter()
            {
                PageIndex = 0,
                PageSize = 100,
                Search = Search,
            });

            if (backlog.Status)
            {
                foreach (var item in backlog.Result.Items)
                {
                    Memos.Add(item);
                }
            }

            UpdateLoading(false);
            Search = null;
        }

        /// <summary>
        /// 选择待办事项数据
        /// </summary>
        /// <param name="obj"></param>
        private async void Select(MemoDto obj)
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

        /// <summary>
        /// 添加备完录
        /// </summary>
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
                        var backlog = Memos.FirstOrDefault(t => t.Id == CurrentDto.Id);
                        if (backlog != null)
                        {
                            backlog.Title = CurrentDto.Title;
                            backlog.Content = CurrentDto.Content;
                        }
                    }
                    IsRightDrawerOpen = false;
                }
                else
                {
                    var result = await service.AddAsync(CurrentDto);
                    if (result.Status)
                    {
                        Memos.Add(result.Result);
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
        /// 删除选中的备完录
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private async void Delete(MemoDto obj)
        {
            var dialogResult = await dialog.Question("温馨提示", $"确认删除 ?");
            if (dialogResult.Result != Prism.Services.Dialogs.ButtonResult.OK)
            {
                return;
            }

            var result = await service.DeleteAsync(obj.Id);
            if (result.Status)
            {
                var backlog = Memos.FirstOrDefault(t => t.Id == obj.Id);
                if (backlog != null)
                {
                    Memos.Remove(backlog);
                }
            }
        }

        #endregion
    }
}
