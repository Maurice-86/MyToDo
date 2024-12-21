using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using MyToDo.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static MaterialDesignThemes.Wpf.Theme;

namespace MyToDo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView(MainViewModel viewModel)
        {
            InitializeComponent();

            this.DataContext = viewModel;

            ColorZone.MouseMove += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    this.DragMove();
            };

            MinimizeButton.Click += (s, e) => this.WindowState = WindowState.Minimized;

            MaximizeButton.Click += (s, e) =>
            {
                if (this.WindowState == WindowState.Normal)
                    WindowState = WindowState.Maximized;
                else
                    WindowState = WindowState.Normal;
            };

            CloseButton.Click += (s, e) => this.Close();
        }
    }
}