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
        private string account;
        /// <summary>
        /// 账号
        /// </summary>
        public string Account
        {
            get => account;
            set
            {
                account = value;
                OnPropertyChanged();
            }
        }

        private string name;
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        private string password;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

    }
}
