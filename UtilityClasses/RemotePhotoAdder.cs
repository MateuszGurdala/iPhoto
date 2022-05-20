using System;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using iPhoto.DataBase;
using iPhoto.RemoteDatabase;
using iPhoto.Views.AlbumPage;
using iPhoto.Views.SearchPage;

namespace iPhoto.UtilityClasses
{
    public class RemotePhotoAdder
    {
        //Miscellaneous
        private readonly RemoteDatabaseHandler _databaseHandler;
        private BitmapImage _bitmapImage;
        private readonly string _fullPath;
        public AddPhotoPopupView Popup;
        public AddPhotoToAlbumPopupView PopupForAlbums;
        //Image
        private string _fileName;
        private int _width;
        private int _height;
        private double _size;

        //Photo
        private string _title;
        private string? _rawTags;
        private DateTime? _dateCreated;
        private int? _albumId;
        private int? _placeId;
        private Image _image;

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
        public void AddPhoto(string title, string album, string rawTags, string? creationDateString, string placeTaken)
        {
            CheckData(title, album, rawTags, creationDateString, placeTaken);
            ParseData(title, album, rawTags, creationDateString, placeTaken);

        }
        public void UpdatePhoto(int id, string title, string album, string rawTags, string? creationDateString, string placeTaken)
        {
            CheckUpdateData(title, album, rawTags, creationDateString, placeTaken);
            ParseData(title, album, rawTags, creationDateString, placeTaken);
        }
        private void CheckData(string title, string album, string? tags, string? creationDateString, string placeTaken)
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
        private void CheckUpdateData(string title, string album, string? tags, string? creationDateString, string placeTaken)
        {
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
        private void ParseData(string title, string album, string rawTags, string? creationDateString, string placeTaken)
        {
            _rawTags = rawTags == "#none" ? null : rawTags;
            _dateCreated = creationDateString == "" ? DateTime.Now : DateTime.ParseExact(creationDateString, "dd.MM.yyyy", null);
            _title = title == "Default" ? GenerateDefaultTitle() : title;
            _placeId = _databaseHandler.Places.First(e => e.Name == placeTaken).Id;
            _albumId = album == "OtherPhotos" ? _databaseHandler.Albums[0].Id : _databaseHandler.Albums.First(e => e.Name == album).Id; // change this after implementing Photo Add checker TODO
        }
        private string GenerateDefaultTitle()
        {
            var title = DateTime.Now.Date.ToShortDateString() + "photo";
            var number = 0;
            while (_databaseHandler.Photos.FirstOrDefault(e => e.Title == title) != null)
            {
                number += 1;
                title = DateTime.Now.Date.ToShortDateString() + "photo" + number;
            }

            return title;
        }
    }
}
