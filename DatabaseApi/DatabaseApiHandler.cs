using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
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
        public async void GetPhotos()
        {
            var photosApiResult = await _httpClient.GetStringAsync(_apiUrl + "photos");
            _apiPhotoObjects = JsonSerializer.Deserialize<List<ApiPhotoObject>>(photosApiResult);
        }
        public async void GetAlbums()
        {
            var photosApiResult = await _httpClient.GetStringAsync(_apiUrl + "groups");
            _apiAlbumObjects = JsonSerializer.Deserialize<List<ApiAlbumObject>>(photosApiResult);
        }
        public async void GetPlaces()
        {
            var photosApiResult = await _httpClient.GetStringAsync(_apiUrl + "places");
            _apiPlaceObjects = JsonSerializer.Deserialize<List<ApiPlaceObject>>(photosApiResult);
        }
        public async void GetImages()
        {
            var photosApiResult = await _httpClient.GetStringAsync(_apiUrl + "images");
            _apiImageObjects = JsonSerializer.Deserialize<List<ApiImageObject>>(photosApiResult);
        }
        private void SetHandler()
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
