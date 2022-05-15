using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleDriveHandlerDemo.ApiHandler.ApiResponseObjects
{
    public class ApiImageObject
    {
        public int id { get; set; }
        public string source { get; set; }
        public int resolutin_width { get; set; }
        public int resolutin_height { get; set; }
        public string size { get; set; }
    }
}
