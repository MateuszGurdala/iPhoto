using iPhoto.DataBase;
using iPhoto.ViewModels;
using iPhoto.ViewModels.AlbumsPage;
using iPhoto.Views.AlbumPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPhoto.Commands.AlbumPage
{
    public class EditAlbumCommand : CommandBase
    {

        private readonly AlbumViewModel _albumViewModel;
        private readonly Album _album;

        public EditAlbumCommand(AlbumViewModel albumViewModel, Album album)
        {
             _albumViewModel = albumViewModel;
            _album = album;
            
        }
        public override void Execute(object parameter)
        {
            var _ = new EditAlbumPopupView(new EditAlbumPopupViewModel(_album, _albumViewModel));
        }
    }
}
