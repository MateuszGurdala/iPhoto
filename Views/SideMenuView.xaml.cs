using iPhoto.ViewModels;
using System.Windows.Controls;

namespace iPhoto.Views
{
    public partial class SideMenuView : UserControl
    {
        public SideMenuView()
        {
            InitializeComponent();
            DataContext = new SideMenuViewModel(this);
        }
    }
}
