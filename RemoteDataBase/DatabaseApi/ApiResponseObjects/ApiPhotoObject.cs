using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using iPhoto.DataBase;

namespace GoogleDriveHandlerDemo.ApiHandler.ApiResponseObjects
{
    public class ApiPhotoObject
    {
        public int id { get; set; }
        public string title { get; set; }
        public string date_taken { get; set; }
        public string tags { get; set; }
        public int album { get; set; }
        public int image { get; set; }
        public int place { get; set; }

        public PhotoEntity ToEntity()
        {
            return new PhotoEntity
            {
                Id = 1000 + id,
                Title = title,
                DateTaken = DateTime.Parse(date_taken.Substring(0, 10), CultureInfo.InvariantCulture).ToString("dd.MM.yyyy HH:mm:ss"),
                Tags = tags,
                AlbumEntityId = album + 1000,
                ImageEntityId = image + 1000,
                PlaceEntityId = place + 1000
            };
        }
    }
}
