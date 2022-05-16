using iPhoto.Commands;
using iPhoto.UtilityClasses;
using iPhoto.Views;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using iPhoto.Views.UserControls;

namespace iPhoto.ViewModels
{
    public class SideMenuViewModel : ViewModelBase
    {
        
        private Button _lastClicked;

        public Button LastClicked
        {
            get { return _lastClicked; }
            set { _lastClicked = value; }
        }

        //Commands
        public ICommand NavigateCommand { get; set; }
        public ICommand LastClickedCommand { get; set; }
        //Navigate Parameters
        public string HomeParameter { get; } = "Home";
        public string SearchParameter { get; } = "Search";
        public string AlbumsParameter { get; } = "Albums";
        public string PlacesParameter { get; } = "Places";
        public string AccountParameter { get; } = "Account";
        public string SettingsParameter { get; } = "Settings";
        //Icons
        public BitmapImage HomeImage { get; } = DataHandler.LoadBitmapImage(DataHandler.GetSideMenuIconsDirectoryPath() + "Home.png", 100);
        public BitmapImage SearchImage { get; } = DataHandler.LoadBitmapImage(DataHandler.GetSideMenuIconsDirectoryPath() + "Search.png", 100);
        public BitmapImage AlbumsImage { get; } = DataHandler.LoadBitmapImage(DataHandler.GetSideMenuIconsDirectoryPath() + "Albums.png", 100);
        public BitmapImage PlacesImage { get; } = DataHandler.LoadBitmapImage(DataHandler.GetSideMenuIconsDirectoryPath() + "Place.png", 100);
        public BitmapImage AccountImage { get; } = DataHandler.LoadBitmapImage(DataHandler.GetSideMenuIconsDirectoryPath() + "Account.png", 100);
        public BitmapImage SettingsImage { get; } = DataHandler.LoadBitmapImage(DataHandler.GetSideMenuIconsDirectoryPath() + "Settings.png", 100);

        public SideMenuViewModel(SideMenuView sideMenu, SideMenuButton[] buttonList)
        {
            LastClickedCommand = new LastClickedCommand(buttonList);
        }
    }
}
