using System.Windows;
using iPhoto.UtilityClasses;
using iPhoto.ViewModels;

namespace iPhoto
{
    public partial class App : Application
    {
        /*private readonly NavigationStore _navigationStore;
        public App()
        {
            _navigationStore = new NavigationStore();
        }*/
        protected override void OnStartup(StartupEventArgs e)
        {
            //_navigationStore.CurrentViewModel = null;
            MainWindow = new MainWindow();
            MainWindow.DataContext = new MainWindowViewModel(MainWindow);
            // MainWindow.DataContext = new MainWindowViewModel(_navigationStore);
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
