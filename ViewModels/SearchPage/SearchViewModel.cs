using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using iPhoto.Commands;
using iPhoto.DataBase;
using iPhoto.UtilityClasses;

namespace iPhoto.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        public ICommand SearchCommand { get; }
        public ICommand ExtendSearchMenuCommand { get; }
        public ObservableCollection<PhotoSearchResultViewModel> PhotoSearchResultsCollection { get; }
        //MG 15.04 added db handler class
        private DatabaseHandler _databaseHandler;

        public SearchViewModel(DatabaseHandler database)
        {
            PhotoSearchResultsCollection = new ObservableCollection<PhotoSearchResultViewModel>();
            _databaseHandler = database;

            SearchCommand = new SearchCommand(PhotoSearchResultsCollection);
            ExtendSearchMenuCommand = new ExtendSearchMenuCommand();

            //MG 16.04
            //DELETE THIS - ONLY FOR TESTING PURPOSES
            var photo = _databaseHandler.Photos.FirstOrDefault(e => e.Id == 1);
            var image = _databaseHandler.Images.FirstOrDefault(e => e.Id == photo!.ImageId);
            var album = _databaseHandler.Albums.FirstOrDefault(e => e.Id == photo!.AlbumId);
            var place = _databaseHandler.Places.FirstOrDefault(e => e.Id == photo!.PlaceId);
            PhotoSearchResultsCollection.Add(new PhotoSearchResultViewModel(photo!, image!, album!, place!, PhotoSearchResultsCollection));
            //~MG 16.04
        }
    }
}
