using System;
using System.Collections.ObjectModel;
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

            //DELETE THIS - ONLY FOR TESTING PURPOSES
            PhotoSearchResultsCollection.Add(new PhotoSearchResultViewModel(DataHandler.GetTestImagesDirectory() + "\\test.jpg", PhotoSearchResultsCollection));
        }
    }
}
