using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleDriveHandlerDemo.ApiHandler.ApiResponseObjects
{
    public class ApiAlbumObject
    {
        public int id { get; set; }
        public string name { get; set; }
        public int photo_count { get; set; }
        public string tags { get; set; }
        public string color_group { get; set; }
        public bool is_local{ get; set; }
        public string create_time { get; set; }
    }
}
