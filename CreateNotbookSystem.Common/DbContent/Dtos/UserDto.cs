using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.Common.DbContent.Dto
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class UserDto : BaseDto
    {
        public string Account { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }
    }
}
