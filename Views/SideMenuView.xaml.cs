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
            SideMenuButton[] ButtonsList = { HomeButton, SearchButton, SearchButton, AccountButton, SettingsButton, AlbumButton };
            DataContext = new SideMenuViewModel(this, ButtonsList);
        }
    }
}
