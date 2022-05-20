using iPhoto.DataBase;
using iPhoto.ViewModels;
using iPhoto.ViewModels.AlbumsPage;
using iPhoto.Views.SearchPage;

namespace iPhoto.Commands.AlbumPage
{
    public class ShowAlbumContentCommand : CommandBase
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly Album _album;
        private readonly DatabaseHandler _database;
        private readonly PhotoDetailsWindowView _photoDetailsWindow;
        private readonly AlbumViewModel _albumViewModel;
        public ShowAlbumContentCommand(DatabaseHandler database, PhotoDetailsWindowView photoDetailsWindow, MainWindowViewModel mainWindowViewModel, Album album, AlbumViewModel albumViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _album = album;
            _database = database;
            _photoDetailsWindow = photoDetailsWindow;
            _albumViewModel = albumViewModel;

        }
        public override void Execute(object parameter)
        {
            _mainWindowViewModel.MainViewModel = new AlbumContentViewModel(_database,_mainWindowViewModel.SearchViewModel.RemoteDatabaseHandler, _photoDetailsWindow, _mainWindowViewModel, _album, _albumViewModel);
        }
    }
}
