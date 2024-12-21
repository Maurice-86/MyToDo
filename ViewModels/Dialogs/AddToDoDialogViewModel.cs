using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyToDo.ViewModels.Dialogs
{
    public partial class AddToDoDialogViewModel : ViewModelBase, IModalDialogViewModel
    {
        private bool? dialogResult;

        public bool? DialogResult
        {
            get => dialogResult;
            private set => SetProperty(ref dialogResult, value);
        }

        [ObservableProperty]
        private string? title;

        [ObservableProperty]
        private string? content;

        [ObservableProperty]
        private int status;

        [RelayCommand]
        private void Ok()
        {
            if (!string.IsNullOrWhiteSpace(Title))
                DialogResult = true;
        }

        [RelayCommand]
        private void Cancel()
        {
            DialogResult = false;
        }
    }
}
