using System.Windows;

namespace MyToDo.Common.Extensions
{
    public static class WindowExtension
    {
        private static readonly Window window = App.Current.MainWindow;

        public static WindowState WindowState
        {
            get => window.WindowState;
            set => window.WindowState = value;
        }

        public static void Close() => window.Close();

        public static void DragMove() => window.DragMove();

        public static void Hide() => window.Hide();

        public static void Show() => window.Show();

        public static void HideWindowBar()
            => window.WindowStyle = WindowStyle.None;

        public static void ShowWindowBar()
            => window.WindowStyle = WindowStyle.SingleBorderWindow;

        public static void ChangeWindwoSize(double width, double height)
        {
            window.Width = width;
            window.Height = height;

            // 获取工作区域的宽度和高度
            double workAreaWidth = SystemParameters.WorkArea.Width;
            double workAreaHeight = SystemParameters.WorkArea.Height;

            // 计算窗口左上角的坐标，使其居中
            double left = (workAreaWidth - window.Width) / 2;
            double top = (workAreaHeight - window.Height) / 2;

            // 设置窗口位置
            window.Left = left;
            window.Top = top;
        }
    }
}
