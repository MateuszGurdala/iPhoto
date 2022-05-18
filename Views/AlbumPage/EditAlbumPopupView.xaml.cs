using iPhoto.DataBase;
using iPhoto.ViewModels.AlbumsPage;
using System.Windows.Controls.Primitives;
using System.Windows.Shapes;

namespace iPhoto.Views.AlbumPage
{
    public partial class EditAlbumPopupView : Popup
    {
        private readonly EditAlbumPopupViewModel _editAlbumPopupViewModel;
        public EditAlbumPopupView(EditAlbumPopupViewModel editAlbumPopupViewModel)
        {
            _editAlbumPopupViewModel = editAlbumPopupViewModel;
            InitializeComponent();
            DataContext = editAlbumPopupViewModel;
            
        }

        private void ColorsComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _editAlbumPopupViewModel.Album.ColorGroup = ((Rectangle)ColorsComboBox.SelectedItem).Name;
            PreviewAlbum.Source = _editAlbumPopupViewModel.AlbumImageSource;
        }
    }
}
