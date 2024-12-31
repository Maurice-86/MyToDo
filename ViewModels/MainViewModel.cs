using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using MyToDo.Services.Core;
using MyToDo.Services.Implementations;
using System.ComponentModel;

namespace MyToDo.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        private readonly MainNavigationService navigator;
        private readonly UIStateService uIStateService;

        public MainViewModel(MainNavigationService navigationService, UIStateService uIStateService)
        {
            navigator = navigationService;
            this.uIStateService = uIStateService;

            navigator.CurrentViewModelChanged += () =>
            {
                CurrentViewModel = navigator.CurrentViewModel;
            };

            uIStateService.PropertyChanged += OnUIStateServicePropertyChanged;

            MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2));

            navigator.NavigateTo(typeof(LoginViewModel));
        }

        [ObservableProperty]
        private ViewModelBase? currentViewModel;

        public bool IsMaskVisible => uIStateService.IsMaskVisible;
        public bool IsLoadingVisible => uIStateService.IsLoadingVisible;
        public SnackbarMessageQueue MessageQueue { get; set; }

        [RelayCommand]
        private void NavigateToView(Type viewModelType)
        {
            navigator.NavigateTo(viewModelType);
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
    }
}
