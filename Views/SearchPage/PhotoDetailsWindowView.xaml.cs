using System.Windows;
using System.Windows.Input;

namespace iPhoto.Views.SearchPage
{
    public partial class PhotoDetailsWindowView : Window
    {
        public PhotoDetailsWindowView()
        {
            InitializeComponent();
        }
        private void Hide(object sender, MouseButtonEventArgs e)
        {
            Hide();
        }
    }
}
