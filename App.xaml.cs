using System.Windows;
using iPhoto.DataBase;
using iPhoto.ViewModels;

namespace iPhoto
{
    public partial class App : Application
    {
        //MG 15.04  Added db handler class
        private DatabaseHandler? _databaseHandler;

        /*private readonly NavigationStore _navigationStore;
        public App()
        {
            _navigationStore = new NavigationStore();
        }*/
        protected override void OnStartup(StartupEventArgs e)
        {

            _databaseHandler = new DatabaseHandler();   //MG 15.04
            //_navigationStore.CurrentViewModel = null;
            MainWindow = new MainWindow();
            MainWindow.DataContext = new MainWindowViewModel(MainWindow, _databaseHandler); //MG 15.04
            // MainWindow.DataContext = new MainWindowViewModel(_navigationStore);
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
