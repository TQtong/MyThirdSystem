using CreateNotbookSystem.Common.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.Common.Models
{
    /// <summary>
    /// 任务栏实体
    /// </summary>
    public class TaskBarModel : CommonBaseModel
    {
        /// <summary>
        /// 背景颜色
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public string Number { get; set; }

    }
}
