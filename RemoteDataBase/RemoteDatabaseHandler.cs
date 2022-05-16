using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using GoogleDriveHandlerDemo;
using GoogleDriveHandlerDemo.ApiHandler;
using iPhoto.DataBase;

namespace iPhoto.RemoteDatabase
{
    public class RemoteDatabaseHandler : DatabaseHandler
    {
        private readonly DatabaseApiHandler _apiHandler;
        private readonly GoogleDriveHandler _googleDriveHandler;

        public RemoteDatabaseHandler()
        {
            _googleDriveHandler = new GoogleDriveHandler();
            _apiHandler = new DatabaseApiHandler();

            //_googleDriveHandler.LoadAllData();
            //LoadAllData();
        }

        public new async void LoadAlbums()
        {
            var apiAlbums = await _apiHandler.GetAlbums();
            foreach (var e in apiAlbums)
            {
                Albums.Add(new Album(e.ToEntity()));
            }
        }
        public new async void LoadPlaces()
        {
            var apiPlace = await _apiHandler.GetPlaces();
            foreach (var e in apiPlace)
            {
                Places.Add(new Place(e.ToEntity()));
            }
        }
        public new async void LoadImages()
        {
            var apiImages = await _apiHandler.GetImages();
            foreach (var e in apiImages)
            {
                Images.Add(new Image(e.ToEntity()));
            }
        }
        public new async void LoadPhotos()
        {
            LoadImages();
            var apiPhotos = await _apiHandler.GetPhotos();
            foreach (var e in apiPhotos)
            {
                var photoEntity = e.ToEntity();
                Photos.Add(new Photo(photoEntity, Images.FirstOrDefault(ei => ei.GetEntity().Id == photoEntity.ImageEntityId).GetEntity(), false));
            }
        }
        public new void LoadAllData()
        {
            LoadPhotos();
            LoadAlbums();
            LoadPlaces();
        }
    }
}
