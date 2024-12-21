using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using MyToDo.Common.Models;
using MyToDo.Services.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        private readonly NavigationService navigator;
        private readonly UIStateService uIStateService;

        public MainViewModel(NavigationService navigationService, UIStateService uIStateService)
        {
            navigator = navigationService;
            this.uIStateService = uIStateService;

            CreateMenuBars();

            navigator.CurrentViewModelChanged += () =>
            {
                CurrentViewModel = navigator.CurrentViewModel;
            };

            uIStateService.PropertyChanged += OnUIStateServicePropertyChanged;

            MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2));

            navigator.NavigateTo(typeof(IndexViewModel));
        }

        [ObservableProperty]
        private ViewModelBase? currentViewModel;

        public bool IsMaskVisible => uIStateService.IsMaskVisible;
        public bool IsLoadingVisible => uIStateService.IsLoadingVisible;
        public SnackbarMessageQueue MessageQueue { get; set; }

        [ObservableProperty]
        private ObservableCollection<MenuBarModel>? menuBars;

        [ObservableProperty]
        private bool isLeftDrawerOpen;

        [RelayCommand]
        private async Task NavigateToView(MenuBarModel menuBar)
        {
            if (menuBar.MyType != null)
            {
                navigator.NavigateTo(menuBar.MyType);
                await Task.Delay(TimeSpan.FromSeconds(0.3));
                IsLeftDrawerOpen = false;
            }

        }

        private void OnUIStateServicePropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(UIStateService.IsMaskVisible))
            {
                OnPropertyChanged(nameof(IsMaskVisible));
            }
            if (e.PropertyName == nameof(UIStateService.IsLoadingVisible))
            {
                OnPropertyChanged(nameof(IsLoadingVisible));
            }
            if (e.PropertyName == nameof(UIStateService.SnacbarMessage))
            {
                var message = uIStateService.SnacbarMessage;
                if (message != null)
                {
                    MessageQueue.Enqueue(message);
                    uIStateService.SnacbarMessage = null;
                }
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
