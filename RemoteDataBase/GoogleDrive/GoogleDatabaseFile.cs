using System;
using System.IO;
using System.Windows.Media.Imaging;
using Google.Apis.Drive.v3;

namespace GoogleDriveHandlerDemo
{
    public class GoogleDatabaseFile
    {
        public readonly string Name;
        public readonly string StringId;

        private readonly Google.Apis.Drive.v3.Data.File _googleApiFile;
        private readonly FilesResource.GetRequest _fileRequest;
        private readonly string _httpViewLink = "https://drive.google.com/uc?id=";
        private BitmapImage? _bitmapImage = null;

        public GoogleDatabaseFile(Google.Apis.Drive.v3.Data.File googleApiFile, FilesResource.GetRequest fileRequest)
        {
            _googleApiFile = googleApiFile;
            _fileRequest = fileRequest;

            Name = googleApiFile.Name;
            StringId = googleApiFile.Id;
        }

        public void DownloadFile(string directoryPath)
        {
            var fileStream = new FileStream(directoryPath + _googleApiFile.Name, FileMode.Create);
            _fileRequest.Download(fileStream);
        }
        public BitmapImage GetBitmapImage()
        {
            if (_bitmapImage != null) return _bitmapImage;

            var stream = new MemoryStream();
            _fileRequest.Download(stream);

            _bitmapImage = new BitmapImage();
            _bitmapImage.BeginInit();
            _bitmapImage.StreamSource = stream;
            _bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            _bitmapImage.DecodePixelWidth = 400;
            _bitmapImage.EndInit();

            return _bitmapImage;
        }
    }
}
