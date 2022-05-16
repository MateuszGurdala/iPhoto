using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using GoogleDriveHandlerDemo.ApiHandler.ApiResponseObjects;

namespace GoogleDriveHandlerDemo.ApiHandler
{
    public class DatabaseApiHandler
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly string _apiUrl = @"http://iphotos-pap.herokuapp.com/api/";
        private List<ApiPhotoObject> _apiPhotoObjects;
        private List<ApiAlbumObject> _apiAlbumObjects;
        private List<ApiPlaceObject> _apiPlaceObjects;
        private List<ApiImageObject> _apiImageObjects;

        public DatabaseApiHandler()
        {
            SetHandler();
        }
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
        private void SetHandler()
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
