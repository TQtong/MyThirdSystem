using CreateNotbookSystem.Common.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.Common.Models
{
    /// <summary>
    /// 待办事项
    /// </summary>
    public class BacklogModel : CommonBaseModel
    {
        private bool state;
        /// <summary>
        /// 状态：true表示完成，false表示为完成
        /// </summary>
        public bool State
        {
            get => state;
            set
            {
                state = value;
                RaisePropertyChanged();
            }
        }

    }
}
