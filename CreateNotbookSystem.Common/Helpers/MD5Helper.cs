using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.Common.Helpers
{
    public static class MD5Helper
    {
        /// <summary>
        /// 生成加密字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetMD5(this string data)
        {
            if (string.IsNullOrWhiteSpace(data))
                throw new ArgumentNullException(nameof(data));

            var hash = MD5.Create().ComputeHash(Encoding.Default.GetBytes(data));
            return Convert.ToBase64String(hash);//将加密后的字节数组转换为加密字符串
        }
    }
}
