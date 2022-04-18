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
        private readonly SearchViewModel _searchViewModel;
        public SearchCommand(SearchViewModel searchViewModel)
        {
            _searchViewModel = searchViewModel;
        }
        public override void Execute(object parameter)
        {
            var photoSearchOptions = parameter as PhotoSearchOptionsView;
            var searchData = new SearchData(photoSearchOptions!);

            _searchViewModel.PhotoSearchResultsCollection.Clear();

            if (searchData.Title == "%ALL")
            {
                SearchAllPhotos();
            }
        }
        //MG 17.04 
        private async void SearchAllPhotos()
        {
            foreach (var photo in _searchViewModel.DatabaseHandler.Photos)
            {
                var image = _searchViewModel.DatabaseHandler.Images.First(e => e.Id == photo.ImageId);
                var album = _searchViewModel.DatabaseHandler.Albums.First(e => e.Id == photo.AlbumId);
                var place = _searchViewModel.DatabaseHandler.Places.First(e => e.Id == photo.PlaceId);
                _searchViewModel.PhotoSearchResultsCollection.Add(new PhotoSearchResultViewModel(photo, image, album, place, _searchViewModel));
                await Task.Delay(10);
            }
        }
    }
}
