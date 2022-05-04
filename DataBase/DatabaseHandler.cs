using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

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
            if (Albums.FirstOrDefault(e => e.Id == 1) == null)
            {
                AddAlbum("OtherPhotos", 0, null, null, true, null);
            }
        }
        private void CreateBasePlace()
        {
            if (Places.FirstOrDefault(e => e.Id == 1) == null)
            {
                AddPlace("NoPlace", null, null);
            }
        }
        //~MG 16.04

        private void AddPhotosToAlbum()
        {
            foreach (Album Album in Albums)
            {
                foreach(Photo photo in Photos.Where(e => e.AlbumId == Album.Id))
                {
                    Album.PhotoEntities.Add(photo);
                    Album.PhotoCount++;
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
                Photos.Add(new Photo(e));
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
        public void AddAlbum(string name, int count, List<string>? tags, DateTime? date, bool isLocal, string? colorGroup)
        {
            var id = Albums.Count == 0 ? 0 : Albums.OrderByDescending(e => e.Id).FirstOrDefault()!.Id;

            if (Albums.FirstOrDefault(e => e.Name.Equals(name)) != null)
            {
                throw new InvalidDataException("Album with that name already exists.");
            }

            var album = new Album(id + 1, name, count, tags, date, isLocal, colorGroup);
            Albums.Add(album);

            using var db = new DatabaseContext();
            db.AlbumEntities.Add(album.GetEntity());
            db.SaveChanges();
        }
        public void AddImage(string source, int width, int height, double size)
        {
            var id = Images.Count == 0 ? 0 : Images.OrderByDescending(e => e.Id).FirstOrDefault()!.Id;

            if (Images.FirstOrDefault(e => e.Source.Equals(source)) != null)
            {
                throw new InvalidDataException("Image source is already in database.");
            }

            var image = new Image(id + 1, source, width, height, size);
            Images.Add(image);

            using var db = new DatabaseContext();
            db.ImageEntities.Add(image.GetEntity());
            db.SaveChanges();
        }
        public void AddPlace(string name, double? lat, double? lon)
        {
            var id = Places.Count == 0 ? 0 : Places.OrderByDescending(e => e.Id).FirstOrDefault()!.Id;

            if (Places.FirstOrDefault(e => e.Name.Equals(name)) != null)
            {
                throw new InvalidDataException("Place is already in database.");
            }

            var place = new Place(id + 1, name, lat, lon);
            Places.Add(place);

            using var db = new DatabaseContext();
            db.PlaceEntities.Add(place.GetEntity());
            db.SaveChanges();
        }
        public void AddPhoto(string title, int? albumId, string? tags, DateTime? date, int? placeId, int imageId)
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
            var photo = new Photo(id + 1, title, albumId, tags, date, placeId, imageId);
            AddPhotoToAlbum(Albums.FirstOrDefault(e => e.Id == albumId), photo);
            Photos.Add(photo);
            using var db = new DatabaseContext();
            db.PhotoEntities.Add(photo.GetEntity());
            db.SaveChanges();

        }
        // KG 03.05 small bugfix
        private void AddPhotoToAlbum(Album album, Photo photo)
        {
            album.PhotoEntities.Add(photo);
            //album.TotalSize += photo. 03.05 KG TODO
            album.PhotoCount++;
        }
        //Removing records
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

            Photos.Remove(photo);

            using var db = new DatabaseContext();
            db.PhotoEntities.Remove(photo.GetEntity());
            db.SaveChanges();
        }
        //Updating Records
        public void UpdateAlbum(int id, string? name, int? count, List<string>? tags, DateTime? date, bool? isLocal)
        {
            var album = Albums.FirstOrDefault(e => e.Id == id);

            if (album == null)
            {
                throw new InvalidDataException("Album doesn't exist.");
            }

            album.Name = name ?? album.Name;
            album.PhotoCount = count ?? album.PhotoCount;
            album.Tags = tags!;
            album.CreationDate = date ?? album.CreationDate;
            album.IsLocal = isLocal ?? album.IsLocal;
        }
        public void UpdateImage(int id, string? source, int? width, int? height, double? size)
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
        }
        public void UpdatePlace(int id, string? name, double? lat, double? lon)
        {
            var place = Places.FirstOrDefault(e => e.Id == id);

            if (place == null)
            {
                throw new InvalidDataException("Place doesn't exist.");
            }

            place.Name = name ?? place.Name;
            place.Latitude = lat ?? place.Latitude;
            place.Longitude = lon ?? place.Longitude;
        }
        public void UpdatePhoto(int id, string? title, int? albumId, List<string>? tags, DateTime? date, int? placeId)
        {
            var photo = Photos.FirstOrDefault(e => e.Id == id);

            if (photo == null)
            {
                throw new InvalidDataException("Photo doesn't exist.");
            }
            if (title != null && Photos.FirstOrDefault(e => e.Title.Equals(title)) != null)
            {
                throw new InvalidDataException("Photo title is already taken.");
            }
            if (albumId != null && Albums.FirstOrDefault(e => e.Id == albumId) == null)
            {
                throw new InvalidDataException("Wrong album Id.");
            }
            if (placeId != null && Places.FirstOrDefault(e => e.Id == albumId) == null)
            {
                throw new InvalidDataException("Wrong place Id.");
            }

            photo.Title = title ?? photo.Title;
            photo.AlbumId = albumId ?? photo.AlbumId;
            photo.Tags = tags ?? photo.Tags;
            photo.DateTaken = date ?? photo.DateTaken;
            photo.PlaceId = placeId ?? photo.PlaceId;
        }
        //Utility methods
        public ObservableCollection<string> GetAlbumList()
        {
            var albumList = new List<string>();
            var albumCollection = new ObservableCollection<string>();
            albumList.Add("*");
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
    }
}
