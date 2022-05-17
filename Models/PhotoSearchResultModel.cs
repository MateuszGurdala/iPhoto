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
        public BitmapImage PreviewImage { get; set; }
        private BitmapImage? _image = null;
        private const int _maxNameLength = 15;
        private readonly string _fullPath;

        //MG 16.04 implemented handling database data format 
        public PhotoSearchResultModel(Photo photoData, Image imageData, Album albumData, Place placeData)
        {
            PhotoData = photoData;
            ImageData = imageData;
            AlbumData = albumData;
            PlaceData = placeData;

            Title = PhotoData.Title;

            if (photoData.IsLocal)
            {
                _fullPath = DataHandler.GetDatabaseImageDirectory() + "\\" + ImageData.Source;
                PreviewImage = DataHandler.LoadBitmapImage(_fullPath, 140);
            }
            else
            {
                _fullPath = "https://drive.google.com/uc?id=" + ImageData.Source;
                PreviewImage = DataHandler.LoadBitmapImageAsync(_fullPath, 140);
            }
        }
        public BitmapImage GetImage()
        {
            if (_image == null)
            {
                if (!PhotoData.IsLocal)
                {
                    _image = DataHandler.LoadBitmapImageAsync(_fullPath, null);
                }
                else
                {
                    _image = DataHandler.LoadBitmapImage(_fullPath, null);
                }
            }
            return _image;
        }
    }
}
