using iPhoto.ViewModels.AlbumsPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPhoto.Commands.AlbumSearchResultOptions
{
    public class ShowAlbumDetailsCommand : CommandBase
    {
        private readonly ObservableCollection<AlbumSearchResultViewModel> _searchResults;
        private readonly AlbumDetailsViewModel _albumDetails;

        public ShowAlbumDetailsCommand(ObservableCollection<AlbumSearchResultViewModel> searchResults, AlbumDetailsViewModel albumDetails)
        {
            _searchResults = searchResults;
            _albumDetails = albumDetails;
        }
        public override void Execute(object? parameter)
        {
            foreach (var albumSearchResultViewModel in _searchResults)
            {
                albumSearchResultViewModel.IsClicked = false;
            }

            var photoViewModel = (parameter as AlbumSearchResultViewModel)!;
            photoViewModel.IsClicked = true;
            PassDetails(photoViewModel);
        }

        private void PassDetails(AlbumSearchResultViewModel albumViewModel)
        {
            
        }
    }
}
