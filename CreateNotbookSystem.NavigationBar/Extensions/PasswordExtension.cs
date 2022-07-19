using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CreateNotbookSystem.NavigationBar.Extensions
{
    public class PasswordExtension
    {
        /// <summary>
        /// 得到密码值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(PasswordPropertyProperty);
        }

        /// <summary>
        /// 更新密码值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetPassword(DependencyObject obj, string value)
        {
            obj.SetValue(PasswordPropertyProperty, value);
        }

        public static readonly DependencyProperty PasswordPropertyProperty =
            DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordExtension), new FrameworkPropertyMetadata(string.Empty, OnPassWordPropertyChanged));

        /// <summary>
        /// 动态改变命名框的值
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnPassWordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = d as PasswordBox;
            var newPassword = (string)e.NewValue;
            if (passwordBox != null && passwordBox.Password != newPassword)
            {
                passwordBox.Password = newPassword;
            }
        }
    }
}
