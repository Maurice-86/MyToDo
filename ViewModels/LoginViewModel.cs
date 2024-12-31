using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyToDo.Common.Extensions;
using MyToDo.Common.Helps;
using MyToDo.Common.Models;
using MyToDo.Services.Core;
using MyToDo.Services.Implementations;

namespace MyToDo.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {
        private readonly MainNavigationService navigator;
        private readonly UIStateService uIStateService;
        private readonly RecordUserModel? recordUser;

        public LoginViewModel(MainNavigationService navigationService, UIStateService uIStateService)
        {
            this.navigator = navigationService;
            this.uIStateService = uIStateService;
            recordUser = RecordUserHelp.LoadUser();

            Username = recordUser?.Username;
            Password = recordUser?.Password;
            ConfirmPassword = recordUser?.Password;
        }

        [ObservableProperty]
        private string? username;

        [ObservableProperty]
        private string? password;

        [ObservableProperty]
        private string? confirmPassword;

        [ObservableProperty]
        private int selectedIndex;

        [RelayCommand]
        private void SwitchExecute(string name)
            => SelectedIndex = name == "Login" ? 0 : 1;

        [RelayCommand]
        private async Task LoginAsync()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                return;

            bool? result = null;
            string? resultMessage = null;
            try
            {
                uIStateService.IsLoadingVisible = true;
                (result, resultMessage) = await AuthService.LoginAsync(Username, Password);
            }
            finally
            {
                await Task.Delay(TimeSpan.FromSeconds(0.5));
                uIStateService.IsLoadingVisible = false;
                if (resultMessage != null)
                    uIStateService.SetSnacbarMessage(resultMessage);

                if (result == true)
                    GoToHomeView();
            }
        }

        [RelayCommand]
        private async Task RegisterAsync()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
                return;

            if (Password != ConfirmPassword)
            {
                uIStateService.SetSnacbarMessage("密码与确认密码不同");
                return;
            }

            bool? result = null;
            string? resultMessage = null;
            try
            {
                uIStateService.IsLoadingVisible = true;
                (result, resultMessage) = await AuthService.RegisterAsync(Username, Password);
            }
            finally
            {
                await Task.Delay(TimeSpan.FromSeconds(0.5));
                uIStateService.IsLoadingVisible = false;
                if (resultMessage != null)
                    uIStateService.SetSnacbarMessage(resultMessage);

                if (result == true)
                    GoToHomeView();
            }
        }

        private void GoToHomeView()
        {
            WindowExtension.Hide();
            navigator.NavigateTo(typeof(HomeViewModel));
            WindowExtension.ChangeWindwoSize(1150, 646);
            WindowExtension.HideWindowBar();
            WindowExtension.Show();
        }
    }
}
