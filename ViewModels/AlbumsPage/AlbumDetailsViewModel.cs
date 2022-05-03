using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace iPhoto.ViewModels.AlbumsPage
{
    public class AlbumDetailsViewModel
    {
        public string Name { get; set; }

        public int PhotoCount { get; set; }
        public string Tags { get; set; }
        public string CreationDate { get; set; }
        public string CoverPhoto { get; set; }
        public string Source { get; set; }
        public string ColorGroupName { get; set; }
        public SolidColorBrush ĆolorGroupBrush { get; set; }
        
    }
}
