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
        #region 属性
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
        #endregion

        #region 构造函数
        public BacklogViewModel()
        {
            OpenSideWindow = new DelegateCommand(Open);
            BacklogModels = new ObservableCollection<BacklogModel>();
            CreateTaskBar();
        }

        #endregion

        #region 方法
        private void CreateTaskBar()
        {
            BacklogModels.Clear();

            for (int i = 0; i < 20; i++)
            {
                BacklogModels.Add(new BacklogModel() { Title = $"{i}", Content = "哈哈哈" });
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
