using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyToDo.Common.Models;
using MyToDo.Services.Implementations;
using MyToDo.ViewModels.Settings;
using System.Collections.ObjectModel;

namespace MyToDo.ViewModels
{
    public partial class SettingsViewModel : ViewModelBase
    {
        private readonly SettingsNavigationService navigator;

        public SettingsViewModel(SettingsNavigationService navigationService)
        {
            navigator = navigationService;
            CreateMenuBars();

            navigator.CurrentViewModelChanged += () =>
            {
                CurrentViewModel = navigator.CurrentViewModel;
            };
            navigator.NavigateTo(typeof(SkinViewModel));
        }

        [ObservableProperty]
        private ViewModelBase? currentViewModel;

        [ObservableProperty]
        private ObservableCollection<MenuBarModel>? menuBars;

        [ObservableProperty]
        private int selectedIndex;

        [RelayCommand]
        private void NavigateToView(MenuBarModel? menuBar)
        {
            if (menuBar?.MyType != null)
            {
                navigator.NavigateTo(menuBar.MyType);
            }

        }

        private void CreateMenuBars()
        {
            MenuBars = new ObservableCollection<MenuBarModel> {
                new MenuBarModel { Icon = "Palette", Name = "个性化" , MyType = typeof(SkinViewModel)},
                new MenuBarModel { Icon = "Palette", Name = "我的账号" , MyType = typeof(UserViewModel)},
                new MenuBarModel { Icon = "Information", Name = "关于更多" , MyType = typeof(AboutViewModel)}
            };
        }
    }
}
