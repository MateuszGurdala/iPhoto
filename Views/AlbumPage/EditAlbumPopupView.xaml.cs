using iPhoto.DataBase;
using iPhoto.ViewModels.AlbumsPage;
using System.Linq;
using System.Windows.Controls.Primitives;
using System.Windows.Shapes;

namespace iPhoto.Views.AlbumPage
{
    public partial class EditAlbumPopupView : Popup
    {
        private readonly EditAlbumPopupViewModel _editAlbumPopupViewModel;
        private readonly DatabaseHandler _databaseHandler;
        public EditAlbumPopupView(EditAlbumPopupViewModel editAlbumPopupViewModel, DatabaseHandler databaseHandler)
        {
            _editAlbumPopupViewModel = editAlbumPopupViewModel;
            _databaseHandler = databaseHandler;
            InitializeComponent();
            DataContext = editAlbumPopupViewModel;
            
        }

        private void ColorsComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _editAlbumPopupViewModel.Album.ColorGroup = ((Rectangle)ColorsComboBox.SelectedItem).Name;
            PreviewAlbum.Source = _editAlbumPopupViewModel.AlbumImageSource;
        }

        private void PhotosList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _editAlbumPopupViewModel.Album.CoverPhotoId = _databaseHandler.Photos.FirstOrDefault(e => e.Title == (string)PhotosComboBox.SelectedItem).ImageId;
            PreviewCover.Source = _editAlbumPopupViewModel.CoverImageSource;
        }
    }
}
