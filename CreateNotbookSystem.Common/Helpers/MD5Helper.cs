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
        /// MD5加密字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string MD5Encryption(this string data)
        {
            //这种方法生成的密码太简单了
            //MD5 mD5 = MD5.Create();
            //byte[] pwd = mD5.ComputeHash(Encoding.UTF8.GetBytes(str));
            //string password = "";
            //foreach (var item in pwd)
            //{
            //    password += item.ToString("X2");
            //}
            //password = password.ToLower();
            //return password;

            if (string.IsNullOrWhiteSpace(data))
            {
                throw new ArgumentNullException(nameof(data));
            }

            var hash = MD5.Create().ComputeHash(Encoding.Default.GetBytes(data));
            return Convert.ToBase64String(hash);//将加密后的字节数组转换为加密字符串
        }

    }
}
