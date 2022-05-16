using iPhoto.Models;
using iPhoto.UtilityClasses;
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
    public class SearchAlbumsCommand : CommandBase
    {
        private readonly AlbumViewModel _albumViewModel;
        public SearchAlbumsCommand(AlbumViewModel albumViewModel)
        {
            _albumViewModel = albumViewModel;
        }

        public override void Execute(object parameter)
        {
            AlbumSearchView albumSearchView = (AlbumSearchView)parameter;
            var albumResult = _albumViewModel.SearchAlbumEngine.SearchAlbums(new AlbumSearchParams(albumSearchView));
            _albumViewModel.LoadGivenAlbums(albumResult);

        }
    }
}
