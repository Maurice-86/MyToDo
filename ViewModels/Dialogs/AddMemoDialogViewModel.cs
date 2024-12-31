using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmDialogs;

namespace MyToDo.ViewModels.Dialogs
{
    public partial class AddMemoDialogViewModel : ViewModelBase, IModalDialogViewModel
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
