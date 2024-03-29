﻿using CreateNotbookSystem.CustomControl.Views;
using CreateNotbookSystem.NavigationBar.Extensions;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CreateNotbookSystem.App.Views
{
    /// <summary>
    /// MainWindowView.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView(IEventAggregator aggregator)
        {
            InitializeComponent();

            //注册等待消息窗口
            aggregator.Resgiter(arg =>
            {
                dialogTheme.IsOpen = arg.IsOpen;

                if (dialogTheme.IsOpen)
                {
                    dialogTheme.DialogContent = new ProgressView();
                }
            });

            //窗口最小化
            btnMin.Click += (s, e) =>
            {
                this.WindowState = WindowState.Minimized;
            };

            //窗口最大化
            btnMax.Click += (s, e) =>
            {
                if (this.WindowState == WindowState.Maximized)
                {
                    this.WindowState = WindowState.Normal;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                }
            };

            //窗口关闭
            btnClose.Click += (s, e) =>
            {
                this.Close();
            };

            //窗口拖动
            colorZone.MouseMove += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };

            //双击窗口放大
            colorZone.MouseDoubleClick += (s, e) =>
            {
                if (this.WindowState == WindowState.Normal)
                {
                    this.WindowState = WindowState.Maximized;
                }
                else
                {
                    this.WindowState = WindowState.Normal;
                }
            };
        }
    }
}
