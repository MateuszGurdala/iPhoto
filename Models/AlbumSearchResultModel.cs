using iPhoto.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPhoto.Models
{
    public class AlbumSearchResultModel
    {
        public readonly int Id;
        public readonly string Name;
        public readonly int PhotoCount;
        public readonly List<string> Tags;
        public readonly DateTime CreationDate;
        public readonly bool IsLocal;
        public readonly string ColorGroup;
        public readonly List<PhotoEntity> PhotoEntities;
        public readonly double TotalMemorySize;

        public AlbumSearchResultModel(Album album, List<PhotoEntity>? photoEntities)
        {
            Id = album.Id;
            Name = album.Name;
            PhotoCount = album.PhotoCount;
            Tags = album.Tags;
            CreationDate = album.CreationDate;
            IsLocal = album.IsLocal;
            ColorGroup = album.ColorGroup;
            PhotoEntities = photoEntities ?? new List<PhotoEntity>{ };
            TotalMemorySize = album.TotalMemorySize;
        }

        
    }
}
