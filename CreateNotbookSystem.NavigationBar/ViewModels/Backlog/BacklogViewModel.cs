using CreateNotbookSystem.Common.Models;
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
        /// <summary>
        /// 显示隐藏侧边内容
        /// </summary>
        public DelegateCommand OpenSideWindow { get; private set; }

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


        public BacklogViewModel()
        {
            OpenSideWindow = new DelegateCommand(Open);
            BacklogModels = new ObservableCollection<BacklogModel>();
            CreateTaskBarInfo();
        }

        /// <summary>
        /// 显示隐藏侧边内容
        /// </summary>
        private void Open()
        {

            IsRightDrawerOpen = true;

        }

        private void CreateTaskBarInfo()
        {
            BacklogModels.Clear();

            for (int i = 0; i < 20; i++)
            {
                BacklogModels.Add(new BacklogModel() { Title = $"{i}", Content = "哈哈哈" });
            }
        }
    }
}
