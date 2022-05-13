using iPhoto.Models;
using iPhoto.UtilityClasses;
using iPhoto.ViewModels;
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
        private readonly SearchAlbumEngine _searchAlbumEngine;
        private readonly AlbumSearchView _albumSearchView;
        public SearchAlbumsCommand(AlbumViewModel albumViewModel)
        {
            _searchAlbumEngine = albumViewModel.SearchAlbumEngine;
            _albumSearchView = albumViewModel.AlbumSearchView;
        }

        public override void Execute(object parameter)
        {
            _searchAlbumEngine.SearchAlbums(new AlbumSearchParams(_albumSearchView));
        }
    }
}
