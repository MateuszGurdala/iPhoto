using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using GoogleDriveHandlerDemo.ApiHandler.ApiResponseObjects;
using iPhoto.Commands.AccountPage;
using iPhoto.UtilityClasses.Api;
using Newtonsoft.Json;

namespace iPhoto.UtilityClasses
{
    public static class ApiAuthorizationHandler
    {
        private static HttpClient _httpClient = new HttpClient();
        public static string SessionCookie { get; set; }
        public static bool IsLoggedIn { get; set; } = false;
        public static string CSRF { get; set; }

        private static void SetHandler()
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async void Authorize(string username, string password, LogInCommand logInCommand)
        {
            SetHandler();
            var credentials = new ApiAuthObject()
            {
                username = username,
                password = password
            };

            string json = JsonConvert.SerializeObject(credentials);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await _httpClient.PostAsync(@"http://iphotos-pap.herokuapp.com/api/login", content);

            if (result.StatusCode == HttpStatusCode.Accepted)
            {
                var headers = result.Headers.ToString().Split("\r\n");
                var cookies = headers[7].Split(';');
                CSRF = cookies[0].Split('=')[1];
                SessionCookie = cookies[4].Split(", ")[1];
                IsLoggedIn = true;

                logInCommand.StartSession();
            }
        }

        public static Cookie GetAuthCookie()
        {
            var name = "sessionid";
            var value = SessionCookie.Split('=')[1];
            var cookie =  new Cookie
            {
                Name = name,
                Value = value,
                Domain = "iphotos-pap.herokuapp.com"
            };
            return cookie;
        }
        public static Cookie GetCSRFCookie()
        {
            var name = "csrftoken";
            var value = CSRF;
            var cookie = new Cookie
            {
                Name = name,
                Value = value,
                Domain = "iphotos-pap.herokuapp.com"
            };
            return cookie;
        }

        public static async void LogOut()
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(ApiAuthorizationHandler.GetAuthCookie());

            var handler = new HttpClientHandler()
            {
                CookieContainer = cookieContainer
            };

            var client = new HttpClient(handler);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.GetAsync(@"http://iphotos-pap.herokuapp.com/api/logout");
        }
    }
}
