using iPhoto.Commands.AlbumPage;
using iPhoto.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iPhoto.ViewModels.AlbumsPage
{
    public class AddAlbumViewModel : ViewModelBase
    {
        public ICommand AddAlbumCommand { get; }
        public AddAlbumViewModel(DatabaseHandler databaseHandler, AlbumViewModel albumsViewModel )
        {
            AddAlbumCommand = new AddAlbumCommand(databaseHandler, albumsViewModel);
        }
    }
}
