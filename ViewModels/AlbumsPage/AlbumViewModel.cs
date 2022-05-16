using iPhoto.Commands.AlbumPage;
using iPhoto.DataBase;
using iPhoto.UtilityClasses;
using iPhoto.ViewModels.AlbumsPage;
using iPhoto.Views.AlbumPage;
using iPhoto.Views.SearchPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iPhoto.ViewModels
{
    public class AlbumViewModel : ViewModelBase
    {
        private ObservableCollection<AlbumSearchResultViewModel> _albumSearchResultsCollection;
        public SearchAlbumsCommand SearchAlbumsCommand { get; }
        private readonly MainWindowViewModel _mainWindowViewModel;
        public DatabaseHandler DatabaseHandler;
        private readonly PhotoDetailsWindowView _photoDetailsWindow;
        public AddAlbumViewModel AddAlbumViewModel { get; }

        public SearchAlbumEngine SearchAlbumEngine { get; }
        public AlbumSearchViewModel AlbumSearchViewModel { get; }
        public AlbumSearchView AlbumSearchViewx { get; }
        public AddAlbumView AddAlbumView { get; }

        public ObservableCollection<AlbumSearchResultViewModel> AlbumSearchResultsCollection
        {
            get { return _albumSearchResultsCollection; }
            set { _albumSearchResultsCollection = value; }
        }
        public AlbumViewModel(DatabaseHandler DataBase, MainWindowViewModel mainWindowViewModel, PhotoDetailsWindowView photoDetailsWindow)
        {
            DatabaseHandler = DataBase;
            _mainWindowViewModel = mainWindowViewModel;
            _photoDetailsWindow = photoDetailsWindow;
            _albumSearchResultsCollection = new ObservableCollection<AlbumSearchResultViewModel>();
            AlbumSearchViewModel = new AlbumSearchViewModel();
            AddAlbumViewModel = new AddAlbumViewModel(DataBase, this);
            DisplayAllAlbums();
            SearchAlbumEngine = new SearchAlbumEngine(DataBase);
            SearchAlbumsCommand = new SearchAlbumsCommand(this);
        }

        public void AddAlbumToView(Album album)
        {
            AlbumSearchResultsCollection.Add(new AlbumSearchResultViewModel(DatabaseHandler, _photoDetailsWindow, album, null, _mainWindowViewModel, this));
        }
        public void ReloadAlbumView(Album album)
        {
            int index = AlbumSearchResultsCollection.IndexOf(AlbumSearchResultsCollection.FirstOrDefault(y => y.AlbumId == album.Id));
            AlbumSearchResultsCollection[index] = new AlbumSearchResultViewModel(DatabaseHandler, _photoDetailsWindow, album, null, _mainWindowViewModel, this);
        }

        public void DisplayAllAlbums()
        {
            AlbumSearchResultsCollection.Clear();
            for (int i = 1; i <= DatabaseHandler.Albums.Count; i++)
            {
                var album = DatabaseHandler.Albums.FirstOrDefault(e => e.Id == i);
                AlbumSearchResultsCollection.Add(new AlbumSearchResultViewModel(DatabaseHandler,_photoDetailsWindow, album, null, _mainWindowViewModel, this));
            }
        }

        /// <summary>
        /// displays only albums that are in <paramref name="albums"/> list
        /// </summary>
        /// <param name="albums"></param>
        public void LoadGivenAlbums(List<Album> albums)
        {
            AlbumSearchResultsCollection.Clear();
            foreach (Album album in albums)
            {
                AlbumSearchResultsCollection.Add(new AlbumSearchResultViewModel(DatabaseHandler, _photoDetailsWindow, album, null, _mainWindowViewModel, this));
            }
        }
}
}
