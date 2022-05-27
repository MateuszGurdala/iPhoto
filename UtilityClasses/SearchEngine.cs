using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iPhoto.DataBase;
using iPhoto.Models;
using iPhoto.RemoteDatabase;
using iPhoto.ViewModels;

namespace iPhoto.UtilityClasses
{
    public class SearchEngine
    {
        private readonly DatabaseHandler _databaseHandler;
        private readonly RemoteDatabaseHandler _remoteDatabaseHandler;
        private readonly IPhotoSearchVM _searchViewModel;

        private SearchParams _searchParams;

        private bool _newDataLoaded;
        private bool _localSearch = true;
        private bool _onlineSearch = false;

        public SearchEngine(DatabaseHandler databaseHandler, RemoteDatabaseHandler remoteDatabaseHandler, IPhotoSearchVM viewModel)
        {
            _databaseHandler = databaseHandler;
            _searchViewModel = viewModel;
            _remoteDatabaseHandler = remoteDatabaseHandler;
        }
        public void LoadParams(SearchParams searchParams)
        {
            if (searchParams != _searchParams && searchParams.IsNull() != true)
            {
                _searchParams = searchParams;
                _newDataLoaded = true;
            }
        }
        public async void GetSearchResults(bool localSearch, bool onlineSearch)
        {
            _localSearch = localSearch;
            _onlineSearch = onlineSearch;
            if (_newDataLoaded)
            {
                _searchViewModel.PhotoSearchResultsCollection.Clear();
                if (localSearch)
                {
                    var firstSearch = FirstSearch(_databaseHandler);
                    var secondSearch = SecondSearch(firstSearch);
                    var thirdSearch = ThirdSearch(secondSearch);

                    foreach (var photo in thirdSearch)
                    {
                        _searchViewModel.PhotoSearchResultsCollection.Add(GetViewModel(photo.Id, "Local"));
                        await Task.Delay(10);
                    }
                }

                if (onlineSearch)
                {
                    var firstSearch = FirstSearch(_remoteDatabaseHandler);
                    var secondSearch = SecondSearch(firstSearch);
                    var thirdSearch = ThirdSearch(secondSearch);

                    foreach (var photo in thirdSearch)
                    {
                        _searchViewModel.PhotoSearchResultsCollection.Add(GetViewModel(photo.Id, "Remote"));
                        await Task.Delay(10);
                    }
                }

            }
        }
        public void UpdateSearchResults()
        {
            _searchViewModel.PhotoSearchResultsCollection.Clear();
            if (_searchParams.GetTitleParam() != null && _searchParams.GetTitleParam() == "%ALL")
            {
                LoadAllPhotos();
            }
            else
            {
                GetSearchResults(_localSearch,  _onlineSearch);
            }
        }
        private PhotoSearchResultViewModel GetViewModel(int photoId, string database)
        {
            Photo photo;
            Album album;
            Image image;
            Place place;
            if (database == "Local")
            {
                photo = _databaseHandler.Photos.FirstOrDefault(e => e.Id == photoId);
                image = _databaseHandler.Images.FirstOrDefault(e => e.Id == photo!.ImageId);
                album = _databaseHandler.Albums.FirstOrDefault(e => e.Id == photo!.AlbumId);
                place = _databaseHandler.Places.FirstOrDefault(e => e.Id == photo!.PlaceId);
            }
            else
            {
                photo = _remoteDatabaseHandler.Photos.FirstOrDefault(e => e.Id == photoId);
                image = _remoteDatabaseHandler.Images.FirstOrDefault(e => e.Id == photo!.ImageId);
                album = _remoteDatabaseHandler.Albums.FirstOrDefault(e => e.Id == photo!.AlbumId);
                place = _remoteDatabaseHandler.Places.FirstOrDefault(e => e.Id == photo!.PlaceId);
            }

            return new PhotoSearchResultViewModel(photo!, image!, album!, place!, _searchViewModel);
        }
        private List<Photo> FirstSearch(DatabaseHandler database)
        {
            //First Search
            //Filtering all photos through album and place Ids params
            Album album = null;
            Place place = null;
            var photos = database.Photos;

            //Searching through albums
            if (_searchParams.GetAlbumParam() != null)
            {
                album = database.Albums.FirstOrDefault(e => e.Name == _searchParams.GetAlbumParam());
                if (album == null)
                {
                    return new List<Photo>();
                }
                photos = photos.FindAll(e => e.AlbumId == album.Id);
            }
            //Searching through places
            if (_searchParams.GetLocationParam() != null)
            {
                place = database.Places.FirstOrDefault(e => e.Name == _searchParams.GetLocationParam());
                if (place == null)
                {
                    return new List<Photo>();
                }
                photos = photos.FindAll(e => e.PlaceId == place.Id);
            }
            return photos;
        }
        private List<Photo> FirstSearch(RemoteDatabaseHandler database)
        {
            //First Search
            //Filtering all photos through album and place Ids params
            Album album = null;
            Place place = null;
            var photos = database.Photos;

            //Searching through albums
            if (_searchParams.GetAlbumParam() != null)
            {
                album = database.Albums.FirstOrDefault(e => e.Name == _searchParams.GetAlbumParam());
                if (album == null)
                {
                    return new List<Photo>();
                }
                photos = photos.FindAll(e => e.AlbumId == album.Id);
            }
            //Searching through places
            if (_searchParams.GetLocationParam() != null)
            {
                place = database.Places.FirstOrDefault(e => e.Name == _searchParams.GetLocationParam());
                if (place == null)
                {
                    return new List<Photo>();
                }
                photos = photos.FindAll(e => e.PlaceId == place.Id);
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
        private List<Photo> ThirdSearch(List<Photo> secondSearchResults)
        {
            if (_searchParams.GetTitleParam() != null)
            {
                secondSearchResults = secondSearchResults.FindAll(e =>
                    e.Title.ToLower().Contains(_searchParams.GetTitleParam().ToLower()));
            }

            if (_searchParams.GetTagsParam() != null)
            {
                var tags = _searchParams.GetTagsParam().Split('#');
                foreach (var tag in tags)
                {
                    secondSearchResults = secondSearchResults.FindAll(e =>
                        e.RawTags.ToLower().Contains('#' + tag.ToLower()));
                }
            }

            return secondSearchResults;
        }
        public async void LoadAllPhotos()
        {
            _searchViewModel.PhotoSearchResultsCollection.Clear();

            foreach (var photo in _searchViewModel.DatabaseHandler.Photos)
            {
                var image = _databaseHandler.Images.First(e => e.Id == photo.ImageId);
                var album = _databaseHandler.Albums.First(e => e.Id == photo.AlbumId);
                var place = _databaseHandler.Places.First(e => e.Id == photo.PlaceId);
                _searchViewModel.PhotoSearchResultsCollection.Add(new PhotoSearchResultViewModel(photo, image, album, place, _searchViewModel));
                await Task.Delay(10);
            }
        }
    }
}
