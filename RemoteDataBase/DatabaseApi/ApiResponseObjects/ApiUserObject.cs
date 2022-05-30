using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPhoto.RemoteDataBase.DatabaseApi.ApiResponseObjects
{
    public class ApiUserObject
    {
        public string? username { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? email { get; set; }
        public string? date_joined { get; set; }
    }
}
