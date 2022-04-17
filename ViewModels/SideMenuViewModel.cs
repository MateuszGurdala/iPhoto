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
        
        private BitmapImage _extendImage;
        public BitmapImage ExtendImage
        {
            get
            {
                return _extendImage;
            }
            set
            {
                _extendImage = value;
                OnPropertyChanged(nameof(ExtendImage));
            }
        }

        private Button _lastClicked;

        public Button LastClicked
        {
            get { return _lastClicked; }
            set { _lastClicked = value; }
        }

        public BitmapImage ExtendImageSource { get; } =
            DataHandler.LoadBitmapImage(DataHandler.GetSideMenuIconsDirectoryPath() + "Extend.png", 100);
        public BitmapImage HideImageSource { get; } = 
            DataHandler.LoadBitmapImage(DataHandler.GetSideMenuIconsDirectoryPath() + "Hide.png", 100);
        //Commands
        public ICommand ExtendCommand { get; set; }
        public ICommand NavigateCommand { get; set; }
        public ICommand LastClickedCommand { get; set; }
        //Navigate Parameters
        public string HomeParameter { get; } = "Home";
        public string SearchParameter { get; } = "Search";
        public string AlbumsParameter { get; } = "Albums";
        public string PlacesParameter { get; } = "Places";
        public string AccountParameter { get; } = "Account";
        public string SettingsParameter { get; } = "Settings";

        public SideMenuViewModel(SideMenuView sideMenu, SideMenuButton[] buttonList)
        {
            ExtendCommand = new ExtendSideMenuCommand(sideMenu, this);
            ExtendImage = ExtendImageSource;
            LastClickedCommand = new LastClickedCommand(buttonList);
        }
    }
}
