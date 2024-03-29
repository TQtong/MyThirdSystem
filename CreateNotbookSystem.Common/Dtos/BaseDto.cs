﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.Common.DbContent.Dto
{
    public class BaseDto : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// 实现通知绑定
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged([CallerMemberName] string propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
