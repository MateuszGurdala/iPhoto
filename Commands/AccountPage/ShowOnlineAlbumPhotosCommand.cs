using System;
using System.Linq;
using iPhoto.Models;
using iPhoto.ViewModels;
using iPhoto.ViewModels.AccountPage;
using iPhoto.Views.AccountPage;

namespace iPhoto.Commands.AccountPage
{
    public class ShowOnlineAlbumPhotosCommand : CommandBase
    {
        private MainWindowViewModel _mainWindowViewModel;
        private OnlineAlbumViewModel _album;
        public override void Execute(object parameter)
        {
            _album = parameter as OnlineAlbumViewModel;
            _mainWindowViewModel = App.Current.MainWindow.DataContext as MainWindowViewModel;

            SearchOnlinePhotos();
            ChangePage();
        }

        private void ChangePage()
        {
            _mainWindowViewModel.MainViewModel = _mainWindowViewModel.SearchViewModel;
            var command = _mainWindowViewModel.SideMenuViewModel.LastClickedCommand as LastClickedCommand;
            var button = command.ButtonsList.FirstOrDefault(e => e.Text == "Search");
            button.Button_MouseEnter(button, null);
            button.AnimateClicked();
            command.Execute(button);
        }

        private void SearchOnlinePhotos()
        {
            var searchViewModel = _mainWindowViewModel.SearchViewModel;
            var albumSearchParams = new SearchParams();
            albumSearchParams.BuildDefaultSearchParamsForAlbum(_album.Name);
            searchViewModel.SearchEngine.LoadParams(albumSearchParams);
            searchViewModel.SearchEngine.GetSearchResults(false, true);
        }
    }
}
