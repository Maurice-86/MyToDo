using MyToDo.Common.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyToDo.Views
{
    /// <summary>
    /// HomeView.xaml 的交互逻辑
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();

            ColorZone.MouseMove += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    WindowExtension.DragMove();
            };

            MinimizeButton.Click += (s, e) => WindowExtension.WindowState = WindowState.Minimized;

            MaximizeButton.Click += (s, e) =>
            {
                if (WindowExtension.WindowState == WindowState.Normal)
                    WindowExtension.WindowState = WindowState.Maximized;
                else
                    WindowExtension.WindowState = WindowState.Normal;
            };

            CloseButton.Click += (s, e) => WindowExtension.Close();
        }
    }
}
