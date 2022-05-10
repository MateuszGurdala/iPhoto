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

        /// <summary>
        /// Adds new album to database and to view  
        /// </summary>
        /// <param name="databaseHandler"> database where album entity will be added </param>
        /// <param name="albumViewModel"> album view where new album will be displayed</param>
        public AddAlbumCommand(DatabaseHandler databaseHandler, AlbumsViewModel albumViewModel)
        {
            _databaseHandler = databaseHandler;
            _albumViewModel = albumViewModel;
        }
        /// <summary>
        /// execute command and Add album 
        /// </summary>
        /// <param name="parameter"> AlbumView where new album is displayed </param>
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
