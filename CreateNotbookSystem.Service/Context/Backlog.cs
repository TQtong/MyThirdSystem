using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.Service.Context
{
    /// <summary>
    /// 待办事项
    /// </summary>
    public class Backlog : BaseEntity
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public int Status { get; set; } //1：表示完成；0:表示未完成
    }
}
