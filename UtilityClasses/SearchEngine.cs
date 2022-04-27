using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iPhoto.DataBase;
using iPhoto.Models;
using iPhoto.ViewModels;

namespace iPhoto.UtilityClasses
{
    public class SearchEngine
    {
        private readonly DatabaseHandler _databaseHandler;
        private readonly SearchViewModel _searchViewModel;

        private SearchParams _searchParams;
        private ObservableCollection<PhotoSearchResultModel> _searchResults;

        private bool _newDataLoaded;

        public SearchEngine(DatabaseHandler databaseHandler, SearchViewModel viewModel)
        {
            _databaseHandler = databaseHandler;
            _searchViewModel = viewModel;


        }
        public void LoadParams(SearchParams searchParams)
        {
            if (searchParams != _searchParams)
            {
                _searchParams = searchParams;
                _newDataLoaded = true;
            }
        }
        public void GetSearchResults()
        {
            if (_newDataLoaded)
            {
                _searchViewModel.PhotoSearchResultsCollection.Clear();

                var firstSearch = FirstSearch();
                var secondSearch = SecondSearch(firstSearch);

                foreach (var photo in secondSearch)
                {
                    _searchViewModel.PhotoSearchResultsCollection.Add(GetViewModel(photo.Id));
                }
            }
        }
        private PhotoSearchResultViewModel GetViewModel(int photoId)
        {
            var photo = _databaseHandler.Photos.FirstOrDefault(e => e.Id == photoId);
            var image = _databaseHandler.Images.FirstOrDefault(e => e.Id == photo!.ImageId);
            var album = _databaseHandler.Albums.FirstOrDefault(e => e.Id == photo!.AlbumId);
            var place = _databaseHandler.Places.FirstOrDefault(e => e.Id == photo!.PlaceId);
            return new PhotoSearchResultViewModel(photo!, image!, album!, place!, _searchViewModel);
        }
        private List<Photo> FirstSearch()
        {
            //First Search
            //Filtering all photos through album and place Ids params
            Album album = null;
            Place place = null;
            var photos = _databaseHandler.Photos;

            //Searching through albums
            if (_searchParams.GetAlbumParam() != null)
            {
                album = _databaseHandler.Albums.FirstOrDefault(e => e.Name == _searchParams.GetAlbumParam());
                if (album != null)
                {
                    photos = photos.FindAll(e => e.AlbumId == album.Id);
                }
            }
            //Searching through places
            if (_searchParams.GetLocationParam() != null)
            {
                place = _databaseHandler.Places.FirstOrDefault(e => e.Name == _searchParams.GetLocationParam());
                if (place != null)
                {
                    photos = photos.FindAll(e => e.PlaceId == place.Id);
                }
            }
            return photos;
        }
        private List<Photo> SecondSearch(List<Photo> firstSearchResults)
        {
            bool invalidStartDateFormat = false;
            bool invalidEndDateFormat = false;

            if (_searchParams.GetStartDateParam() != null)
            {
                DateTime startDate = default;
                try
                {
                    startDate = DateTime.Parse(_searchParams.GetStartDateParam());

                }
                catch (Exception e)
                {
                    invalidStartDateFormat = true;
                }

                if (!invalidStartDateFormat)
                {
                    firstSearchResults = firstSearchResults.FindAll(e => DateTime.Compare(e.DateTaken, startDate) >= 0);
                }
            }

            if (_searchParams.GetEndDateParam() != null)
            {
                DateTime endDate = default;
                try
                {
                    endDate = DateTime.Parse(_searchParams.GetEndDateParam());

                }
                catch (Exception e)
                {
                    invalidEndDateFormat = true;
                }

                if (!invalidEndDateFormat)
                {
                    firstSearchResults = firstSearchResults.FindAll(e => DateTime.Compare(e.DateTaken, endDate) <= 0);
                }
            }
            return firstSearchResults;
        }
    }
}
