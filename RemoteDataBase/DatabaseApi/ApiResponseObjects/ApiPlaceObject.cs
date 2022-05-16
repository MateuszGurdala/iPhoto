using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using iPhoto.DataBase;

namespace GoogleDriveHandlerDemo.ApiHandler.ApiResponseObjects
{
    public class ApiPlaceObject
    {
        public int id { get; set; }
        public string name { get; set; }
        public string? latitude { get; set; }
        public string? longitude { get; set; }

        public PlaceEntity ToEntity()
        {
            return new PlaceEntity()
            {
                Id = 1000 + id,
                Name = name,
                Latitude = (latitude == null) ? null : float.Parse(latitude),
                Longitude = (longitude == null) ? null : float.Parse(longitude)
            };
        }
    }
}
