using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyToDo.Common.Models;
using MyToDo.Services.Core;
using MyToDo.ViewModels.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace MyToDo.ViewModels
{
    public partial class SettingsViewModel : ViewModelBase
    {
        private NavigationService navigator;
        public SettingsViewModel()
        {
            navigator = new NavigationService();
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
                new MenuBarModel { Icon = "Information", Name = "关于更多" , MyType = typeof(AboutViewModel)}
            };
        }

    }
}
