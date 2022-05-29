using System;
using System.Globalization;
using iPhoto.RemoteDataBase.DatabaseApi.ApiResponseObjects;

namespace iPhoto.Models
{
    public class AccountDataModel
    {
        public string Account { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        private int _albumCount;
        public string AlbumCount
        {
            get
            {
                return _albumCount.ToString();
            }
            set
            {
                _albumCount = int.Parse(value);
            }
        }
        public string Email { get; set; }
        public string LastLoggedOn { get; set; }

        public AccountDataModel(ApiUserObject apiUserObject)
        {
            Account = apiUserObject.username;
            Name = apiUserObject.first_name;
            Surname = apiUserObject.last_name;
            Email = apiUserObject.email;
            LastLoggedOn = DateTime.Parse(apiUserObject.date_joined.Substring(0, 10), CultureInfo.InvariantCulture)
                .ToString("dd.MM.yyyy HH:mm:ss");
        }
    }
}
