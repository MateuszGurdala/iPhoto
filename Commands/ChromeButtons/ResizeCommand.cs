using System.Windows;

namespace iPhoto.Commands
{
    public class ResizeCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var dimensions = parameter as int[];
            Application.Current.MainWindow.WindowState = WindowState.Normal;
            Application.Current.MainWindow.Height = dimensions[1];
            Application.Current.MainWindow.Width = dimensions[0];
            Application.Current.MainWindow.Left = (1920 - dimensions[0]) / 2;
            Application.Current.MainWindow.Top = (1080 - dimensions[1]) / 2;
        }
    }
}
