using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using iPhoto.DataBase;
using iPhoto.Models;
using iPhoto.ViewModels;
using iPhoto.Views;

namespace iPhoto.Commands
{
    public class SearchCommand : CommandBase
    {
        private readonly ObservableCollection<PhotoSearchResultViewModel> _searchResults;
        private readonly DatabaseHandler _databaseHandler;
        public SearchCommand(DatabaseHandler databaseHandler, ObservableCollection<PhotoSearchResultViewModel> searchResults)
        {
            _databaseHandler = databaseHandler;
            _searchResults = searchResults;
        }
        public override void Execute(object parameter)
        {
            var photoSearchOptions = parameter as PhotoSearchOptionsView;
            var searchData = new SearchData(photoSearchOptions!);

            _searchResults.Clear();

            if (searchData.Title == "%ALL")
            {
                SearchAllPhotos();
            }
        }
        //MG 17.04 
        private async void SearchAllPhotos()
        {
            foreach (var photo in _databaseHandler.Photos)
            {
                var image = _databaseHandler.Images.First(e => e.Id == photo.ImageId);
                var album = _databaseHandler.Albums.First(e => e.Id == photo.AlbumId);
                var place = _databaseHandler.Places.First(e => e.Id == photo.PlaceId);
                _searchResults.Add(new PhotoSearchResultViewModel(photo, image, album, place, _searchResults));
                await Task.Delay(10);
            }
        }
    }
}
