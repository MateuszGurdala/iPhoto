using iPhoto.UtilityClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace iPhoto.DataBase
{
    public class DatabaseHandler
    {
        public List<Album> Albums;
        public List<Image> Images;
        public List<Photo> Photos;
        public List<Place> Places;


        public DatabaseHandler()
        {
            Albums = new List<Album>();
            Images = new List<Image>();
            Photos = new List<Photo>();
            Places = new List<Place>();

            //Delete if loading all data immediately is unnecessary
            LoadAllData();
            //MG 16.04
            CreateBaseAlbum();
            CreateBasePlace();
            //~MG 16.04
        }

        //MG 16.04
        private void CreateBaseAlbum()
        {
            if (Albums.Count == 0)
            {
                AddAlbum("OtherPhotos", 0, null, null, true, "Generic");
            }
        }
        private void CreateBasePlace()
        {
            if (Places.FirstOrDefault(e => e.Name == "NoPlace") == null)
            {
                AddPlace("NoPlace", null, null, null);
            }
        }
        //~MG 16.04

        private void AddPhotosToAlbum()
        {
            foreach (Album Album in Albums)
            {
                foreach (Photo photo in Photos.Where(e => e.AlbumId == Album.Id))
                {
                    Album.PhotoEntities.Add(photo);
                }
            }
        }
        public void LoadAlbums()
        {
            using var db = new DatabaseContext();
            foreach (var e in db.AlbumEntities)
            {
                Albums.Add(new Album(e));
            }
        }
        public void LoadImages()
        {
            using var db = new DatabaseContext();
            foreach (var e in db.ImageEntities)
            {
                Images.Add(new Image(e));
            }
        }
        public void LoadPhotos()
        {
            using var db = new DatabaseContext();
            foreach (var e in db.PhotoEntities)
            {
                Photos.Add(new Photo(e, Images.FirstOrDefault(ei => ei.Id == e.ImageEntityId).GetEntity(), true));
            }
        }
        public void LoadPlaces()
        {
            using var db = new DatabaseContext();
            foreach (var e in db.PlaceEntities)
            {
                var place = new Place(e);
                if (e.Latitude != null)
                {
                    place.Latitude = e.Latitude;
                }
                if (e.Longitude != null)
                {
                    place.Longitude = e.Longitude;
                }
                Places.Add(place);
            }
        }
        public void LoadAllData()
        {
            LoadAlbums();
            LoadImages();
            LoadPhotos();
            LoadPlaces();
            AddPhotosToAlbum();
        }
        public void ShowData()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("####################PHOTOS####################");
            foreach (var e in Photos)
            {
                Console.WriteLine(e);
            }
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("####################ALBUMS####################");
            foreach (var e in Albums)
            {
                Console.WriteLine(e);
            }
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("####################IMAGES####################");
            foreach (var e in Images)
            {
                Console.WriteLine(e);
            }
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("####################PLACES####################");
            foreach (var e in Places)
            {
                Console.WriteLine(e);
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void SaveChanges()
        {
            using var db = new DatabaseContext();
            foreach (var e in Albums)
            {
                db.AlbumEntities.Update(e.GetEntity());
            }
            foreach (var e in Images)
            {
                db.ImageEntities.Update(e.GetEntity());
            }
            foreach (var e in Places)
            {
                db.PlaceEntities.Update(e.GetEntity());
            }
            foreach (var e in Photos)
            {
                db.PhotoEntities.Update(e.GetEntity());
            }
            db.SaveChanges();
        }

        //Adding new records
        public bool AddAlbum(string name, int count, List<string>? tags, DateTime? date, bool isLocal, string? colorGroup)
        {
            var id = Albums.Count == 0 ? 0 : Albums.OrderByDescending(e => e.Id).FirstOrDefault()!.Id;

            if (Albums.FirstOrDefault(e => e.Name.Equals(name)) != null)
            {
                MessageBox.Show("Unable to add album. Album with that name already exists. Try again.", "Add Album Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //throw new InvalidDataException("Album with that name already exists.");
                return false;
            }
         
            var album = new Album(id + 1, name, count, tags, date, isLocal, colorGroup);
            Albums.Add(album);

            using var db = new DatabaseContext();
            db.AlbumEntities.Add(album.GetEntity());
            db.SaveChanges();
            return true;
        }
        public bool AddImage(string source, int width, int height, double size)
        {
            var id = Images.Count == 0 ? 0 : Images.OrderByDescending(e => e.Id).FirstOrDefault()!.Id;

            if (Images.FirstOrDefault(e => e.Source.Equals(source)) != null)
            {
                MessageBox.Show("Unable to add photo. Image source is already in database. Try again.", "Photo Update Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //throw new InvalidDataException("Image source is already in database.");
                return false;
            }

            var image = new Image(id + 1, source, width, height, size);
            Images.Add(image);

            using var db = new DatabaseContext();
            db.ImageEntities.Add(image.GetEntity());
            db.SaveChanges();
            return true;
        }
        public void AddPlace(string name, double? lat, double? lon, string? markerColor)
        {
            var id = Places.Count == 0 ? 0 : Places.OrderByDescending(e => e.Id).FirstOrDefault()!.Id;

            if (Places.FirstOrDefault(e => e.Name.Equals(name)) != null)
            {
                throw new InvalidDataException("Place is already in database.");
            }

            var place = new Place(id + 1, name, lat, lon, markerColor);
            Places.Add(place);

            using var db = new DatabaseContext();
            db.PlaceEntities.Add(place.GetEntity());
            db.SaveChanges();
        }
        public void AddPhoto(string title, int? albumId, string? tags, DateTime? date, int? placeId, int imageId, double memorySize)
        {
            var id = Photos.Count == 0 ? 0 : Photos.OrderByDescending(e => e.Id).FirstOrDefault()!.Id;

            if (Photos.FirstOrDefault(e => e.Title.Equals(title)) != null)
            {
                throw new InvalidDataException("Photo is already in database.");
            }
            if (albumId != null && Albums.FirstOrDefault(e => e.Id == albumId) == null)
            {
                throw new InvalidDataException("Wrong album Id.");
            }
            if (Images.FirstOrDefault(e => e.Id == imageId) == null)
            {
                throw new InvalidDataException("Wrong image Id.");
            }
            if (placeId != null && Places.FirstOrDefault(e => e.Id == placeId) == null)
            {
                throw new InvalidDataException("Wrong place Id.");
            }
            var photo = new Photo(id + 1, title, albumId, tags, date, placeId, imageId, memorySize, true);
            AddPhotoToAlbum(Albums.FirstOrDefault(e => e.Id == albumId), photo);
            Photos.Add(photo);
            using var db = new DatabaseContext();
            db.PhotoEntities.Add(photo.GetEntity());
            db.SaveChanges();

        }
        private void AddPhotoToAlbum(Album album, Photo photo)
        {
            album.PhotoEntities.Add(photo);
            album.PhotoCount++;
            if(album.PhotoCount == 1)
            {
                AddCoverPhotoToAlbum(album, photo);
            }
            UpdateAlbum(album.Id, null, album.PhotoCount);
            AddAlbumTags(album, photo);

        }
        private void AddCoverPhotoToAlbum(Album album, Photo photo)
        {
            UpdateAlbum(album.Id, null, null, null, null, null, null, photo.ImageId);
        }

        //Removing records
        private void RemovePhotoFromAlbum(Album album, Photo photo)
        {
            if(album.CoverPhotoId == photo.Id)
            {
                album.CoverPhotoId = null;
            }
            album.PhotoEntities.Remove(photo);
            album.PhotoCount--;
            UpdateAlbum(album.Id, null, album.PhotoCount);
            RemoveAlbumTags(album, photo);
        }
        public void RemoveAlbum(int id)
        {
            var album = Albums.FirstOrDefault(e => e.Id == id);
            if (album == null)
            {
                throw new InvalidDataException("Album doesn't exist.");
            }

            Albums.Remove(album);

            using var db = new DatabaseContext();
            db.AlbumEntities.Remove(album.GetEntity());
            db.SaveChanges();
        }
        public void RemoveImage(int id)
        {
            var image = Images.FirstOrDefault(e => e.Id == id);
            if (image == null)
            {
                throw new InvalidDataException("Image doesn't exist.");
            }

            Images.Remove(image);

            using var db = new DatabaseContext();
            db.ImageEntities.Remove(image.GetEntity());
            db.SaveChanges();
        }
        public void RemovePlace(int id)
        {
            var place = Places.FirstOrDefault(e => e.Id == id);
            if (place == null)
            {
                throw new InvalidDataException("Place doesn't exist.");
            }

            Places.Remove(place);

            using var db = new DatabaseContext();
            db.PlaceEntities.Remove(place.GetEntity());
            db.SaveChanges();
        }
        public void RemovePhoto(int id)
        {
            var photo = Photos.FirstOrDefault(e => e.Id == id);
            if (photo == null)
            {
                throw new InvalidDataException("Photo doesn't exist.");
            }
            RemovePhotoFromAlbum(Albums.FirstOrDefault(e => e.Id == photo.AlbumId), photo);
            Photos.Remove(photo);

            using var db = new DatabaseContext();
            db.PhotoEntities.Remove(photo.GetEntity());
            db.SaveChanges();
        }

        public void RemoveAllAlbumPhotos(int albumId)
        {
            using var db = new DatabaseContext();
            var photosToDelete = Photos.Where(e => e.AlbumId == albumId);
            foreach (var photo in photosToDelete)
            {
                var imageToDelete = Images.FirstOrDefault(e => e.Id == photo.ImageId);
                if (imageToDelete != null)
                {
                    Images.Remove(imageToDelete);
                    File.Delete(DataHandler.GetDatabaseImageDirectory() + "\\" + imageToDelete.Source);
                    db.ImageEntities.Remove(imageToDelete.GetEntity());
                }
                db.PhotoEntities.Remove(photo.GetEntity());
            }

            Photos.RemoveAll(e => e.AlbumId == albumId);
            db.SaveChanges();
        }

        //Updating Records
        public void UpdateAlbum(int id, string? name = null, int? count = null, List<string>? tags = null, 
                                DateTime? date = null, bool? isLocal = null, string? ColorGroup = null, int? coverImageId = null)
        {
            var album = Albums.FirstOrDefault(e => e.Id == id);

            if (album == null)
            {
                throw new InvalidDataException("Album doesn't exist.");
            }

            album.Name = name ?? album.Name;
            album.PhotoCount = count ?? album.PhotoCount;
            album.Tags = tags ?? album.Tags;
            album.CreationDate = date ?? album.CreationDate;
            album.IsLocal = isLocal ?? album.IsLocal;
            album.ColorGroup = ColorGroup ?? album.ColorGroup;
            album.CoverPhotoId = coverImageId ?? album.CoverPhotoId;
            using var db = new DatabaseContext();
            db.AlbumEntities.Update(album.GetEntity());
            db.SaveChanges();
        }
        public void UpdateImage(int id, string? source = null, int? width = null, int? height = null, double? size = null)
        {
            var image = Images.FirstOrDefault(e => e.Id == id);

            if (image == null)
            {
                throw new InvalidDataException("Image doesn't exist.");
            }

            image.Source = source ?? image.Source;
            image.ResolutionHeight = height ?? image.ResolutionHeight;
            image.ResolutionWidth = width ?? image.ResolutionWidth;
            image.Size = size ?? image.Size;
            using var db = new DatabaseContext();
            db.ImageEntities.Update(image.GetEntity());
            db.SaveChanges();
        }
        public void UpdatePlace(int id, string? name = null, double? lat = null, double? lon = null)
        {
            var place = Places.FirstOrDefault(e => e.Id == id);

            if (place == null)
            {
                throw new InvalidDataException("Place doesn't exist.");
            }

            place.Name = name ?? place.Name;
            place.Latitude = lat ?? place.Latitude;
            place.Longitude = lon ?? place.Longitude;
            using var db = new DatabaseContext();
            db.PlaceEntities.Update(place.GetEntity());
            db.SaveChanges();
        }
        public void UpdatePhoto(int id, string? title=null, string? album=null, string? rawTags = null, DateTime? date = null, string? place = null)
        {
            var photo = Photos.FirstOrDefault(e => e.Id == id);

            if (photo == null)
            {
                throw new InvalidDataException("Photo doesn't exist.");
            }
            //if (title != null && Photos.FirstOrDefault(e => e.Title.Equals(title)) != null)
            //{
            //    throw new InvalidDataException("Photo title is already taken.");
            //}
            if (album != null && Albums.FirstOrDefault(e => e.Name == album) == null)
            {
                throw new InvalidDataException("Wrong album Id.");
            }
            if (place != null && Places.FirstOrDefault(e => e.Name == place) == null)
            {
                throw new InvalidDataException("Wrong place Id.");
            }
            using var db = new DatabaseContext();
            if (Albums.FirstOrDefault(e => e.Id == photo.AlbumId).CoverPhotoId == photo.ImageId)
            {
                var albumObj = Albums.FirstOrDefault(e => e.Id == photo.AlbumId);
                albumObj.CoverPhotoId = null;
                db.AlbumEntities.Update(albumObj.GetEntity());
            }
            RemovePhotoFromAlbum(Albums.FirstOrDefault(e => e.Id == photo.AlbumId), photo);
            photo.Title = title ?? photo.Title;
            photo.AlbumId = Albums.FirstOrDefault(e => e.Name == album) != null ? Albums.FirstOrDefault(e => e.Name == album).Id : photo.AlbumId;
            photo.RawTags = rawTags ?? photo.RawTags;
            photo.DateTaken = date ?? photo.DateTaken;
            photo.PlaceId = Places.FirstOrDefault(e => e.Name == place) != null ? Places.FirstOrDefault(e => e.Name == place).Id : photo.PlaceId;
            AddPhotoToAlbum(Albums.FirstOrDefault(e => e.Id == photo.AlbumId), photo);
            db.PhotoEntities.Update(photo.GetEntity());
            db.SaveChanges();
        }
        //Utility methods
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
        public ObservableCollection<string> GetPlacesList(bool addStar)
        {
            var placesCollection = new ObservableCollection<string>();
            if (addStar)
            {
                placesCollection.Add("*");
            }
            foreach (var place in Places)
            {
                placesCollection.Add(place.Name);
            }
            return placesCollection;
        }
        private void AddAlbumTags(Album album, Photo photo)
        {
            List<string> tagsToAdd = new List<string>(album.Tags);
            tagsToAdd.Remove("#none");
            foreach (string tag in photo.Tags)
            {
                if (!(album.Tags.Contains(tag)))
                {
                    tagsToAdd.Add(tag);
                }
            }
            if (tagsToAdd.Count == 0)
                tagsToAdd.Add("#none");
            album.Tags = tagsToAdd;
            UpdateAlbum(album.Id, null, null, album.Tags);
        }

        private void RemoveAlbumTags(Album album, Photo photo)
        {
            List<string> newTagsList = new List<string>(album.Tags);

            foreach (string tag in photo.Tags)
            {
                if (!(album.PhotoEntities.Any(e => e.Tags.Contains(tag) && e.Id != photo.Id)))
                {
                    newTagsList.Remove(tag);
                }
            }
            if(newTagsList.Count == 0)
            {
                newTagsList.Add("#none");
            }
            album.Tags = newTagsList;
            UpdateAlbum(album.Id, null, null, album.Tags);
        }
    }
}
