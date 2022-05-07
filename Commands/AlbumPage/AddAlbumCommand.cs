using iPhoto.DataBase;
using iPhoto.ViewModels;
using iPhoto.Views.AlbumPage;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace iPhoto.Commands.AlbumPage
{
    public class AddAlbumCommand : CommandBase
    {
        private readonly DatabaseHandler _databaseHandler;
        private readonly AlbumsViewModel _albumViewModel;
        public AddAlbumCommand(DatabaseHandler databaseHandler, AlbumsViewModel albumViewModel)
        {
            _databaseHandler = databaseHandler;
            _albumViewModel = albumViewModel;
        }
        public override void Execute(object parameter)
        {
            var view = parameter as AddAlbumView;

            string albumColor = ((Rectangle)view.AlbumColorsComboBox.SelectedItem).Name;
            string albumName = view.AlbumName.Text;
            _databaseHandler.AddAlbum(albumName, 0, null, DateTime.Now, true, albumColor);
            _albumViewModel.AddAlbumToView(_databaseHandler.Albums.Last());
        }
    }
}
