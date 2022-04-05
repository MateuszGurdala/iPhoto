using iPhoto.ViewModels;
using System.Windows.Controls;

namespace iPhoto.Views
{
    /// <summary>
    /// Interaction logic for ChromeButtonsView.xaml
    /// </summary>
    public partial class ChromeButtonsView : UserControl
    {
        public ChromeButtonsView()
        {
            InitializeComponent();
            DataContext = new UtilityButtonsViewModel();
        }
    }
}
