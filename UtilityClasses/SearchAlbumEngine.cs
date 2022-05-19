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
        private readonly DatabaseHandler _databaseHandler;

        public SearchAlbumEngine(DatabaseHandler databaseHandler)
        {
            _databaseHandler = databaseHandler;
        }

        /// <summary>
        /// Restricts Albums collection, 
        /// leaving only albums that does contain <paramref name="name"/> as substring
        /// </summary>
        /// <param name="name"></param>
        private List<Album> SearchAlbumsByName(string? name)
        {
            var excludedAlbums = new List<Album>();
            if (name != null && name != "" && name != "*Any")
            {
                excludedAlbums = (_databaseHandler.Albums.Where(e => !(e.Name.Contains(name)))).ToList();
            }
            return excludedAlbums;
        }
        /// <summary>
        /// Restricts Albums collection, 
        /// leaving only albums that does contain <paramref name="color"/> as their color group
        /// </summary>
        /// <param name="color"></param>
        private List<Album> SearchAlbumsByColorGroup(string? color)
        {
            var excludedAlbums = new List<Album>();
            if (color != "Any" && color != null)
            {
                excludedAlbums = (_databaseHandler.Albums.Where(e=> e.ColorGroup != color)).ToList();
            }
            return excludedAlbums;
        }
        /// <summary>
        /// Restricts Albums collection, 
        /// leaving only albums which where created beetween <paramref name="beginDate"/> and <paramref name="endDate"/>
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        private List<Album> SearchAlbumsByDate(DateTime? beginDate, DateTime? endDate)
        {
            var excludedAlbums = new List<Album>();
            if (beginDate != null && endDate != null)
            {
                excludedAlbums = _databaseHandler.Albums.Where(e => e.CreationDate <= beginDate || e.CreationDate >= endDate).ToList();
            }
            return excludedAlbums;
        }

        private List<Album> SearchAlbumsByTags(string[]? tagsList)
        {
            var excludedAlbums = new List<Album>();
            if (tagsList != null && tagsList[0] != "")
            {
                foreach (var tag in tagsList)
                {
                    excludedAlbums = _databaseHandler.Albums.Where(e => !(e.Tags.Contains(tag))).ToList();
                }
            }
            return excludedAlbums;


        }

        public List<Album> SearchAlbums(AlbumSearchParams albumSearchParams)
        {
            var excludedAlbums = new List<Album>();
            excludedAlbums = excludedAlbums.Concat(SearchAlbumsByColorGroup(albumSearchParams.Color)).ToList();
            excludedAlbums = excludedAlbums.Concat(SearchAlbumsByTags(albumSearchParams.Tags)).ToList();
            excludedAlbums = excludedAlbums.Concat(SearchAlbumsByDate(albumSearchParams.StartDate,albumSearchParams.EndDate)).ToList();
            excludedAlbums = excludedAlbums.Concat(SearchAlbumsByName(albumSearchParams.Name)).ToList();
            return _databaseHandler.Albums.Except(excludedAlbums).ToList();
        }

    }
}
