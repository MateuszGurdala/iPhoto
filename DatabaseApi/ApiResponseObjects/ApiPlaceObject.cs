using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleDriveHandlerDemo.ApiHandler.ApiResponseObjects
{
    public class ApiPlaceObject
    {
        public int id { get; set; }
        public string name { get; set; }
        public string? latitude { get; set; }
        public string? longitude { get; set; }
    }
}
