using CreateNotbookSystem.Common.Models;
using Prism.Commands;
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
    public class IndexViewModel : BindableBase
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

        private ObservableCollection<BacklogModel> backlogModels;
        /// <summary>
        /// 待办事项
        /// </summary>
        public ObservableCollection<BacklogModel> BacklogModels
        {
            get => backlogModels;
            set
            {
                backlogModels = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<MemoModel> memoModels;

        /// <summary>
        /// 备忘录
        /// </summary>
        public ObservableCollection<MemoModel> MemorModels
        {
            get => memoModels;
            set
            {
                memoModels = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 字段
        /// <summary>
        /// 弹窗接口
        /// </summary>
        private readonly IDialogService service;
        #endregion

        #region 命令
        /// <summary>
        /// 命令集合体
        /// </summary>
        public DelegateCommand<string> ExecuteCommand { get; private set; }
        #endregion

        #region 构造函数
        public IndexViewModel(IDialogService service)
        {
            TaskBarModels = new ObservableCollection<TaskBarModel>();
            BacklogModels = new ObservableCollection<BacklogModel>();
            MemorModels = new ObservableCollection<MemoModel>();

            ExecuteCommand = new DelegateCommand<string>(Execute);

            CreateTaskBar();
            this.service = service;
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
            service.ShowDialog("AddBacklogView");
        }

        /// <summary>
        /// 添加备忘录
        /// </summary>
        private async void AddMemo()
        {
            service.ShowDialog("AddMemoView");
        }
        #endregion



    }
}
