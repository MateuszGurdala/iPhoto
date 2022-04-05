using System.Windows;
using iPhoto.ViewModels;

namespace iPhoto
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow();
            MainWindow.DataContext = new MainWindowViewModel(MainWindow);
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
