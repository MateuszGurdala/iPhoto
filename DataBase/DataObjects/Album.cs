using System;
using System.Collections.Generic;
using System.IO;

namespace iPhoto.DataBase
{
    public class Album
    {
        private readonly AlbumEntity _albumEntity;
        public int Id
        {
            get => _albumEntity.Id;
            set => _albumEntity.Id = value;
        }
        public string Name
        {
            get => _albumEntity.Name;
            set => _albumEntity.Name = value;
        }
        public int PhotoCount
        {
            get => _albumEntity.PhotoCount;
            set => _albumEntity.PhotoCount = value;
        }
        public List<string> Tags
        {
            get => ParseTags(RawTags);
            set => RawTags = String.Concat(value);
        }
        public string RawTags
        {
            get => _albumEntity.Tags;
            set => _albumEntity.Tags = value;
        }
        public DateTime CreationDate
        {
            get => DateTime.Parse(_albumEntity.CreationDate);
            set => _albumEntity.CreationDate = value.ToString();
        }


        public Album(AlbumEntity albumEntity)
        {
            _albumEntity = albumEntity;
        }
        public Album(int id, string name, int count, List<string>? tags, DateTime? date)
        {
            if (count < 0)
            {
                throw new InvalidDataException("Number of pictures cannot be less than zero.");
            }

            _albumEntity = new AlbumEntity();
            Id = id;
            Name = name;
            PhotoCount = count;
            Tags = tags ?? new List<string>() {"#none"};
            CreationDate = date ?? DateTime.Now.Date;
        }

        private List<string> ParseTags(string tags)
        {
            var list = new List<string>();
            var tagsParsed = tags.Split('#');
            foreach (var tag in tagsParsed)
            {
                list.Add('#' + tag);
            }

            return list;
        }
        public AlbumEntity GetEntity()
        {
            return _albumEntity;
        }
        public override string ToString()
        {
            return $"{Id} | {Name} | {PhotoCount} | {RawTags ?? "null"} | {CreationDate.Date}";
        }
    }
}
