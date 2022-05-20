using iPhoto.DataBase;
using iPhoto.Models;
using iPhoto.UtilityClasses;
using iPhoto.ViewModels;
using iPhoto.ViewModels.AlbumsPage;
using iPhoto.Views;

namespace iPhoto.Commands
{
    public class SearchCommand : CommandBase
    {
        private readonly SearchEngine _searchEngine;
        private readonly AlbumContentViewModel _currentAlbumVM;
        public SearchCommand(SearchEngine searchEngine)
        {
            _searchEngine = searchEngine;
        }
        public SearchCommand(SearchEngine searchEngine, AlbumContentViewModel currentAlbumVM)
        {
            _searchEngine = searchEngine;
            _currentAlbumVM = currentAlbumVM;
        }
        public override void Execute(object parameter)
        {
            var photoSearchOptions = parameter as PhotoSearchOptionsView;
            var searchData = new SearchParams(photoSearchOptions!);

            //MG 27.04 Implemented Search Engine
            if (searchData.GetTitleParam() == "%ALL")
            {
                if (_currentAlbumVM == null)
                    _searchEngine.LoadAllPhotos();
                else
                    _currentAlbumVM.LoadAllAlbumPhotos();
                _searchEngine.LoadParams(searchData);
            }
            else
            {
                _searchEngine.LoadParams(searchData);

                var accountViewModel = (App.Current.MainWindow.DataContext as MainWindowViewModel).AccountViewModel;

                if (accountViewModel.CurrentViewModel == accountViewModel.LoggedInViewModel)
                {
                    _searchEngine.GetSearchResults(true, true);
                }
                else
                {
                    _searchEngine.GetSearchResults(true, false);
                }
            }
        }
    }
}
