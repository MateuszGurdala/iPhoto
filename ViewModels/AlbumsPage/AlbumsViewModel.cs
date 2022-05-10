using iPhoto.Commands.AlbumPage;
using iPhoto.DataBase;
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
    public class AlbumsViewModel : ViewModelBase
    {
        private ObservableCollection<AlbumSearchResultViewModel> _albumSearchResultsCollection;

        private readonly MainWindowViewModel _mainWindowViewModel;
        public DatabaseHandler DatabaseHandler;
        private readonly PhotoDetailsWindowView _photoDetailsWindow;
        public AddAlbumViewModel AddAlbumViewModel { get; }

        public AddAlbumView AddAlbumView { get; }
        public ObservableCollection<AlbumSearchResultViewModel> AlbumSearchResultsCollection
        {
            get { return _albumSearchResultsCollection; }
            set { _albumSearchResultsCollection = value; }
        }
        public AlbumsViewModel(DatabaseHandler DataBase, MainWindowViewModel mainWindowViewModel, PhotoDetailsWindowView photoDetailsWindow)
        {
             DatabaseHandler = DataBase;
            _mainWindowViewModel = mainWindowViewModel;
            _photoDetailsWindow = photoDetailsWindow;
            _albumSearchResultsCollection = new ObservableCollection<AlbumSearchResultViewModel>();
            AddAlbumViewModel = new AddAlbumViewModel(DataBase, this);
            DisplayAllAlbums();
        }

        public void AddAlbumToView(Album album)
        {
            AlbumSearchResultsCollection.Add(new AlbumSearchResultViewModel(DatabaseHandler, _photoDetailsWindow, album, null, _mainWindowViewModel));
        }

        public void DisplayAllAlbums()
        {
            for (int i = 1; i <= DatabaseHandler.Albums.Count; i++)
            {
                var album = DatabaseHandler.Albums.FirstOrDefault(e => e.Id == i);
                AlbumSearchResultsCollection.Add(new AlbumSearchResultViewModel(DatabaseHandler,_photoDetailsWindow, album, null, _mainWindowViewModel));
            }
        }
/*        /// <summary>
        /// Restricts Albums collection, 
        /// leaving only albums that does contain <paramref name="phrase"/> as substring
        /// </summary>
        /// <param name="phrase"></param>
        public void SearchAlbumsByName(string phrase)
        {
            // TODO make this into LINQ KG 29.4
            if (phrase == null || phrase == "")
            {
                _albumSearchResultsCollection = _dataBase.Albums;
            }
            else
            {
                List<Album> tempAlbumCollection = new();
                foreach (Album album in _dataBase.Albums)
                {
                    if(album.Name.Contains(phrase))
                    {
                        tempAlbumCollection.Add(album);
                    }
                }
                _albumSearchResultsCollection = tempAlbumCollection;
            }
        }
        /// <summary>
        ///  Restricts Albums collection, 
        /// leaving only albums that does contain <paramref name="color"/> as their color group
        /// </summary>
        /// <param name="color"></param>
        public void SearchAlbumsByColorGroup(string color)
        {
            // TODO make this into LINQ KG 29.4
            if (color == null || color == "")
            {
                _albumSearchResultsCollection = _dataBase.Albums;
            }
            else
            {
                List<Album> tempAlbumCollection = new();
                foreach (Album album in _dataBase.Albums)
                {
                    if (album.ColorGroup == color)
                    {
                        tempAlbumCollection.Add(album);
                    }
                }
                _albumSearchResultsCollection = tempAlbumCollection;
            }
        }*/


}
}
