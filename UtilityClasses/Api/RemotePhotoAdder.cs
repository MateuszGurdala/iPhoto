using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GoogleDriveHandlerDemo;
using iPhoto.RemoteDatabase;

namespace iPhoto.UtilityClasses
{
    public static class RemotePhotoAdder
    {
        //Miscellaneous
        private static RemoteDatabaseHandler _databaseHandler;

        //Photo
        private static string _title;
        private static string? _rawTags;
        private static DateTime _dateCreated;
        private static int? _albumId;
        private static int? _placeId;

        public static void SetRemoteDatabaseHandler(RemoteDatabaseHandler remoteDatabase)
        {
            _databaseHandler = remoteDatabase;
        }
        public static string UploadImage(string fileName, string filePath)
        {
            var stringId = GoogleDriveHandler.UploadFile(fileName, filePath);
            return stringId;
        }

        public static async Task AddImage(string source, double size, int width, int height)
        {
            await _databaseHandler.AddImage(source, size, width, height);
        }
        public async static void AddPhoto(string title, string album,string imageSource, string rawTags, string? creationDateString, string placeTaken, Task task)
        {
            CheckData(title, album, rawTags, creationDateString, placeTaken);
            ParseData(title, album, rawTags, creationDateString, placeTaken);

            await _databaseHandler.AddPhoto(_title, _dateCreated.ToString("yyyy-MM-ddTHH:mm:ssZ"), _rawTags, album, imageSource, task);
        }
        public static void UpdatePhoto(int id, string title, string album, string rawTags, string? creationDateString, string placeTaken)
        {
            CheckUpdateData(title, album, rawTags, creationDateString, placeTaken);
            ParseData(title, album, rawTags, creationDateString, placeTaken);
        }
        private static void CheckData(string title, string album, string? tags, string? creationDateString, string placeTaken)
        {
            if (_databaseHandler.Photos.FirstOrDefault(e => e.Title == title) != null)
            {
                throw new InvalidDataException("Title is already taken");
            }
            if (_databaseHandler.Albums.FirstOrDefault(e => e.Name == album) == null)
            {
                throw new InvalidDataException("Invalid album name.");
            }
            //if (_databaseHandler.Places.FirstOrDefault(e => e.Name == placeTaken) == null)
            //{
            //    throw new InvalidDataException("Invalid place name.");
            //}
            if (tags != null && tags[0] != '#')
            {
                throw new InvalidDataException("Invalid tags format.");
            }
        }
        private static void CheckUpdateData(string title, string album, string? tags, string? creationDateString, string placeTaken)
        {
            if (_databaseHandler.Albums.FirstOrDefault(e => e.Name == album) == null)
            {
                throw new InvalidDataException("Invalid album name.");
            }
            if (_databaseHandler.Places.FirstOrDefault(e => e.Name == placeTaken) == null)
            {
                throw new InvalidDataException("Invalid place name.");
            }
            if (tags != null && tags[0] != '#')
            {
                throw new InvalidDataException("Invalid tags format.");
            }
        }
        private static void ParseData(string title, string album, string rawTags, string? creationDateString, string placeTaken)
        {
            _rawTags = rawTags;
            _dateCreated = creationDateString == "" ? DateTime.Now : DateTime.ParseExact(creationDateString, "dd.MM.yyyy", null);
            _title = title == "Default" ? GenerateDefaultTitle() : title;
            _placeId = _databaseHandler.Places.First(e => e.Name == placeTaken).Id - 1000;
            _albumId = _databaseHandler.Albums.First(e => e.Name == album).Id - 1000;
        }
        private static string GenerateDefaultTitle()
        {
            var title = DateTime.Now.Date.ToShortDateString() + "remotePhoto";
            var number = 0;
            while (_databaseHandler.Photos.FirstOrDefault(e => e.Title == title) != null)
            {
                number += 1;
                title = DateTime.Now.Date.ToShortDateString() + "remotePhoto" + number;
            }

            return title;
        }
    }
}
