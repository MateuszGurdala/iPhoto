using System.Windows;
using iPhoto.DataBase;
using iPhoto.ViewModels;
using iPhoto.Views;
using iPhoto.Views.SearchPage;

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

            MainWindow = new MainWindow();
            _databaseHandler = new DatabaseHandler();   //MG 15.04
            MainWindow.DataContext = new MainWindowViewModel(MainWindow, _databaseHandler); //MG 15.04
            var sideWindowView = new PhotoDetailsWindowView();

            //_navigationStore.CurrentViewModel = null;
            // MainWindow.DataContext = new MainWindowViewModel(_navigationStore);
            MainWindow.Show();
            sideWindowView.Show();
            base.OnStartup(e);
        }
    }
}
