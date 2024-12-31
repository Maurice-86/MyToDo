using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyToDo.Common.Models;
using MyToDo.Services.Implementations;
using System.Collections.ObjectModel;

namespace MyToDo.ViewModels
{
    public partial class HomeViewModel : ViewModelBase
    {
        private readonly HomeNavigationService navigator;

        public HomeViewModel(HomeNavigationService navigationService)
        {
            navigator = navigationService;

            CreateMenuBars();

            navigator.CurrentViewModelChanged += () =>
            {
                CurrentViewModel = navigator.CurrentViewModel;
            };

            navigator.NavigateTo(typeof(IndexViewModel));
        }

        [ObservableProperty]
        private ViewModelBase? currentViewModel;

        [ObservableProperty]
        private string? username = App.Current.Username;

        [ObservableProperty]
        private ObservableCollection<MenuBarModel>? menuBars;

        [ObservableProperty]
        private bool isLeftDrawerOpen;

        [RelayCommand]
        private async Task NavigateToView(MenuBarModel? menuBar)
        {
            if (menuBar?.MyType != null)
            {
                navigator.NavigateTo(menuBar.MyType);
                await Task.Delay(TimeSpan.FromSeconds(0.3));
                IsLeftDrawerOpen = false;
            }
        }

        private void CreateMenuBars()
        {
            MenuBars = new ObservableCollection<MenuBarModel> {
                new MenuBarModel { Icon = "Home", Name = "首页" , MyType = typeof(IndexViewModel)},
                new MenuBarModel { Icon = "FormatListChecks", Name = "待办事项" , MyType = typeof(ToDoViewModel)},
                new MenuBarModel { Icon = "Animation", Name = "备忘录" , MyType = typeof(MemoViewModel)},
                new MenuBarModel { Icon = "Cog", Name = "设置" , MyType = typeof(SettingsViewModel)},
            };
        }
    }
}