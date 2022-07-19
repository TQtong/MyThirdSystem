using CreateNotbookSystem.NavigationBar.Extensions;
using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CreateNotbookSystem.NavigationBar.Behavior
{
    public class PasswordBehavior : Behavior<PasswordBox>
    {
        /// <summary>
        /// 为密码框添加改变事件
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PasswordChanged += AssociatedObject_PasswordChanged;
        }

        /// <summary>
        /// 为密码框移除改变事件
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PasswordChanged -= AssociatedObject_PasswordChanged;
        }

        /// <summary>
        /// 用于监听密码框是否有改动，有就调用写入函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociatedObject_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            string password = PasswordExtension.GetPassword(passwordBox);

            if (passwordBox != null && passwordBox.Password != password)
            {
                PasswordExtension.SetPassword(passwordBox, passwordBox.Password);
            }
        }
    }
}
