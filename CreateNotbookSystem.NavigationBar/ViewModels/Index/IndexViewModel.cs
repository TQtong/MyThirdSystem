using CreateNotbookSystem.Common.DbContent.Dto;
using CreateNotbookSystem.Common.Models;
using CreateNotbookSystem.Common.Models.Managers;
using CreateNotbookSystem.NavigationBar.Commo;
using CreateNotbookSystem.NavigationBar.Event;
using CreateNotbookSystem.NavigationBar.Extensions;
using CreateNotbookSystem.NavigationBar.Service;
using CreateNotbookSystem.NavigationBar.ViewModels.BaseViewModels;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
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

        private SummaryModel summary;
        /// <summary>
        /// 汇总信息
        /// </summary>
        public SummaryModel Summary
        {
            get => summary;
            set
            {
                summary = value;
                RaisePropertyChanged();
            }
        }

        private string title;
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get => title;
            set
            {
                title = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 字段
        /// <summary>
        /// 弹窗接口(自定义)
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

        /// <summary>
        /// 导航管理
        /// </summary>
        private readonly IRegionManager regionManager;

        /// <summary>
        /// 定时器
        /// </summary>
        private System.Windows.Threading.DispatcherTimer timer;

        /// <summary>
        /// 事件聚合器
        /// </summary>
        private readonly IEventAggregator aggregator;
        #endregion

        #region 变量
        private string userName;
        #endregion

        #region 命令
        /// <summary>
        /// 命令集合体
        /// </summary>
        public DelegateCommand<string> ExecuteCommand { get; private set; }

        /// <summary>
        /// 双击编辑待办事项
        /// </summary>
        public DelegateCommand<BacklogDto> EditBacklogCommand { get; private set; }

        /// <summary>
        /// 双击编辑备忘录
        /// </summary>
        public DelegateCommand<MemoDto> EditMemoCommand { get; private set; }

        /// <summary>
        /// 点击togglebutton改变待办事项状态
        /// </summary>
        public DelegateCommand<BacklogDto> BacklogCompleteCommand { get; private set; }

        /// <summary>
        /// 右键删除备完录
        /// </summary>
        public DelegateCommand<MemoDto> DeleteMemoCommand { get; private set; }

        /// <summary>
        /// 点击任务栏导航到指定的窗口中
        /// </summary>
        public DelegateCommand<TaskBarModel> NavigateCommand { get; private set; }
        #endregion

        #region 构造函数
        public IndexViewModel(IContainerProvider container) : base(container)
        {

            timer = new System.Windows.Threading.DispatcherTimer();//创建定时器
            timer.Tick += Timer_Tick; ;//执行事件
            timer.Interval = new TimeSpan(0, 0, 0, 1);//1s执行
            timer.Start();//开启

            TaskBarModels = new ObservableCollection<TaskBarModel>();

            ExecuteCommand = new DelegateCommand<string>(Execute);
            EditBacklogCommand = new DelegateCommand<BacklogDto>(AddBacklog);
            EditMemoCommand = new DelegateCommand<MemoDto>(AddMemo);
            BacklogCompleteCommand = new DelegateCommand<BacklogDto>(Complete);
            DeleteMemoCommand = new DelegateCommand<MemoDto>(Delete);
            NavigateCommand = new DelegateCommand<TaskBarModel>(Navigate);

            this.backlogService = container.Resolve<IBacklogService>();
            this.memoService = container.Resolve<IMemoService>();
            this.regionManager = container.Resolve<IRegionManager>();
            this.dialog = container.Resolve<IDialogHostService>();
            this.aggregator = container.Resolve<IEventAggregator>();

            aggregator.GetEvent<UserNameEvent>().Subscribe(GetName);

            CreateTaskBar();
        }

        #endregion

        #region 方法
        /// <summary>
        /// 创建任务栏
        /// </summary>
        private void CreateTaskBar()
        {
            TaskBarModels.Add(new TaskBarModel() { Color = "#FF0CA0FF", Icon = "ClockFast", Title = "汇总", NameSpace = "BacklogView" });
            TaskBarModels.Add(new TaskBarModel() { Color = "#FF1ECA3A", Icon = "ClockCheckOutline", Title = "已完成", NameSpace = "BacklogView" });
            TaskBarModels.Add(new TaskBarModel() { Color = "#FF02C6DC", Icon = "ChartLineVariant", Title = "完成比例", NameSpace = "" });
            TaskBarModels.Add(new TaskBarModel() { Color = "#FFFFA000", Icon = "PlaylistStar", Title = "备忘录", NameSpace = "MemoView" });
        }

        /// <summary>
        /// 命令集合体
        /// </summary>
        private void Execute(string obj)
        {
            switch (obj)
            {
                case "新增待办":
                    AddBacklog(null);
                    break;
                case "新增备忘录":
                    AddMemo(null);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 添加待办事项
        /// </summary>
        private async void AddBacklog(BacklogDto obj)
        {
            DialogParameters pairs = new DialogParameters();

            if (obj != null)
            {
                pairs.Add("Value", obj);
            }

            var dialogResult = await dialog.ShowDialogAsync("AddBacklogView", pairs);

            try
            {
                UpdateLoading(true);
                if (dialogResult.Result == ButtonResult.OK)
                {
                    var backlog = dialogResult.Parameters.GetValue<BacklogDto>("Value");
                    if (backlog.Id > 0)
                    {
                        var result = await backlogService.UpdateAsync(backlog);

                        if (result.Status)
                        {
                            var backlogModel = Summary.BacklogList.ToList().FirstOrDefault(x => x.Id.Equals(backlog.Id));

                            if (backlogModel != null)
                            {
                                backlogModel.Title = backlog.Title;
                                backlogModel.Content = backlog.Content;
                            }
                        }
                    }
                    else
                    {
                        var result = await backlogService.AddAsync(backlog);

                        if (result.Status)
                        {
                            GetSummary();
                        }
                    }
                }
            }
            finally
            {
                UpdateLoading(false);
            }
        }

        /// <summary>
        /// 添加备忘录
        /// </summary>
        private async void AddMemo(MemoDto obj)
        {
            DialogParameters pairs = new DialogParameters();

            if (obj != null)
            {
                pairs.Add("Value", obj);
            }

            var dialogResult = await dialog.ShowDialogAsync("AddMemoView", pairs);
            try
            {
                UpdateLoading(true);

                if (dialogResult.Result == ButtonResult.OK)
                {
                    var memo = dialogResult.Parameters.GetValue<MemoDto>("Value");
                    if (memo.Id > 0)
                    {
                        var result = await memoService.UpdateAsync(memo);

                        if (result.Status)
                        {
                            var memoModel = Summary.MemoList.ToList().FirstOrDefault(x => x.Id.Equals(memo.Id));

                            if (memoModel != null)
                            {
                                memoModel.Title = memo.Title;
                                memoModel.Content = memo.Content;
                            }
                        }
                    }
                    else
                    {
                        var result = await memoService.AddAsync(memo);

                        if (result.Status)
                        {
                            GetSummary();
                        }
                    }
                }
            }
            finally
            {
                UpdateLoading(false);
            }
        }

        /// <summary>
        /// 改变状态移除目标
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="NotImplementedException"></exception>
        private async void Complete(BacklogDto obj)
        {
            try
            {
                UpdateLoading(true);
                var updateResult = await backlogService.UpdateAsync(obj);

                if (updateResult.Status)
                {
                    var result = Summary.BacklogList.FirstOrDefault(x => x.Id.Equals(obj.Id));

                    if (result != null)
                    {
                        Summary.BacklogList.Remove(result);
                    }
                }
                GetSummary();

                aggregator.SendHintMessage("已完成");
            }
            catch (Exception ex) 
            {
                throw ex;
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

            var result = await memoService.DeleteAsync(obj.Id);
            if (result.Status)
            {
                var backlog = Summary.MemoList.FirstOrDefault(t => t.Id == obj.Id);
                if (backlog != null)
                {
                    Summary.MemoList.Remove(backlog);
                }
            }
        }

        /// <summary>
        /// 导航栏加载时
        /// </summary>
        /// <param name="navigationContext"></param>
        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            GetSummary();

            base.OnNavigatedTo(navigationContext);
        }

        /// <summary>
        /// 获取汇总信息
        /// </summary>
        private async void GetSummary()
        {
            var summaryResult = await backlogService.GetSummaryAsync();

            if (summaryResult.Status)
            {
                Summary = summaryResult.Result;
                Refresh();
            }
        }

        /// <summary>
        /// 刷新任务栏
        /// </summary>
        private void Refresh()
        {
            TaskBarModels[0].Content = Summary.BacklogSum.ToString();
            TaskBarModels[1].Content = Summary.CompletedCount.ToString();
            TaskBarModels[2].Content = Summary.CompletedRatio.ToString();
            TaskBarModels[3].Content = Summary.MemoeCount.ToString();
        }

        /// <summary>
        /// 点击任务栏导航到指定的窗口中
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Navigate(TaskBarModel obj)
        {
            NavigationParameters pairs = new NavigationParameters();

            if (obj.Title == "已完成")
            {
                pairs.Add("Value", 2);
            }

            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(obj.NameSpace, pairs);
        }

        /// <summary>
        /// 定时器，隔一秒更新下标题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object? sender, EventArgs e)
        {
            Title = $"你好，{userName}，现在是：{DateTime.Now.ToString("yyyy年MM月dd日 dddd tt HH:mm:ss")}";
        }

        /// <summary>
        /// 获取登录的用户名，用于动态改名字
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void GetName(string obj)
        {
            userName = obj;
            Title = $"你好，{userName}，现在是：{DateTime.Now.ToString("yyyy年MM月dd日 dddd tt HH:mm:ss")}";
        }
        #endregion
    }
}
