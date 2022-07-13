using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.Service.Context
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class User : BaseEntity
    {
        public string Account { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

    }
}
