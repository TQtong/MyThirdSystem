using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.Service.Context
{
    /// <summary>
    /// 待办事项
    /// </summary>
    [Table("backlog")]
    public class Backlog : BaseEntity
    {
        [Column("title")]
        public string Title { get; set; }

        [Column("content")]
        public string Content { get; set; }

        [Column("status")]
        public int Status { get; set; } //1：表示完成；0:表示未完成
    }
}
