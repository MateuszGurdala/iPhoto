using System;
using System.Linq;
using System.Windows.Media.Imaging;
using iPhoto.DataBase;
using iPhoto.UtilityClasses;

namespace iPhoto.Models
{
    public class PhotoSearchResultModel
    {
        public readonly Photo PhotoData;
        public readonly Image ImageData;
        public readonly Album AlbumData;
        public readonly Place PlaceData;
        public string Title { get; }
        public BitmapImage PreviewImage { get; }
        private const int _maxNameLength = 15;
        private readonly string _fullPath;

        //MG 16.04 implemented handling database data format 
        public PhotoSearchResultModel(Photo photoData, Image imageData, Album albumData, Place placeData)
        {
            PhotoData = photoData;
            ImageData = imageData;
            AlbumData = albumData;
            PlaceData = placeData;

            _fullPath = DataHandler.GetDatabaseImageDirectory() + "\\" + ImageData.Source;
            Title = PhotoData.Title;
            PreviewImage = DataHandler.LoadBitmapImage(_fullPath, 140);
        }
        public BitmapImage GetImage()
        {
            return DataHandler.LoadBitmapImage(_fullPath, null);
        }
    }
}
