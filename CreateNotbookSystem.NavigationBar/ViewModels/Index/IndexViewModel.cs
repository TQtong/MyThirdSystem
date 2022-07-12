using CreateNotbookSystem.Common.Models;
using Prism.Mvvm;
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



        public IndexViewModel()
        {
            TaskBarModels = new ObservableCollection<TaskBarModel>();
            BacklogModels = new ObservableCollection<BacklogModel>();
            MemorModels = new ObservableCollection<MemoModel>();
            CreateTaskBar();
            CreateTaskBarInfo();
        }

        private void CreateTaskBarInfo()
        {
            BacklogModels.Clear();
            MemorModels.Clear();

            for (int i = 0; i < 20; i++)
            {
                BacklogModels.Add(new BacklogModel() { Title = $"{i}", Content = "哈哈哈" });
                MemorModels.Add(new MemoModel() { Title = $"{i}", Content = "哈哈哈" });
            }
        }

        private void CreateTaskBar()
        {
            TaskBarModels.Add(new TaskBarModel() { Color = "#FF0CA0FF", Icon = "ClockFast", Title = "汇总", Number = "1", NameSpace = "" });
            TaskBarModels.Add(new TaskBarModel() { Color = "#FF1ECA3A", Icon = "ClockCheckOutline", Title = "完成", Number = "2", NameSpace = "" });
            TaskBarModels.Add(new TaskBarModel() { Color = "#FF02C6DC", Icon = "ChartLineVariant", Title = "完成比例", Number = "97%", NameSpace = "" });
            TaskBarModels.Add(new TaskBarModel() { Color = "#FFFFA000", Icon = "PlaylistStar", Title = "备忘录", Number = "4", NameSpace = "" });
        }

    }
}
