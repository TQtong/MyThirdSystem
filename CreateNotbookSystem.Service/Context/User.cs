using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.Service.Context
{
    /// <summary>
    /// 用户表
    /// </summary>
    [Table("user")]
    public class User : BaseEntity
    {
        [Column("account")]
        public string Account { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("password")]
        public string Password { get; set; }

    }
}
