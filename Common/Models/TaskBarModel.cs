﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common.Models
{
    public class TaskBarModel : INotifyPropertyChanged
    {
        private string content = string.Empty;

        public string Color { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Content
        {
            get => content;
            set
            {
                if (content != value)
                {
                    content = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
