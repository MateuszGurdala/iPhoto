using iPhoto.DataBase;
using iPhoto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPhoto.Commands.AlbumPage
{
    public class NavigateBackToAlbumsCommand : CommandBase
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly AlbumViewModel _albumViewModel;
        private readonly Album _currentAlbum;
        public NavigateBackToAlbumsCommand(MainWindowViewModel mainWindowViewModel, AlbumViewModel albumViewModel, Album currentAlbum)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _albumViewModel = albumViewModel;
            _currentAlbum = currentAlbum;
        }
        public override void Execute(object parameter)
        {
            _albumViewModel.DisplayAllAlbums();
            _mainWindowViewModel.MainViewModel = _mainWindowViewModel.AlbumsViewModel;

        }
    }
}
