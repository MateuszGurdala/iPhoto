using System;
using System.Linq;
using System.Windows.Media.Imaging;

namespace iPhoto.Models
{
    public class PhotoSearchResultModel
    {
        public string PhotoUri { get;}
        public string Title { get;}
        public BitmapImage PreviewImage { get;}
        private const int MaxNameLength = 15;

        public PhotoSearchResultModel(string uri)
        {
            PhotoUri = uri;
            Title = GetTitle(PhotoUri);
            PreviewImage = GetPreviewImage(PhotoUri);
        }
        private string GetTitle(string uri)
        {
            string fileName;
            foreach (char letter in uri)
            {
                if (letter == '/')
                {
                    fileName = uri.Split('/').Last();
                    return (fileName.Length > MaxNameLength) ? String.Concat(fileName.Substring(0, MaxNameLength - 4), "...") : fileName;
                }
                else if (letter == '\\')
                {
                    fileName = uri.Split('\\').Last();
                    return (fileName.Length > MaxNameLength) ? String.Concat(fileName.Substring(0, MaxNameLength - 4), "...") : fileName;
                }
            }
            return "NameError";
        }
        private BitmapImage GetPreviewImage(string uri)
        {
            BitmapImage myBitmapImage = new BitmapImage();

            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = new Uri(uri);
            myBitmapImage.DecodePixelWidth = 140;
            myBitmapImage.EndInit();

            myBitmapImage.Freeze();

            return myBitmapImage;
        }
        public BitmapImage GetImage()
        {
            BitmapImage myBitmapImage = new BitmapImage();

            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = new Uri(PhotoUri);
            myBitmapImage.EndInit();

            myBitmapImage.Freeze();

            return myBitmapImage;
        }
    }
}
