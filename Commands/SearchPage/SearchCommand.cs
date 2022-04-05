using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using iPhoto.Models;
using iPhoto.UtilityClasses;
using iPhoto.ViewModels;
using iPhoto.Views;

namespace iPhoto.Commands
{
    public class SearchCommand : CommandBase
    {
        private readonly ObservableCollection<PhotoSearchResultViewModel> _searchResults;

        public SearchCommand(ObservableCollection<PhotoSearchResultViewModel> searchResults)
        {
            _searchResults = searchResults;
        }
        public override void Execute(object parameter)
        {
            var photoSearchOptions = parameter as PhotoSearchOptionsView;
            var searchData = new SearchData(photoSearchOptions);

            _searchResults.Clear();

            if (searchData.Title == "#TEST")
            {
                SearchTestImages();
            }
        }
        private async void SearchTestImages()
        {
            var photoUris = Directory.GetFiles(DataHandler.GetTestImagesDirectory());

            foreach (var uri in photoUris)
            {
                _searchResults.Add(new PhotoSearchResultViewModel(uri, _searchResults));
                await Task.Delay(1);
            }
        }
    }
}
