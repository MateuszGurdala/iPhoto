using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using iPhoto.Commands;
using iPhoto.UtilityClasses;

namespace iPhoto.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        public ICommand SearchCommand { get; }
        public ICommand ExtendSearchMenuCommand { get; }
        public ICommand LoseFocusCommand { get; }
        public ObservableCollection<PhotoSearchResultViewModel> PhotoSearchResultsCollection { get; }
        public SearchViewModel()
        {
            PhotoSearchResultsCollection = new ObservableCollection<PhotoSearchResultViewModel>();

            SearchCommand = new SearchCommand(PhotoSearchResultsCollection);
            ExtendSearchMenuCommand = new ExtendSearchMenuCommand();

            //DELETE THIS
            PhotoSearchResultsCollection.Add(new PhotoSearchResultViewModel(DataHandler.GetTestImagesDirectory() + "\\test.jpg", PhotoSearchResultsCollection));
        }
    }
}
