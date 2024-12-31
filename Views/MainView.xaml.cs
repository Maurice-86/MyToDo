using MyToDo.ViewModels;
using System.Windows;

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
        }
    }
}