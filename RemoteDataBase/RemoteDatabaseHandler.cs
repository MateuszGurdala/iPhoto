using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using GoogleDriveHandlerDemo;
using GoogleDriveHandlerDemo.ApiHandler;
using iPhoto.DataBase;

namespace iPhoto.RemoteDatabase
{
    public class RemoteDatabaseHandler
    {
        private readonly DatabaseApiHandler _apiHandler;
        private readonly GoogleDriveHandler _googleDriveHandler;

        public ObservableCollection<Album> Albums { get; set; }
        public ObservableCollection<Photo> Photos { get; set; }
        public ObservableCollection<Image> Images { get; set; }
        public ObservableCollection<Place> Places { get; set; }

        public RemoteDatabaseHandler()
        {
            _googleDriveHandler = new GoogleDriveHandler();
            _apiHandler = new DatabaseApiHandler();

            Albums = new ObservableCollection<Album>();
            Photos = new ObservableCollection<Photo>();
            Images = new ObservableCollection<Image>();
            Places = new ObservableCollection<Place>();

            //LoadAllData();
        }

        public async void LoadAlbums()
        {
            var apiAlbums = await _apiHandler.GetAlbums();
            foreach (var e in apiAlbums)
            {
                Albums.Add(new Album(e.ToEntity()));
            }
        }
        public async void LoadPlaces()
        {
            var apiPlace = await _apiHandler.GetPlaces();
            foreach (var e in apiPlace)
            {
                Places.Add(new Place(e.ToEntity()));
            }
        }
        public async void LoadImages()
        {
            var apiImages = await _apiHandler.GetImages();
            foreach (var e in apiImages)
            {
                Images.Add(new Image(e.ToEntity()));
            }
        }
        public async void LoadPhotos()
        {
            LoadImages();
            var apiPhotos = await _apiHandler.GetPhotos();
            foreach (var e in apiPhotos)
            {
                var photoEntity = e.ToEntity();
                Photos.Add(new Photo(photoEntity, Images.FirstOrDefault(ei => ei.GetEntity().Id == photoEntity.ImageEntityId).GetEntity(), false));
            }
        }
        public void LoadAllData()
        {
            _googleDriveHandler.LoadAllData();
            LoadPhotos();
            LoadAlbums();
            LoadPlaces();
        }
    }
}
