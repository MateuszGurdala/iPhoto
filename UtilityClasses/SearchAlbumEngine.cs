using iPhoto.DataBase;
using iPhoto.Models;
using iPhoto.ViewModels;
using iPhoto.ViewModels.AlbumsPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPhoto.UtilityClasses
{
    public class SearchAlbumEngine
    {
        private readonly AlbumViewModel _albumViewModel;

        public SearchAlbumEngine(AlbumViewModel albumViewModel)
        {
            _albumViewModel = albumViewModel;
        }

        /// <summary>
        /// Restricts Albums collection, 
        /// leaving only albums that does contain <paramref name="name"/> as substring
        /// </summary>
        /// <param name="name"></param>
        private void SearchAlbumsByName(string? name)
        {
            if (name != null && name != "")
            {
                _albumViewModel.AlbumSearchResultsCollection = new (_albumViewModel.AlbumSearchResultsCollection.
                    Where(e => e.AlbumData.Name == name));
            }
        }
        /// <summary>
        /// Restricts Albums collection, 
        /// leaving only albums that does contain <paramref name="color"/> as their color group
        /// </summary>
        /// <param name="color"></param>
        private void SearchAlbumsByColorGroup(string? color)
        {

            if (color != null && color != "")
            {
                _albumViewModel.AlbumSearchResultsCollection = new(_albumViewModel.AlbumSearchResultsCollection.
                    Where(e => e.AlbumData.ColorGroup == color));
            }
        }
        /// <summary>
        /// Restricts Albums collection, 
        /// leaving only albums which where created beetween <paramref name="beginDate"/> and <paramref name="endDate"/>
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        private void SearchAlbumsByDate(DateTime? beginDate, DateTime? endDate)
        {

            if (beginDate != null && endDate != null)
            {
                _albumViewModel.AlbumSearchResultsCollection = new(_albumViewModel.AlbumSearchResultsCollection.
                    Where(e => (e.AlbumData.CreationDate > beginDate && e.AlbumData.CreationDate < beginDate)));
            }
        }

        private void SearchAlbumsByTags(string[]? tagsList)
        {
            // TODO after implementing tags for albums
            throw new NotImplementedException();
        }

        public void SearchAlbums(AlbumSearchParams albumSearchParams)
        {
            SearchAlbumsByColorGroup(albumSearchParams.Color);
            //SearchAlbumsByTags(albumSearchParams.Tags);
            SearchAlbumsByDate(albumSearchParams.StartDate,albumSearchParams.EndDate);
            SearchAlbumsByName(albumSearchParams.Name);      
        }

    }
}
