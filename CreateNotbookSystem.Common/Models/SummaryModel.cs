using CreateNotbookSystem.Common.DbContent.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.Common.Models
{
    /// <summary>
    /// 汇总
    /// </summary>
    public class SummaryModel : BaseDto
    {
        private int backlogSum;
        /// <summary>
        /// 待办事项总数
        /// </summary>
        public int BacklogSum
        {
            get => backlogSum;
            set
            {
                backlogSum = value;
                OnPropertyChanged();
            }
        }

        private int completedCount;
        /// <summary>
        /// 完成待办事项数量
        /// </summary>
        public int CompletedCount
        {
            get => completedCount;
            set
            {
                completedCount = value;
                OnPropertyChanged();
            }
        }

        private int memoeCount;
        /// <summary>
        /// 备忘录数量
        /// </summary>
        public int MemoeCount
        {
            get => memoeCount;
            set
            {
                memoeCount = value;
                OnPropertyChanged();
            }
        }

        private string completedRatio;
        /// <summary>
        /// 完成比例
        /// </summary>
        public string CompletedRatio
        {
            get => completedRatio;
            set
            {
                completedRatio = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<BacklogDto> backlogList;
        /// <summary>
        /// 待办事项列表
        /// </summary>
        public ObservableCollection<BacklogDto> BacklogList
        {
            get => backlogList;
            set
            {
                backlogList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MemoDto> memoList;
        /// <summary>
        /// 备忘录列表
        /// </summary>
        public ObservableCollection<MemoDto> MemoList
        {
            get => memoList;
            set
            {
                memoList = value;
                OnPropertyChanged();
            }
        }

    }
}
