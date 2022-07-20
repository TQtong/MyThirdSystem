using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.Common.Models
{
    public class MessageModel
    {
        /// <summary>
        /// 过滤器(防止多个订阅后，都触发对应的消息)
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// 消息主体
        /// </summary>
        public string Message { get; set; }
    }
}
