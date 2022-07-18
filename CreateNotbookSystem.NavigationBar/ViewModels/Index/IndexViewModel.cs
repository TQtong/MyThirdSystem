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

        public SummaryModel Summary
        {
            get => summary;
            set
            {
                summary = value;
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
        #endregion

        #region 构造函数
        public IndexViewModel(IContainerProvider container) : base(container)
        {
            TaskBarModels = new ObservableCollection<TaskBarModel>();

            ExecuteCommand = new DelegateCommand<string>(Execute);
            EditBacklogCommand = new DelegateCommand<BacklogDto>(AddBacklog);
            EditMemoCommand = new DelegateCommand<MemoDto>(AddMemo);
            BacklogCompleteCommand = new DelegateCommand<BacklogDto>(Complete);
            DeleteMemoCommand = new DelegateCommand<MemoDto>(Delete);

            this.backlogService = container.Resolve<IBacklogService>();
            this.memoService = container.Resolve<IMemoService>();

            this.dialog = container.Resolve<IDialogHostService>();

            CreateTaskBar();
        }




        #endregion

        #region 方法
        /// <summary>
        /// 创建任务栏
        /// </summary>
        private void CreateTaskBar()
        {
            TaskBarModels.Add(new TaskBarModel() { Color = "#FF0CA0FF", Icon = "ClockFast", Title = "汇总", NameSpace = "" });
            TaskBarModels.Add(new TaskBarModel() { Color = "#FF1ECA3A", Icon = "ClockCheckOutline", Title = "完成", NameSpace = "" });
            TaskBarModels.Add(new TaskBarModel() { Color = "#FF02C6DC", Icon = "ChartLineVariant", Title = "完成比例", NameSpace = "" });
            TaskBarModels.Add(new TaskBarModel() { Color = "#FFFFA000", Icon = "PlaylistStar", Title = "备忘录", NameSpace = "" });
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
                        Summary.BacklogList.Add(result.Result);
                    }
                }
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
                        Summary.MemoList.Add(result.Result);
                    }
                }
            }
        }

        /// <summary>
        /// 改变状态移除目标
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="NotImplementedException"></exception>
        private async void Complete(BacklogDto obj)
        {
            var updateResult = await backlogService.UpdateAsync(obj);

            if (updateResult.Status)
            {
                var result = Summary.BacklogList.FirstOrDefault(x => x.Id.Equals(obj.Id));

                if (result != null)
                {
                    Summary.BacklogList.Remove(result);
                }
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
            var summaryResult = await backlogService.GetSummaryAsync();

            if (summaryResult.Status)
            {
                Summary = summaryResult.Result;
                Refresh();
            }

            base.OnNavigatedTo(navigationContext);
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
        #endregion



    }
}
