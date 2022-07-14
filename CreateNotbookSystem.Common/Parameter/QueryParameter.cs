using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.Common.Parameter
{
    /// <summary>
    /// 查询的对象
    /// </summary>
    public class QueryParameter
    {
        /// <summary>
        /// 查询的页数
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 查询的数量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 查询的条件（id、title等）
        /// </summary>
        public string? Search { get; set; }
    }
}
