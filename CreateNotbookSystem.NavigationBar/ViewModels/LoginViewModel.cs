using CreateNotbookSystem.Common.Models;
using CreateNotbookSystem.NavigationBar.Event;
using CreateNotbookSystem.NavigationBar.Extensions;
using CreateNotbookSystem.NavigationBar.Service;
using CreateNotbookSystem.NavigationBar.ViewModels.BaseViewModels;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CreateNotbookSystem.NavigationBar.ViewModels
{
    public class LoginViewModel : BindableBase, IDialogAware
    {
        #region 属性
        public string Title { get; set; } = "登录";

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
                RaisePropertyChanged();
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
                RaisePropertyChanged();
            }
        }

        private int selectIndex;
        /// <summary>
        /// 页面索引（用于切换登录注册界面）
        /// </summary>
        public int SelectIndex
        {
            get => selectIndex;
            set
            {
                selectIndex = value;
                RaisePropertyChanged();
            }
        }

        private RegisterModel registerModel;

        public RegisterModel RegisterModel
        {
            get => registerModel;
            set
            {
                registerModel = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region 字段
        public event Action<IDialogResult> RequestClose;

        private readonly ILoginService service;

        private readonly IEventAggregator aggregator;
        #endregion

        #region 命令
        public DelegateCommand<string> ExecuteCommand { get; private set; }
        #endregion

        public LoginViewModel(ILoginService service, IEventAggregator aggregator)
        {
            RegisterModel = new RegisterModel();

            this.service = service;
            this.aggregator = aggregator;

            ExecuteCommand = new DelegateCommand<string>(Execute);
        }

        #region 方法

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            Exit();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "Login":
                    Login();//登录
                    break;
                case "Exit":
                    Exit();//退出
                    break;
                case "GoRegister":
                    SelectIndex = 1;//跳转注册界面
                    break;
                case "GoLogin":
                    SelectIndex = 0;//返回登录界面
                    break;
                case "Register":
                    Register();//注册账号
                    break;
                case "GoPassword":
                    SelectIndex = 2;//跳转找回密码界面
                    break;
                case "Retrieve":
                    Retrieve();//找回密码
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        private async void Login()
        {
            if (string.IsNullOrWhiteSpace(Account) || string.IsNullOrWhiteSpace(Password))
            {
                return;
            }

            try
            {

                var result = await service.LoginAsync(new Common.DbContent.Dto.UserDto()
                {
                    Account = Account,
                    Password = Password
                });

                if (result.Status)
                {
                    RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
                    //aggregator.SendHintMessage("登录成功", "Login");
                    aggregator.GetEvent<UserNameEvent>().Publish(result.Result.Name);
                }
                else
                {
                    aggregator.SendHintMessage(result.Message, "Login");
                }
            }
            finally
            {
            }

        }

        /// <summary>
        /// 注册账户
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private async void Register()
        {
            if (string.IsNullOrWhiteSpace(RegisterModel.Account) || string.IsNullOrWhiteSpace(RegisterModel.Name) || string.IsNullOrWhiteSpace(RegisterModel.Password) || string.IsNullOrWhiteSpace(RegisterModel.NewPassword))
            {
                return;
            }

            if (RegisterModel.Password != RegisterModel.NewPassword)
            {
                aggregator.SendHintMessage("两次命码不一致", "Login");
                return;
            }

            try
            {
                var result = await service.RegisterAsync(new Common.DbContent.Dto.UserDto()
                {
                    Account = RegisterModel.Account,
                    Password = RegisterModel.Password,
                    Name = RegisterModel.Name,
                });

                if (result.Status)
                {
                    SelectIndex = 0; //注册成功
                    aggregator.SendHintMessage("注册成功", "Login");
                }
                else
                {
                    aggregator.SendHintMessage(result.Message, "Login");
                }
            }
            finally
            {
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        private void Exit()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.No));
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void Retrieve()
        {

        }
        #endregion
    }
}
