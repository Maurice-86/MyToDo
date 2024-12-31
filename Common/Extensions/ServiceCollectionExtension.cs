using Microsoft.Extensions.DependencyInjection;
using MyToDo.Services.Implementations;
using MyToDo.ViewModels;
using MyToDo.ViewModels.Settings;
using MyToDo.Views;
using MyToDo.Views.Settings;

namespace MyToDo.Common.Extensions
{
    public static class ServiceContainerExtension
    {
        public static IServiceCollection AddNavigations(this IServiceCollection collection)
        {
            collection.AddSingleton<MainNavigationService>();
            collection.AddSingleton<HomeNavigationService>();
            collection.AddSingleton<SettingsNavigationService>();
            return collection;
        }

        public static IServiceCollection AddServices(this IServiceCollection collection)
        {
            collection.AddSingleton<AuthService>();
            collection.AddSingleton<ToDoService>();
            collection.AddSingleton<MemoService>();
            return collection;
        }

        public static IServiceCollection AddViews(this IServiceCollection collection)
        {
            collection.AddSingleton<MainView>();
            collection.AddSingleton<LoginView>();
            collection.AddSingleton<HomeView>();
            collection.AddSingleton<IndexView>();
            collection.AddSingleton<ToDoView>();
            collection.AddSingleton<MemoView>();
            collection.AddSingleton<SettingsView>();
            collection.AddSingleton<SkinView>();
            collection.AddSingleton<UserView>();
            collection.AddSingleton<AboutView>();
            return collection;
        }

        public static IServiceCollection AddViewModels(this IServiceCollection collection)
        {
            collection.AddSingleton<MainViewModel>();
            collection.AddSingleton<LoginViewModel>();
            collection.AddTransient<HomeViewModel>();
            collection.AddTransient<IndexViewModel>();
            collection.AddTransient<ToDoViewModel>();
            collection.AddTransient<MemoViewModel>();
            collection.AddSingleton<SettingsViewModel>();
            collection.AddSingleton<SkinViewModel>();
            collection.AddSingleton<UserViewModel>();
            collection.AddSingleton<AboutViewModel>();
            return collection;
        }
    }
}
