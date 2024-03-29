﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using iPhoto.ViewModels.AlbumsPage;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using iPhoto.DataBase;
using iPhoto.RemoteDatabase;
using iPhoto.ViewModels;
using iPhoto.ViewModels.SearchPage;
using iPhoto.Views.SearchPage;
using iPhoto.Views.AlbumPage;
using System.Windows;

namespace iPhoto.UtilityClasses
{
    public class PhotoAdder
    {
        //Miscellaneous
        private readonly DatabaseHandler _databaseHandler;
        private readonly RemoteDatabaseHandler _remoteHandler;
        private BitmapImage _bitmapImage;
        private readonly string _fullPath;
        public AddPhotoPopupView Popup;
        public AddPhotoToAlbumPopupView PopupForAlbums;
        public bool Update { get; set; } = false;

        //Image
        private string _fileName;
        private int _width;
        private int _height;
        private double _size;

        //Photo
        private int _id;
        private string _title;
        private string? _rawTags;
        private DateTime? _dateCreated;
        private int? _albumId;
        private int? _placeId;
        private Image _image;

        public PhotoAdder(DatabaseHandler databaseHandler, string? fileName)
        {
            _databaseHandler = databaseHandler;
            if (fileName != null)
            {
                _fullPath = fileName;
                GetImageData();
            }
        }
        public PhotoAdder(DatabaseHandler databaseHandler,RemoteDatabaseHandler remoteDatabase, string? fileName) : this(databaseHandler, fileName)
        {
            _remoteHandler = remoteDatabase;
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
        public void GetPhotoData(Album album = null, AlbumContentViewModel albumVm = null)
        {
            if (album == null || albumVm == null)
            {
                var accountViewModel = (App.Current.MainWindow.DataContext as MainWindowViewModel).AccountViewModel;

                var albums = _databaseHandler.GetAlbumList(false);

                if (accountViewModel.CurrentViewModel == accountViewModel.LoggedInViewModel)
                {
                    var remoteAlbums = _remoteHandler.GetAlbumList(false);
                    foreach (var remoteAlbum in remoteAlbums)
                    {
                        albums.Add(remoteAlbum);
                    }
                }

                var dataContext = new AddPhotoPopupViewModel()
                {
                    Image = _bitmapImage,
                    AlbumList = albums,
                    PlacesList = _databaseHandler.GetPlacesList(false)
                };
                dataContext.PhotoAdder = this;
                Popup = new AddPhotoPopupView(dataContext);
            }
            else
            {
                var dataContext = new AddPhotoToAlbumPopupViewModel(album, albumVm)
                {
                    Image = _bitmapImage,
                };
                dataContext.PhotoAdder = this;
                PopupForAlbums = new AddPhotoToAlbumPopupView(dataContext);
            }
        }
        public void AddPhoto(string title, string album, string rawTags, string? creationDateString, string placeTaken)
        {
            if (Popup != null)
                Popup.IsOpen = false;
            else
                PopupForAlbums.IsOpen = false;

            if (_databaseHandler.Albums.FirstOrDefault(e => e.Name == album) == null)
            {
                var source = RemotePhotoAdder.UploadImage(_fileName, _fullPath);
                var addImageTask = RemotePhotoAdder.AddImage(source, _size, _width, _height);
                RemotePhotoAdder.AddPhoto(title, album, source, rawTags, creationDateString, placeTaken, addImageTask);
            }
            else
            {
                if (!CheckData(title, album, rawTags, creationDateString, placeTaken))
                    return;
                ParseData(title, album, rawTags, creationDateString, placeTaken);
                if(!_databaseHandler.AddImage(_fileName, _width, _height, _size))
                    return;
                _image = _databaseHandler.Images.First(e => e.Source == _fileName);
                _databaseHandler.AddPhoto(_title, _albumId, _rawTags, _dateCreated, _placeId, _image.Id, _image.Size);

                MoveFileToDatabaseDirectory();
                
            }
        }
        public void UpdatePhoto(int id, string title, string album, string rawTags, string? creationDateString, string placeTaken, ChangePhotoDetailsViewModel vm)
        {
            _id = id;
            if (CheckUpdateData(title, album, rawTags, creationDateString, placeTaken, vm))
            {
                ParseData(title, album, rawTags, creationDateString, placeTaken);

                _databaseHandler.UpdatePhoto(id, _title, album, _rawTags, DateTime.Parse(creationDateString), placeTaken);
            }
        }
        private bool CheckData(string title, string album, string? tags, string? creationDateString, string placeTaken)
        {
            //if (_databaseHandler.Photos.FirstOrDefault(e => e.Title == title) != null)
            //{
            //    MessageBox.Show("Unable to add photo. Title is already taken. Try again.", "Photo Add Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    //throw new InvalidDataException("Title is already taken");
            //    return false;
            //}
            if (_databaseHandler.Albums.FirstOrDefault(e => e.Name == album) == null)
            {
                MessageBox.Show("Unable to add photo. Album name is invalid. Try again.", "Photo Add Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //throw new InvalidDataException("Invalid album name.");
                return false;
            }
            if (_databaseHandler.Places.FirstOrDefault(e => e.Name == placeTaken) == null)
            {
                MessageBox.Show("Unable to add photo. Place does not exist. Try again.", "Photo Add Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //throw new InvalidDataException("Invalid place name.");
                return false;
            }
            if (tags != null && tags[0] != '#')
            {
                MessageBox.Show("Unable to add photo. Tags format is invalid. Try again, use '#' before name of a tag.", "Photo Add Error", MessageBoxButton.OK, MessageBoxImage.Error);
                // throw new InvalidDataException("Invalid tags format.");
                return false;
            }
            return true;
        }
        private bool CheckUpdateData(string title, string album, string? tags, string? creationDateString, string placeTaken, ChangePhotoDetailsViewModel vm)
        {
            //if (_databaseHandler.Photos.FirstOrDefault(e => e.Title == title) != null && title != _databaseHandler.Photos.FirstOrDefault(e => e.Id == photoId).Title)
            //{
            //    vm.ParentView.IsOpen = false;
            //    MessageBox.Show("Unable to add photo. Title is already taken. Try again.", "Photo Add Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    //throw new InvalidDataException("Title is already taken");
            //    return false;
            //}
            if (_databaseHandler.Albums.FirstOrDefault(e => e.Name == album) == null)
            {
                vm.ParentView.IsOpen = false;
                MessageBox.Show("Unable to add photo. Album name is invalid. Try again.", "Photo Update Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //throw new InvalidDataException("Invalid album name.");
                return false;
            }
            if (_databaseHandler.Places.FirstOrDefault(e => e.Name == placeTaken) == null)
            {
                vm.ParentView.IsOpen = false;
                MessageBox.Show("Unable to update photo. Place does not exist. Try again.", "Photo Update Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //throw new InvalidDataException("Invalid place name.");
                return false;
            }
            if (tags != null && tags[0] != '#')
            {
                vm.ParentView.IsOpen = false;
                MessageBox.Show("Unable to update photo. Tags format is invalid. Try again use '#' before name of tag", "Photo Update Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //throw new InvalidDataException("Invalid tags format.");
                return false;
            }
            return true;
        }
        private void ParseData(string title, string album, string rawTags, string? creationDateString, string placeTaken)
        {
            _rawTags = rawTags == "#none" ? null : rawTags;
            _dateCreated = creationDateString == "" ? DateTime.Now : DateTime.ParseExact(creationDateString, "dd.MM.yyyy", null);
            _placeId = _databaseHandler.Places.First(e => e.Name == placeTaken).Id;
            _albumId = album == "OtherPhotos" ? _databaseHandler.Albums[0].Id : _databaseHandler.Albums.First(e => e.Name == album).Id;

            var newTitle = title;

            if (Update && _databaseHandler.Photos.FirstOrDefault(e => e.Id == _id).Title != title)
            {
                if (_databaseHandler.Photos.FirstOrDefault(e => e.Title == title) != null)
                {
                    var number = 0;
                    while (_databaseHandler.Photos.FirstOrDefault(e => e.Title == newTitle) != null)
                    {
                        number += 1;
                        newTitle = title + "(" + number + ")";
                    }
                }
            }
            else if (!Update)
            {
                if (_databaseHandler.Photos.FirstOrDefault(e => e.Title == title) != null)
                {
                    var number = 0;
                    while (_databaseHandler.Photos.FirstOrDefault(e => e.Title == newTitle) != null)
                    {
                        number += 1;
                        newTitle = title + "(" + number + ")";
                    }
                }
            }

            _title = title == "Default" ? GenerateDefaultTitle() : newTitle;
        }
        private void MoveFileToDatabaseDirectory()
        {
            File.Copy(_fullPath, DataHandler.GetDatabaseImageDirectory() + "\\" + _fileName);
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
