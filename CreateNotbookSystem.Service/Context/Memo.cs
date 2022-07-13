using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.Service.Context
{
    /// <summary>
    /// 备忘录
    /// </summary>
    [Table("memo")]
    public class Memo : BaseEntity
    {
        [Column("title")]
        public string Title { get; set; }

        [Column("content")]
        public string Content { get; set; }
    }
}
