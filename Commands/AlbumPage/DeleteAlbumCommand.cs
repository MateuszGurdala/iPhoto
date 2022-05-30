using iPhoto.DataBase;
using iPhoto.ViewModels;
using iPhoto.ViewModels.AlbumsPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPhoto.Commands.AlbumPage
{
    public class DeleteAlbumCommand : CommandBase
    {
        private readonly AlbumViewModel _albumViewModel;
        private readonly Album _album;

        public DeleteAlbumCommand(AlbumViewModel albumViewModel, Album album)
        {
            _albumViewModel = albumViewModel;
            _album = album;
        }

        public override void Execute(object parameter)
        {
            if (_album.Name != "OtherPhotos")
            {
                _albumViewModel.DeleteAlbumFromView(_album);
                _albumViewModel.DatabaseHandler.RemoveAllAlbumPhotos(_album.Id);
                _albumViewModel.DatabaseHandler.RemoveAlbum(_album.Id);
            }
        }
    }
}
