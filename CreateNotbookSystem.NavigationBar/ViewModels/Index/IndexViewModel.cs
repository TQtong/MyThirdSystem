using CreateNotbookSystem.Common.DbContent.Dto;
using CreateNotbookSystem.Common.Models;
using CreateNotbookSystem.NavigationBar.Commo;
using CreateNotbookSystem.NavigationBar.Service;
using CreateNotbookSystem.NavigationBar.ViewModels.BaseViewModels;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.NavigationBar.ViewModels.Index
{
    public class IndexViewModel : NavigationBaseViewModel
    {
        #region 属性
        private ObservableCollection<TaskBarModel> taskBarModels;
        /// <summary>
        /// 任务栏
        /// </summary>
        public ObservableCollection<TaskBarModel> TaskBarModels
        {
            get => taskBarModels;
            set
            {
                taskBarModels = value;
                RaisePropertyChanged();
            }
        }

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

        private ObservableCollection<MemoDto> memos;

        /// <summary>
        /// 备忘录
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
        #endregion

        #region 字段
        /// <summary>
        /// 弹窗接口
        /// </summary>
        private readonly IDialogHostService dialog;

        /// <summary>
        /// 待办事项服务
        /// </summary>
        private readonly IBacklogService backlogService;

        /// <summary>
        /// 备忘录服务
        /// </summary>
        private readonly IMemoService memoService;
        #endregion

        #region 命令
        /// <summary>
        /// 命令集合体
        /// </summary>
        public DelegateCommand<string> ExecuteCommand { get; private set; }
        #endregion

        #region 构造函数
        public IndexViewModel(IDialogHostService dialog, IContainerProvider container) : base(container)
        {
            TaskBarModels = new ObservableCollection<TaskBarModel>();
            Backlogs = new ObservableCollection<BacklogDto>();
            Memos = new ObservableCollection<MemoDto>();

            ExecuteCommand = new DelegateCommand<string>(Execute);

            this.backlogService = container.Resolve<IBacklogService>();
            this.memoService = container.Resolve<IMemoService>();

            this.dialog = dialog;

            CreateTaskBar();
        }


        #endregion

        #region 方法
        /// <summary>
        /// 创建任务栏
        /// </summary>
        private void CreateTaskBar()
        {
            TaskBarModels.Add(new TaskBarModel() { Color = "#FF0CA0FF", Icon = "ClockFast", Title = "汇总", Number = "1", NameSpace = "" });
            TaskBarModels.Add(new TaskBarModel() { Color = "#FF1ECA3A", Icon = "ClockCheckOutline", Title = "完成", Number = "2", NameSpace = "" });
            TaskBarModels.Add(new TaskBarModel() { Color = "#FF02C6DC", Icon = "ChartLineVariant", Title = "完成比例", Number = "97%", NameSpace = "" });
            TaskBarModels.Add(new TaskBarModel() { Color = "#FFFFA000", Icon = "PlaylistStar", Title = "备忘录", Number = "4", NameSpace = "" });
        }

        /// <summary>
        /// 命令集合体
        /// </summary>
        private void Execute(string obj)
        {
            switch (obj)
            {
                case "新增待办":
                    AddBacklog();
                    break;
                case "新增备忘录":
                    AddMemo(); 
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 添加待办事项
        /// </summary>
        private async void AddBacklog()
        {
            var dialogResult = await dialog.ShowDialogAsync("AddBacklogView", null);

            if (dialogResult.Result == ButtonResult.OK)
            {
                var backlog = dialogResult.Parameters.GetValue<BacklogDto>("Value");
                if (backlog.Id > 0)
                {

                }
                else
                {
                    var result = await backlogService.AddAsync(backlog);

                    if (result.Status)
                    {
                        Backlogs.Add(backlog);
                    }
                }
            }
        }

        /// <summary>
        /// 添加备忘录
        /// </summary>
        private async void AddMemo()
        {
            var dialogResult = await dialog.ShowDialogAsync("AddMemoView", null);

            if (dialogResult.Result == ButtonResult.OK)
            {
                var memo = dialogResult.Parameters.GetValue<MemoDto>("Value");
                if (memo.Id > 0)
                {

                }
                else
                {
                    var result = await memoService.AddAsync(memo);

                    if (result.Status)
                    {
                        Memos.Add(memo);
                    }
                }
            }
        }
        #endregion



    }
}
