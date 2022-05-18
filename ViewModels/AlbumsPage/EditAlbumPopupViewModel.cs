using iPhoto.Commands.AlbumPage;
using iPhoto.Commands.SearchPage;
using iPhoto.DataBase;
using iPhoto.UtilityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace iPhoto.ViewModels.AlbumsPage
{
    public class EditAlbumPopupViewModel
    {
        private readonly Album _album;
        public Album Album => _album;

        public string NameText { get; set; }


        public EditAlbumPopupViewModel(Album album, AlbumViewModel albumViewModel)
        {
            _album = album;
            DiscardCommand = new DiscardCommand();
            UpdateAlbumCommand = new UpdateAlbumCommand(albumViewModel, album);
        }
        
        public BitmapImage AlbumImageSource
        {
            get
            {
                var uri = DataHandler.GetAlbumIconsDirectoryPath() + _album.ColorGroup + "Album.png";
                return new BitmapImage(new Uri(uri));
            }
        }
        
        public ICommand UpdateAlbumCommand { get; }
        public ICommand DiscardCommand { get; }



    }
}
