using System.Windows;
using GoogleDriveHandlerDemo;
using iPhoto.DataBase;
using iPhoto.ViewModels;
using iPhoto.Views.SearchPage;

namespace iPhoto
{
    public partial class App : Application
    {
        //MG 15.04  Added db handler class
        private DatabaseHandler? _databaseHandler;

        //MG 15.05 Added GoogleDriveHandler + deleted commented NavigationService lines
        private GoogleDriveHandler? _googleDriveHandler;
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow();

            //MG 26.04 Added window object, it will be moved to different model after I solve datacontext override bug
            var photoDetailsWindow = new PhotoDetailsWindowView();
            _databaseHandler = new DatabaseHandler();   //MG 15.04
            MainWindow.DataContext = new MainWindowViewModel(MainWindow, _databaseHandler, photoDetailsWindow); //MG 15.04

            //MG 15.05 
            _googleDriveHandler = new GoogleDriveHandler();

            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
