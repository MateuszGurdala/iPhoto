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
            //MG 26.04 Added window object, it will be moved to different model after I solve datacontext override bug
            var photoDetailsWindow = new PhotoDetailsWindowView();
            _databaseHandler = new DatabaseHandler();   //MG 15.04
            MainWindow.DataContext = new MainWindowViewModel(MainWindow, _databaseHandler, photoDetailsWindow); //MG 15.04

            //_navigationStore.CurrentViewModel = null;
            // MainWindow.DataContext = new MainWindowViewModel(_navigationStore);
            MainWindow.Show();
            photoDetailsWindow.Show();
            base.OnStartup(e);
        }
    }
}
