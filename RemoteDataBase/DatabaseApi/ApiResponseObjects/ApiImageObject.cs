using System.Globalization;
using iPhoto.DataBase;

namespace GoogleDriveHandlerDemo.ApiHandler.ApiResponseObjects
{
    public class ApiImageObject
    {
        public int id { get; set; }
        public string source { get; set; }
        public int resolution_width { get; set; }
        public int resolution_height { get; set; }
        public string size { get; set; }

        public ImageEntity ToEntity()
        {
            return new ImageEntity()
            {
                Id = 1000 + id,
                Source = source,
                ResolutionHeight = resolution_width,
                ResolutionWidth = resolution_height,
                Size = double.Parse(size, CultureInfo.InvariantCulture)
            };
        }
    }
}

