using iPhoto.DataBase;
using iPhoto.ViewModels;
using iPhoto.Views.AlbumPage;
using System.Windows.Shapes;

namespace iPhoto.Commands.AlbumPage
{
    internal class UpdateAlbumCommand : CommandBase
    {

        private readonly AlbumViewModel _albumViewModel;
        private readonly Album _album;

        public UpdateAlbumCommand(AlbumViewModel albumViewModel, Album album)
        {
            _albumViewModel = albumViewModel;
            _album = album;

        }

        public override void Execute(object parameter)
        {
            EditAlbumPopupView view = parameter as EditAlbumPopupView;

            string newColorGroup = ((Rectangle)view.ColorsComboBox.SelectedItem).Name;
            string newName = view.Name.ContentTextBox.Text;
            _albumViewModel.DatabaseHandler.UpdateAlbum(_album.Id, newName, null, null, null, null, newColorGroup);
            _album.ColorGroup = newColorGroup;
            _album.Name = newName;
            _albumViewModel.ReloadAlbumView(_album);
            view.IsOpen = false;
        }
    }
}
