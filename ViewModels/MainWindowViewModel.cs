using iPhoto.Commands;
using System.Windows;
using iPhoto.DataBase;
using iPhoto.Views.SearchPage;
using iPhoto.ViewModels.AlbumsPage;
using iPhoto.RemoteDatabase;

namespace iPhoto.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private MainWindow? _mainWindow;

        private ViewModelBase? _mainViewModel;
        public ViewModelBase MainViewModel
        {
            get
            {
                return _mainViewModel;
            }
            set
            {
                _mainViewModel = value;
                OnPropertyChanged(nameof(MainViewModel));
            }
        }
        public SearchViewModel SearchViewModel { get; }
        public AlbumViewModel AlbumsViewModel { get; }
        public PlacesViewModel PlacesViewModel { get; }
        public AccountViewModel AccountViewModel { get; }
        public SettingsViewModel SettingsViewModel { get; }
        public HomePageViewModel HomePageViewModel { get; }
        public SideMenuViewModel SideMenuViewModel { get; set; }

        //MG 15.04 added db handler class
        public MainWindowViewModel(Window mainWindow, DatabaseHandler database, RemoteDatabaseHandler remoteDatabase, PhotoDetailsWindowView photoDetailsWindow)
        {
            HomePageViewModel = new HomePageViewModel();
            SearchViewModel = new SearchViewModel(database, remoteDatabase, photoDetailsWindow);    //MG 15.04 //MG 26.04 add photo details 
            AlbumsViewModel = new AlbumViewModel(database, this, photoDetailsWindow);
            PlacesViewModel = new PlacesViewModel(database);
            AccountViewModel = new AccountViewModel(remoteDatabase);
            SettingsViewModel = new SettingsViewModel();


            BindMainWindow(mainWindow);

            MainViewModel = HomePageViewModel;
        }
        private void BindMainWindow(Window mainWindow)
        {
            _mainWindow = mainWindow as MainWindow;

            SideMenuViewModel = _mainWindow!.sideMenu.DataContext as SideMenuViewModel;

            SideMenuViewModel!.NavigateCommand = new NavigateCommand(this);
        }
    }
}
