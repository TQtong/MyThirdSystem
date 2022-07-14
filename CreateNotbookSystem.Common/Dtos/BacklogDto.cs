using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.Common.DbContent.Dto
{
    /// <summary>
    /// 待办事项数据实体（控制器传输的实体）
    /// </summary>
    public class BacklogDto : BaseDto
    {

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
                OnPropertyChanged();
            }
        }

        private string content;
        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get => content;
            set
            {
                content = value;
                OnPropertyChanged();
            }
        }

        private int status;
        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get => status;
            set
            {
                status = value;
                OnPropertyChanged();
            }
        }

    }
}
