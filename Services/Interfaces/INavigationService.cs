using MyToDo.ViewModels;

namespace MyToDo.Services.Interfaces
{
    public interface INavigationService
    {
        ViewModelBase? CurrentViewModel { get; }

        event Action? CurrentViewModelChanged;

        void NavigateTo(Type viewModelType);
    }
}
