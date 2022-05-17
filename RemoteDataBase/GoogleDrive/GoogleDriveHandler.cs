using System.Collections.Generic;
using System.IO;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using iPhoto.UtilityClasses;

namespace GoogleDriveHandlerDemo
{
    public class GoogleDriveHandler
    {
        private static string[] Scopes = { DriveService.Scope.Drive, DriveService.Scope.DriveFile };
        private static string ApplicationName = "Drive API .NET Quickstart";
        private string _databaseAlbumId = "19oGqH41C6V7pvHTTC98fhMiGqMH0Of3H";
        private Dictionary<string, string> _mimeKeyDictionary = new()
        {
            {".jpg", "image/jpeg"},
            {".jpeg", "image/jpeg"},
            {".png", "image/png"}
        };

        private DriveService _driveService;
        public readonly List<GoogleDatabaseFile> DatabaseFiles;

        public GoogleDriveHandler()
        {
            DatabaseFiles = new List<GoogleDatabaseFile>();
            CreateDriveService();
        }

        public void LoadAllData()
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
        private void CreateDriveService()
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
        public string UploadFile(string fileName, string filePath)
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
        public void DeleteFile(GoogleDatabaseFile file)
        {
            var command = _driveService.Files.Delete(file.StringId);
            var result = command.Execute();
        }
        private string CreateFolder(string folderName)
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
    }
}
