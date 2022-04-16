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
            PreviewImage = GetPreviewImage(ImageData.Source);
        }
        private string GetTitle(string uri)
        {
            foreach (char letter in uri)
            {
                string fileName;
                if (letter == '/')
                {
                    fileName = uri.Split('/').Last();
                    return (fileName.Length > _maxNameLength) ? String.Concat(fileName.Substring(0, _maxNameLength - 4), "...") : fileName;
                }
                else if (letter == '\\')
                {
                    fileName = uri.Split('\\').Last();
                    return (fileName.Length > _maxNameLength) ? String.Concat(fileName.Substring(0, _maxNameLength - 4), "...") : fileName;
                }
            }
            return "NameError";
        }
        private BitmapImage GetPreviewImage(string uri)
        {
            BitmapImage myBitmapImage = new BitmapImage();

            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = new Uri(_fullPath);
            myBitmapImage.DecodePixelWidth = 140;
            myBitmapImage.EndInit();

            myBitmapImage.Freeze();

            return myBitmapImage;
        }
        public BitmapImage GetImage()
        {
            BitmapImage myBitmapImage = new BitmapImage();

            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = new Uri(_fullPath);
            myBitmapImage.EndInit();

            myBitmapImage.Freeze();

            return myBitmapImage;
        }
    }
}
