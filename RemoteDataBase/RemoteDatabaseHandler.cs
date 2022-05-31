using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Threading.Tasks;
using System.Xml.Schema;
using GoogleDriveHandlerDemo;
using GoogleDriveHandlerDemo.ApiHandler;
using iPhoto.DataBase;
using iPhoto.Models;

namespace iPhoto.RemoteDatabase
{
    public class RemoteDatabaseHandler
    {
        private readonly DatabaseApiHandler _apiHandler;

        public List<Album> Albums { get; set; }
        public List<Photo> Photos { get; set; }
        public List<Image> Images { get; set; }
        public List<Place> Places { get; set; }

        public RemoteDatabaseHandler()
        {
            _apiHandler = new DatabaseApiHandler();

            Albums = new List<Album>();
            Photos = new List<Photo>();
            Images = new List<Image>();
            Places = new List<Place>();

            //LoadAllData();
        }

        public async Task<List<Album>> LoadAlbums()
        {
            Albums.Clear();
            var apiAlbums = await _apiHandler.GetAlbums();
            foreach (var e in apiAlbums)
            {
                Albums.Add(new Album(e.ToEntity()));
            }

            return Albums;
        }
        public async void LoadPlaces()
        {
            Places.Clear();
            var apiPlace = await _apiHandler.GetPlaces();
            foreach (var e in apiPlace)
            {
                Places.Add(new Place(e.ToEntity()));
            }
        }
        public async Task LoadImages()
        {
            Images.Clear();
            var apiImages = await _apiHandler.GetImages();
            foreach (var e in apiImages)
            {
                Images.Add(new Image(e.ToEntity()));
            }
        }
        public async Task LoadPhotos()
        {
            Photos.Clear();
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
        public ObservableCollection<string> GetAlbumList(bool addStar)
        {
            var albumList = new List<string>();
            var albumCollection = new ObservableCollection<string>();
            if (addStar)
            {
                albumList.Add("*");
            }
            foreach (var album in Albums)
            {
                albumList.Add(album.Name);
            }
            albumList.Sort();
            foreach (var album in albumList)
            {
                albumCollection.Add(album);
            }
            return albumCollection;
        }
        public async Task AddImage(string source, double size, int width, int height)
        {
            var data = await _apiHandler.PostImage(source, size, width, height);
            await LoadImages();
        }
        public async Task AddPhoto(string title, string dateTaken, string tags, string album, string imageSource, Task addImageTask)
        {
            await addImageTask;
            addImageTask.Wait();
            var imageId = Images.FirstOrDefault(e => e.Source == imageSource).Id - 1000;
            var albumId = Albums.FirstOrDefault(e => e.Name == album).Id - 1000;
            var data = _apiHandler.PostPhoto(title, albumId, imageId, tags, dateTaken);
            await LoadPhotos();
        }
        public void SetHandler()
        {
            _apiHandler.SetHandler();
        }
        public void DeletePhoto(int photoId)
        {
            var photo = Photos.FirstOrDefault(e => e.Id == photoId);
            var image = Images.FirstOrDefault(e => e.Id == photo.ImageId);

            var imageId = photo.ImageId;
            var source = image.Source;


            photoId -= 1000;
            imageId -= 1000;

            _apiHandler.RemoveImage(imageId);
            _apiHandler.RemovePhoto(photoId);

            Images.Remove(image);
            Photos.Remove(photo);

            LoadAllData();

            GoogleDriveHandler.DeleteFile(source);
        }
        public async Task<AccountDataModel> GetUserData()
        {
            var apiUserObject = await _apiHandler.GetUserData();
            return new AccountDataModel(apiUserObject);
        }
    }
}
