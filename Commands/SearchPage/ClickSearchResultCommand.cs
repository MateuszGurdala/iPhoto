using System.Collections.ObjectModel;
using iPhoto.ViewModels;
using iPhoto.ViewModels.SearchPage;

namespace iPhoto.Commands
{
    public class ClickSearchResultCommand : CommandBase
    {
        private readonly ObservableCollection<PhotoSearchResultViewModel> _searchResults;
        private readonly PhotoDetailsViewModel _photoDetails;

        public ClickSearchResultCommand(ObservableCollection<PhotoSearchResultViewModel> searchResults, PhotoDetailsViewModel photoDetails)
        {
            _searchResults = searchResults;
            _photoDetails = photoDetails;
        }
        public override void Execute(object? parameter)
        {
            foreach (var photoSearchResultViewModel in _searchResults)
            {
                photoSearchResultViewModel.IsClicked = false;
            }

            var photoViewModel = (parameter as PhotoSearchResultViewModel)!;
            photoViewModel.IsClicked = true;
            PassDetails(photoViewModel);
        }

        private void PassDetails(PhotoSearchResultViewModel photoViewModel)
        {
            var photoData = photoViewModel.GetPhotoData();
            _photoDetails.Title = photoData.PhotoData.Title;
            _photoDetails.Album = photoData.AlbumData.Name;
            _photoDetails.Tags = photoData.PhotoData.RawTags;
            _photoDetails.CreationDate = photoData.PhotoData.DateTaken.ToShortDateString();
            _photoDetails.PlaceTaken = photoData.PlaceData.Name;
            _photoDetails.Source = photoData.ImageData.Source;
            _photoDetails.ResolutionWidth = photoData.ImageData.ResolutionWidth;
            _photoDetails.ResolutionHeight = photoData.ImageData.ResolutionHeight;
            _photoDetails.MemorySize = photoData.ImageData.Size.ToString();
        }
    }
}
