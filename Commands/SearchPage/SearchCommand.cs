using System.Linq;
using System.Threading.Tasks;
using iPhoto.Models;
using iPhoto.UtilityClasses;
using iPhoto.ViewModels;
using iPhoto.Views;

namespace iPhoto.Commands
{
    public class SearchCommand : CommandBase
    {
        private readonly SearchViewModel _searchViewModel;
        private readonly SearchEngine _searchEngine;
        public SearchCommand(SearchViewModel searchViewModel, SearchEngine searchEngine)
        {
            _searchViewModel = searchViewModel;
            _searchEngine = searchEngine;
        }
        public override void Execute(object parameter)
        {
            var photoSearchOptions = parameter as PhotoSearchOptionsView;
            var searchData = new SearchParams(photoSearchOptions!);

            //MG 27.04 Implemented Search Engine
            if (searchData.GetTitleParam() == "%ALL")
            {
                SearchAllPhotos();
            }
            else
            {
                _searchEngine.LoadParams(searchData);
                _searchEngine.GetSearchResults();
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
