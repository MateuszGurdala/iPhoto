using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using GoogleDriveHandlerDemo.ApiHandler.ApiResponseObjects;
using iPhoto.RemoteDataBase.DatabaseApi.ApiResponseObjects;
using iPhoto.UtilityClasses;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace GoogleDriveHandlerDemo.ApiHandler
{
    public class DatabaseApiHandler
    {
        private static HttpClient _httpClient = new HttpClient();
        private readonly string _apiUrl = @"http://iphotos-pap.herokuapp.com/api/";
        private List<ApiPhotoObject> _apiPhotoObjects;
        private List<ApiAlbumObject> _apiAlbumObjects;
        private List<ApiPlaceObject> _apiPlaceObjects;
        private List<ApiImageObject> _apiImageObjects;

        public async Task<List<ApiPhotoObject>> GetPhotos()
        {
            var photosApiResult = await _httpClient.GetStringAsync(_apiUrl + "photos");
            _apiPhotoObjects = JsonSerializer.Deserialize<List<ApiPhotoObject>>(photosApiResult);
            return _apiPhotoObjects;
        }
        public async Task<List<ApiAlbumObject>> GetAlbums()
        {
            var photosApiResult = await _httpClient.GetStringAsync(_apiUrl + "groups");
            _apiAlbumObjects = JsonSerializer.Deserialize<List<ApiAlbumObject>>(photosApiResult);
            return _apiAlbumObjects;
        }
        public async Task<List<ApiPlaceObject>> GetPlaces()
        {
            var photosApiResult = await _httpClient.GetStringAsync(_apiUrl + "places");
            _apiPlaceObjects = JsonSerializer.Deserialize<List<ApiPlaceObject>>(photosApiResult);
            return _apiPlaceObjects;
        }
        public async Task<List<ApiImageObject>> GetImages()
        {
            var photosApiResult = await _httpClient.GetStringAsync(_apiUrl + "images");
            _apiImageObjects = JsonSerializer.Deserialize<List<ApiImageObject>>(photosApiResult);
            return _apiImageObjects;
        }
        public async Task<ApiUserObject> GetUserData()
        {
            var photosApiResult = await _httpClient.GetStringAsync(_apiUrl + "users");
            var apiUserObject = JsonSerializer.Deserialize<ApiUserObject>(photosApiResult);
            return apiUserObject;
        }
        public void SetHandler()
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(ApiAuthorizationHandler.GetAuthCookie());
            cookieContainer.Add(ApiAuthorizationHandler.GetCSRFCookie());

            var handler = new HttpClientHandler()
            {
                CookieContainer = cookieContainer
            };

            _httpClient = new HttpClient(handler);

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("X-CSRFToken", ApiAuthorizationHandler.CSRF);
        }
        public async Task<string> PostImage(string source, double size, int width, int height)
        {
            var image = new ApiImageObject
            {
                source = source,
                size = size.ToString().Replace(',', '.'),
                resolution_height = height,
                resolution_width = width
            };

            string json = JsonConvert.SerializeObject(image);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await _httpClient.PostAsync(@"http://iphotos-pap.herokuapp.com/api/images", content);
            return await result.Content.ReadAsStringAsync();
        }
        public async Task<string> PostPhoto(string title,int albumId,int imageId,string tags, string creationDate)
        {
            var photo = new ApiPhotoObject()
            {
                title = title,
                album = albumId,
                date_taken = creationDate,
                image = imageId,
                place = 1,
                tags = tags
            };

            string json = JsonConvert.SerializeObject(photo);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await _httpClient.PostAsync(@"http://iphotos-pap.herokuapp.com/api/photos", content);
            return await result.Content.ReadAsStringAsync();
        }
        public async void RemoveImage(int id)
        {
            var result = await _httpClient.DeleteAsync(@"http://iphotos-pap.herokuapp.com/api/images/" + id.ToString());
        }
        public async void RemovePhoto(int id)
        {
            var result = await _httpClient.DeleteAsync(@"http://iphotos-pap.herokuapp.com/api/photos/" + id.ToString());
        }
    }
}
