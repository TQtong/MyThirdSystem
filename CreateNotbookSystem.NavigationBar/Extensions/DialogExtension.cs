﻿using CreateNotbookSystem.Common.Models;
using CreateNotbookSystem.NavigationBar.Event;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.NavigationBar.Extensions
{
    public static class DialogExtension
    {
        /// <summary>
        /// 推送等待消息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="action"></param>
        public static void UpdateLoading(this IEventAggregator aggregator, UpdateModel model)
        {
            aggregator.GetEvent<UpdateLoadingEvent>().Publish(model);
        }

        /// <summary>
        /// 注册等待消息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="model"></param>
        public static void Resgiter(this IEventAggregator aggregator, Action<UpdateModel> model)
        {
            aggregator.GetEvent<UpdateLoadingEvent>().Subscribe(model);
        }
    }
}
