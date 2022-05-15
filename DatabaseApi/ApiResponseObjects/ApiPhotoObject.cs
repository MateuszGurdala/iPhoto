using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleDriveHandlerDemo.ApiHandler.ApiResponseObjects
{
    public class ApiPhotoObject
    {
        public int id { get; set; }
        public string title { get; set; }
        public string date_taken { get; set; }
        public string tags { get; set; }
        public int album { get; set; }
        public int image { get; set; }
        public int place { get; set; }

    }
}
