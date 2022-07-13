using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.Common.Models.BaseModels
{
    /// <summary>
    /// 公共基类
    /// </summary>
    public abstract class CommonBaseModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public virtual string Content { get; set; }

        /// <summary>
        /// 命名空间
        /// </summary>
        public virtual string NameSpace { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public virtual string Icon { get; set; }
    }
}
