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
        public DatabaseHandler? DatabaseHandler;

        //MG 15.05 Added GoogleDriveHandler + RemoteDatabaseHandler + deleted commented NavigationService lines
        public RemoteDatabaseHandler? RemoteDatabaseHandler;
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow();


            var photoDetailsWindow = new PhotoDetailsWindowView();

            DatabaseHandler = new DatabaseHandler();   //MG 15.04
            RemoteDatabaseHandler = new RemoteDatabaseHandler(); //MG 15.05
            RemotePhotoAdder.SetRemoteDatabaseHandler(RemoteDatabaseHandler); //MG 22.05

            MainWindow.DataContext = new MainWindowViewModel(MainWindow, DatabaseHandler, RemoteDatabaseHandler, photoDetailsWindow); //MG 15.04

            //MG 20.05 
            GoogleDriveHandler.CreateDriveService();

            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
