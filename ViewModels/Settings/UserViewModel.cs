using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyToDo.Common.Extensions;
using MyToDo.Common.Helps;
using MyToDo.Common.Models;
using MyToDo.Services.Implementations;

namespace MyToDo.ViewModels.Settings
{
    public partial class UserViewModel : ViewModelBase
    {
        private readonly MainNavigationService navigator;
        private readonly RecordUserModel? recordUser;

        public UserViewModel(MainNavigationService navigationService)
        {
            navigator = navigationService;

            recordUser = RecordUserHelp.LoadUser();

            Username = recordUser?.Username;
            Password = recordUser?.Password;
        }
        [ObservableProperty]
        private string? username;

        [ObservableProperty]
        private string? password;

        [RelayCommand]
        private void Logout()
        {
            WindowExtension.Hide();
            navigator.NavigateTo(typeof(LoginViewModel));
            WindowExtension.ShowWindowBar();
            WindowExtension.ChangeWindwoSize(800, 450);
            WindowExtension.Show();
        }
    }
}
