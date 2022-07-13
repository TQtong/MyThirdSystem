using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.Common.DbContent.Dto
{
    /// <summary>
    /// 待办事项
    /// </summary>
    public class BacklogDto : BaseDto
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Status { get; set; }
    }
}
