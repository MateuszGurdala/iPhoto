using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using iPhoto.DataBase;
using iPhoto.ViewModels.SearchPage;
using iPhoto.Views.SearchPage;

namespace iPhoto.UtilityClasses
{
    public class PhotoAdder
    {
        //Miscellaneous
        private DatabaseHandler _databaseHandler;
        private BitmapImage _bitmapImage;
        private readonly string _fullPath;
        public AddPhotoPopupView Popup;

        //Image
        private String _fileName;
        private int _width;
        private int _height;
        private double _size;

        public PhotoAdder(DatabaseHandler databaseHandler, String fileName)
        {
            _databaseHandler = databaseHandler;
            _fullPath = fileName;
            GetImageData();
        }

        private void GetImageData()
        {
            _fileName = Path.GetFileName(_fullPath);
            _bitmapImage = DataHandler.LoadBitmapImage(_fullPath, null);

            _size = new FileInfo(_fullPath).Length;
            _size /= 1048576;
            _size = Math.Round(_size, 2);

            _width = (int)_bitmapImage.Width;
            _height = (int)_bitmapImage.Height;
        }
        public void GetPhotoData()
        {
            var dataContext = new AddPhotoPopupViewModel()
            {
                Image = _bitmapImage,
            };
            dataContext.PhotoAdder = this;
            Popup = new AddPhotoPopupView(dataContext);
        }
        public async void AddPhoto(string title, string album, string rawTags, string creationDateString, string placeTaken)
        {
            DateTime? creationDate = creationDateString == "Today" ? null : DateTime.Parse(creationDateString);
            int? albumId;
            string? tags = rawTags == "#none" ? null : rawTags;

            CheckData(title, album, tags, creationDateString, placeTaken);

            var placeId = _databaseHandler.Places.First(e => e.Name == placeTaken).Id;

            if (album == "OtherPhotos")
            {
                albumId = null;
            }
            else
            {
                albumId = _databaseHandler.Albums.First(e => e.Name == album).Id;
            }

            await Task.Run(() =>
            {
                _databaseHandler.AddImage(_fileName, _width, _height, _size);
                var imageId = _databaseHandler.Images.First(e => e.Source == _fileName).Id;
                _databaseHandler.AddPhoto(title, albumId, rawTags, creationDate, placeId, imageId);
                MoveFileToDatabaseDirectory();
            });

            Popup.IsOpen = false;

        }
        private void CheckData(string title, string album, string? tags, string creationDateString, string placeTaken)
        {
            if (_databaseHandler.Photos.FirstOrDefault(e => e.Title == title) != null)
            {
                throw new InvalidDataException("Title is already taken");
            }
            if (_databaseHandler.Albums.FirstOrDefault(e => e.Name == album) == null)
            {
                throw new InvalidDataException("Invalid album name.");
            }
            if (_databaseHandler.Places.FirstOrDefault(e => e.Name == placeTaken) == null)
            {
                throw new InvalidDataException("Invalid place name.");
            }
            if (tags != null && tags[0] != '#')
            {
                throw new InvalidDataException("Invalid tags format.");
            }
        }
        private void MoveFileToDatabaseDirectory()
        {
            File.Move(_fullPath, DataHandler.GetDatabaseImageDirectory() + "\\" + _fileName);
        }
        private bool IsFileLocked(FileInfo file)
        {
            try
            {
                using FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
                stream.Close();
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }

            //file is not locked
            return false;
        }
    }
}
