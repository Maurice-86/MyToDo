using Microsoft.Extensions.DependencyInjection;
using MvvmDialogs;
using MyToDo.Common.Extensions;
using MyToDo.Services.Core;
using System.Windows;

namespace MyToDo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public int Uid { get; set; }
        public string? Username { get; set; }
        public IServiceProvider Services { get; }
        public static new App Current => (App)Application.Current;

        public App()
        {
            var container = new ServiceCollection();

            Services = container.AddNavigations()   // 添加导航服务
                     .AddServices()    // 添加服务
                     .AddViews()    // 添加视图
                     .AddViewModels()    // 添加视图模型
                     .AddSingleton<IDialogService, DialogService>()    // 对话框服务
                     .AddSingleton<UIStateService>()    // 用户界面的状态管理，包括遮罩、加载动画、进度条等
                     .BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainView = Services.GetService<MainView>();
            mainView?.Show();
        }
    }

}
