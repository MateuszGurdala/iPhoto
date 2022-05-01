using System;
using System.Collections.Generic;
using System.IO;

namespace iPhoto.DataBase
{
    public class Album
    {
        private readonly AlbumEntity _albumEntity;
        private readonly string[] validDateFormats = { "dd.MM.yyyy HH:mm:ss", "dd.MM.yyyy" };
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
            get => DateTime.ParseExact(_albumEntity.CreationDate, validDateFormats , null);
            set => _albumEntity.CreationDate = value.ToString();
        }
        public bool IsLocal
        {
            get => _albumEntity.IsLocal;
            set => _albumEntity.IsLocal = value;
        }
        public string ColorGroup
        {
            get => _albumEntity.ColorGroup;
            set => _albumEntity.ColorGroup = value;
        }

        public Album(AlbumEntity albumEntity)
        {
            _albumEntity = albumEntity;
        }
        public Album(int id, string name, int count, List<string>? tags, DateTime? date, bool isLocal, string? colorGroup)
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
            IsLocal = isLocal;
            ColorGroup = colorGroup ?? "None";
        }

        private List<string> ParseTags(string tags)
        {
            var list = new List<string>();
            var tagsParsed = tags.Split('#');
            for (int i = 1; i < tagsParsed.Length; i++)
            {
                list.Add('#' + tagsParsed[i]);
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
