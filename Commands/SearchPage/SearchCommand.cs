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
                _searchEngine.LoadAllPhotos();
            }
            else
            {
                _searchEngine.LoadParams(searchData);
                _searchEngine.GetSearchResults();
            }
        }
    }
}
