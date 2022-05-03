using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iPhoto.ViewModels.AlbumsPage
{
    public class AlbumOptionsViewModel
    {
        public ICommand ShowAlbumContentCommand { get; }
        public ICommand RenameAlbumCommand { get; }
        public ICommand DeleteAlbumCommand { get; }
        public ICommand ChangeAlbumColorCommand { get; }
    }
}
