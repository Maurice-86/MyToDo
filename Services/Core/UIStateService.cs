using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Services.Core
{
    public class UIStateService : INotifyPropertyChanged
    {
        private bool isMaskVisible;
        private bool isLoadingVisible;
        private string? snacbarMessage;

        /// <summary>
        /// 控制主窗体遮罩层的显示，即变灰效果
        /// </summary>
        public bool IsMaskVisible
        {
            get => isMaskVisible;
            set
            {
                if (isMaskVisible != value)
                {
                    isMaskVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 控制 Loading 图标的显示
        /// </summary>
        public bool IsLoadingVisible
        {
            get => isLoadingVisible;
            set
            {
                if (isLoadingVisible != value)
                {
                    isLoadingVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? SnacbarMessage
        {
            get => snacbarMessage;
            set
            {
                if (snacbarMessage != value)
                {
                    snacbarMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public void SetSnacbarMessage(string? message)
        {
            SnacbarMessage = message;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
