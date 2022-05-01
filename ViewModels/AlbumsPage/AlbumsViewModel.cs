using iPhoto.DataBase;
using iPhoto.ViewModels.AlbumsPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPhoto.ViewModels
{
    public class AlbumsViewModel : ViewModelBase
    {
        private ObservableCollection<AlbumSearchResultViewModel> _albumSearchResultsCollection;

        public DatabaseHandler DatabaseHandler;
        public ObservableCollection<AlbumSearchResultViewModel> AlbumSearchResultsCollection
        {
            get { return _albumSearchResultsCollection; }
            set { _albumSearchResultsCollection = value; }
        }
        public AlbumsViewModel(DatabaseHandler DataBase)
        {
             DatabaseHandler = DataBase;
            _albumSearchResultsCollection = new ObservableCollection<AlbumSearchResultViewModel>();

            DisplayAllAlbums();
        }

        private void DisplayAllAlbums()
        {
            for(int i = 1; i <= DatabaseHandler.Albums.Count; i++)
            {
                var album = DatabaseHandler.Albums.FirstOrDefault(e => e.Id == i);
                AlbumSearchResultsCollection.Add(new AlbumSearchResultViewModel(album, null));
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
