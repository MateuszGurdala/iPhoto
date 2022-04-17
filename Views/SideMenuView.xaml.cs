using iPhoto.ViewModels;
using iPhoto.Views.UserControls;
using System.Windows.Controls;

namespace iPhoto.Views
{
    public partial class SideMenuView : UserControl
    {
        public SideMenuView()
        {
            InitializeComponent();
            SideMenuButton[] ButtonsList = { HomeButton, SearchButton, SearchButton, AccountButton, PlacesButton,SettingsButton, AlbumButton };
            DataContext = new SideMenuViewModel(this, ButtonsList);
            //MG 16.04 made home button clicked on startup
            HomeButton.Button_MouseEnter(HomeButton, null);
            HomeButton.AnimateClicked();
            HomeButton.LastClicked = true;
        }
    }
}
