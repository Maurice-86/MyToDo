using Microsoft.Extensions.DependencyInjection;
using MvvmDialogs;
using MyToDo.Services.Core;
using MyToDo.Services.Implementations;
using MyToDo.Services.Interfaces;
using MyToDo.Shared.Dtos;
using MyToDo.ViewModels;
using MyToDo.ViewModels.Settings;
using MyToDo.Views;
using MyToDo.Views.Dialogs;
using MyToDo.Views.Settings;
using System.Configuration;
using System.Data;
using System.Windows;

namespace MyToDo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider Services { get; }
        public static new App Current => (App)Application.Current;

        public App()
        {
            var container = new ServiceCollection();

            container.AddSingleton<NavigationService>();    // 导航服务

            container.AddSingleton<IDialogService, DialogService>();    // 对话框服务

            container.AddSingleton<UIStateService>();  // 用户界面的状态管理，包括遮罩、加载动画、进度条等

            // 请求接口相关的服务
            container.AddSingleton<ToDoService>();
            container.AddSingleton<MemoService>();

            container.AddSingleton<MainView>();
            container.AddSingleton<IndexView>();
            container.AddSingleton<ToDoView>();
            container.AddSingleton<MemoView>();
            container.AddSingleton<SettingsView>();
            container.AddSingleton<SkinView>();
            container.AddSingleton<AboutView>();

            container.AddSingleton<MainViewModel>();
            container.AddTransient<IndexViewModel>();
            container.AddTransient<ToDoViewModel>();
            container.AddTransient<MemoViewModel>();
            container.AddSingleton<SettingsViewModel>();
            container.AddSingleton<SkinViewModel>();
            container.AddSingleton<AboutViewModel>();

            Services = container.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainView = Services.GetService<MainView>();
            mainView?.Show();
        }
    }

}
