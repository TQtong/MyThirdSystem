using CreateNotbookSystem.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.NavigationBar.ViewModels.Memo
{
    public class MemoViewModel : BindableBase
    {

        /// <summary>
        /// 显示隐藏侧边内容
        /// </summary>
        public DelegateCommand OpenSideWindow { get; private set; }

        private ObservableCollection<MemoModel> memoModels;
        /// <summary>
        /// 待办事项
        /// </summary>
        public ObservableCollection<MemoModel> MemoModels
        {
            get => memoModels;
            set
            {
                memoModels = value;
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


        public MemoViewModel()
        {
            OpenSideWindow = new DelegateCommand(Open);
            MemoModels = new ObservableCollection<MemoModel>();
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
            MemoModels.Clear();

            for (int i = 0; i < 20; i++)
            {
                MemoModels.Add(new MemoModel() { Title = $"{i}", Content = "哈哈哈" });
            }
        }

    }
}
