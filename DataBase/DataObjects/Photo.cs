using System;
using System.Collections.Generic;

namespace iPhoto.DataBase
{
    public class Photo
    {
        private readonly PhotoEntity _photoEntity;
        public int Id
        {
            get => _photoEntity.Id;
            set => _photoEntity.Id = value;
        }
        public string Title
        {
            get => _photoEntity.Title;
            set => _photoEntity.Title = value;
        }
        public int AlbumId
        {
            get => _photoEntity.AlbumEntityId;
            set => _photoEntity.AlbumEntityId = value;
        }
        public List<string> Tags
        {
            get => ParseTags(RawTags);
            set => RawTags = String.Concat(value);
        }
        public string RawTags
        {
            get => _photoEntity.Tags;
            set => _photoEntity.Tags = String.Concat(value);
        }
        public DateTime DateTaken
        {
            get => DateTime.ParseExact(_photoEntity.DateTaken, "dd.MM.yyyy HH:mm:ss", null);
            set => _photoEntity.DateTaken = value.ToString();
        }
        public int PlaceId
        {
            get => _photoEntity.PlaceEntityId;
            set => _photoEntity.PlaceEntityId = value;
        }
        public int ImageId
        {
            get => _photoEntity.ImageEntityId;
            set => _photoEntity.ImageEntityId = value;
        }


        public Photo(PhotoEntity photoEntity)
        {
            _photoEntity = photoEntity;
        }
        public Photo(int id, string title, int? albumId, string? tags, DateTime? date, int? placeId, int imageId)
        {
            
            Id = id;
            Title = title;
            AlbumId = albumId ?? 1; //Default "OtherPhotos" album Id
            Tags = tags == null ? new List<string>() { "#none" } : ParseTags(tags)!;
            DateTaken = date ?? DateTime.Now;
            PlaceId = placeId ?? 1; //Default "No place" place Id
            ImageId = imageId;
            _photoEntity = new PhotoEntity(); // Change This to full constructor ??? KG 03.05
        }
        private List<string>? ParseTags(string tags)
        {
            var list = new List<string>();
            var tagsParsed = tags.Split('#');
            for (int i = 1; i < tagsParsed.Length; i++)
            {
                list.Add('#' + tagsParsed[i]);
            }
            return list;
        }
        public PhotoEntity GetEntity()
        {
            return _photoEntity;
        }
        public override string ToString()
        {
            return $"{Id} | {Title} | {AlbumId} | {RawTags ?? "null"} | {DateTaken.ToString() ?? "null"} | {PlaceId} | {ImageId}";
        }
    }
}
