using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Media.Imaging;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util;
using Google.Apis.Util.Store;
using iPhoto.UtilityClasses;

namespace GoogleDriveHandlerDemo
{
    public static class GoogleDriveHandler
    {
        private static string[] Scopes = { DriveService.Scope.Drive, DriveService.Scope.DriveFile };
        private static string ApplicationName = "Drive API .NET Quickstart";
        private static string _databaseAlbumId = "19oGqH41C6V7pvHTTC98fhMiGqMH0Of3H";
        private static Dictionary<string, string> _mimeKeyDictionary = new()
        {
            { ".jpg", "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".png", "image/png" }
        };

        private static DriveService _driveService;
        public static readonly List<GoogleDatabaseFile> DatabaseFiles;

        //public GoogleDriveHandler()
        //{
        //    DatabaseFiles = new List<GoogleDatabaseFile>();
        //    CreateDriveService();
        //}

        public static void LoadAllData()
        {
            FilesResource.ListRequest listRequest = _driveService.Files.List();
            listRequest.PageSize = 10;
            listRequest.Fields = "nextPageToken, files(id, name)";

            var driveFiles = listRequest.Execute().Files;
            foreach (var file in driveFiles)
            {
                if (file.Id != _databaseAlbumId)
                {
                    var fileRequest = _driveService.Files.Get(file.Id);
                    DatabaseFiles.Add(new GoogleDatabaseFile(file, fileRequest));
                }
            }
        }
        public static void CreateDriveService()
        {
            UserCredential credential;
            string tokenPath = DataHandler.GetProjectDirectoryPath() + "\\RemoteDatabase\\GoogleDrive\\token.json";
            string credentialsPath = DataHandler.GetProjectDirectoryPath() + "\\RemoteDatabase\\GoogleDrive\\credentials.json";

            using (var stream =
                   new FileStream(@credentialsPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(tokenPath, true)).Result;
            }

            _driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }
        public static string UploadFile(string fileName, string filePath)
        {
            FileStream file = new FileStream(filePath, FileMode.Open);

            var fileMime = _mimeKeyDictionary[Path.GetExtension(filePath)];

            var driveFile = new Google.Apis.Drive.v3.Data.File
            {
                Name = fileName,
                MimeType = fileMime,
                CopyRequiresWriterPermission = false,
                Parents = new List<string>() { _databaseAlbumId }
            };

            var request = _driveService.Files.Create(driveFile, file, fileMime);
            request.Fields = "id";

            var response = request.Upload();

            return request.ResponseBody.Id;
        }
        public static void DeleteFile(string stringId)
        {
            var command = _driveService.Files.Delete(stringId);
            var result = command.Execute();
        }
        private static string CreateFolder(string folderName)
        {
            var driveFolder = new Google.Apis.Drive.v3.Data.File
            {
                Name = folderName,
                MimeType = "application/vnd.google-apps.folder",
            };
            var command = _driveService.Files.Create(driveFolder);
            var file = command.Execute();
            return file.Id;
        }

        public static BitmapImage GetBitmapImage(string id,List<byte> imageBytes, int? decodePixelWidth)
        {
            if (imageBytes.Count == 0)
            {
                var stream = new MemoryStream();
                var fileRequest = _driveService.Files.Get(id);
                fileRequest.Download(stream);
                imageBytes.AddRange(stream.ToArray());
            }

            var imageStream = new MemoryStream(imageBytes.ToArray());

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = imageStream;
            bitmap.CacheOption = BitmapCacheOption.OnDemand;
            if (decodePixelWidth != null)
            {
                bitmap.DecodePixelWidth = (int) decodePixelWidth;
            }
            bitmap.EndInit();

            bitmap.Freeze();

            return bitmap;
        }
    }
}
