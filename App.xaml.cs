using System.Windows;
using GoogleDriveHandlerDemo;
using iPhoto.DataBase;
using iPhoto.RemoteDatabase;
using iPhoto.UtilityClasses;
using iPhoto.ViewModels;
using iPhoto.Views.SearchPage;

namespace iPhoto
{
    public partial class App : Application
    {
        //MG 15.04  Added db handler class
        private DatabaseHandler? _databaseHandler;

        //MG 15.05 Added GoogleDriveHandler + RemoteDatabaseHandler + deleted commented NavigationService lines
        private RemoteDatabaseHandler? _remoteDatabaseHandler;
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow();


            var photoDetailsWindow = new PhotoDetailsWindowView();

            _databaseHandler = new DatabaseHandler();   //MG 15.04
            _remoteDatabaseHandler = new RemoteDatabaseHandler(); //MG 15.05
            RemotePhotoAdder.SetRemoteDatabaseHandler(_remoteDatabaseHandler); //MG 22.05

            MainWindow.DataContext = new MainWindowViewModel(MainWindow, _databaseHandler, _remoteDatabaseHandler, photoDetailsWindow); //MG 15.04

            //MG 20.05 
            GoogleDriveHandler.CreateDriveService();

            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
