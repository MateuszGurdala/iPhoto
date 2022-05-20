using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GoogleDriveHandlerDemo;
using GoogleDriveHandlerDemo.ApiHandler;
using iPhoto.DataBase;

namespace iPhoto.RemoteDatabase
{
    public class RemoteDatabaseHandler
    {
        private readonly DatabaseApiHandler _apiHandler;
        private readonly GoogleDriveHandler _googleDriveHandler;

        public List<Album> Albums { get; set; }
        public List<Photo> Photos { get; set; }
        public List<Image> Images { get; set; }
        public List<Place> Places { get; set; }

        public RemoteDatabaseHandler()
        {
            _googleDriveHandler = new GoogleDriveHandler();
            _apiHandler = new DatabaseApiHandler();

            Albums = new List<Album>();
            Photos = new List<Photo>();
            Images = new List<Image>();
            Places = new List<Place>();

            //LoadAllData();
        }

        public async Task<List<Album>> LoadAlbums()
        {
            var apiAlbums = await _apiHandler.GetAlbums();
            foreach (var e in apiAlbums)
            {
                Albums.Add(new Album(e.ToEntity()));
            }

            return Albums;
        }
        public async void LoadPlaces()
        {
            var apiPlace = await _apiHandler.GetPlaces();
            foreach (var e in apiPlace)
            {
                Places.Add(new Place(e.ToEntity()));
            }
        }
        public async Task LoadImages()
        {
            var apiImages = await _apiHandler.GetImages();
            foreach (var e in apiImages)
            {
                Images.Add(new Image(e.ToEntity()));
            }
        }
        public async void LoadPhotos()
        {
            await LoadImages();
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
            LoadPlaces();
        }

        public void Clear()
        {
            Albums.Clear();
            Photos.Clear();
            Images.Clear();
            Places.Clear();
        }
    }
}
