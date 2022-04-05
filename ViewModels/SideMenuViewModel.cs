using iPhoto.Commands;
using iPhoto.UtilityClasses;
using iPhoto.Views;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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

        public BitmapImage ExtendImageSource { get; } =
            DataHandler.LoadBitmapImage(DataHandler.GetSideMenuIconsDirectoryPath() + "Extend.png", 100);
        public BitmapImage HideImageSource { get; } = 
            DataHandler.LoadBitmapImage(DataHandler.GetSideMenuIconsDirectoryPath() + "Hide.png", 100);
        //Commands
        public ICommand ExtendCommand { get; set; }
        public ICommand NavigateCommand { get; set; }
        //Navigate Parameters
        public string HomeParameter { get; } = "Home";
        public string SearchParameter { get; } = "Search";
        public string AlbumsParameter { get; } = "Albums";
        public string AccountParameter { get; } = "Account";
        public string SettingsParameter { get; } = "Settings";

        public SideMenuViewModel(SideMenuView sideMenu)
        {
            ExtendCommand = new ExtendSideMenuCommand(sideMenu, this);
            ExtendImage = ExtendImageSource;
        }
    }
}
