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
        private readonly DatabaseHandler _databaseHandler;
        public Album Album => _album;

        public string NameText { get; set; }

        public List<string> PhotosList => _album.PhotoEntities.Select(o => o.Title).ToList();

        public string InitColorGroup { get; }

        public int? InitAlbumCoverId { get; }

        public EditAlbumPopupViewModel(Album album, AlbumViewModel albumViewModel)
        {
            _databaseHandler = albumViewModel.DatabaseHandler;
            _album = album;
            InitColorGroup = album.ColorGroup;
            InitAlbumCoverId = album.CoverPhotoId;
            DiscardCommand = new DiscardCommand(this, album);
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

        public BitmapImage? CoverImageSource
        {
            get
            {
                if (_databaseHandler.Images.FirstOrDefault(o => o.Id == _album.CoverPhotoId) != null)
                {
                    string? imageSource = _databaseHandler.Images.FirstOrDefault(o => o.Id == _album.CoverPhotoId).Source;

                    var uri = DataHandler.GetDatabaseImageDirectory() + "\\" + imageSource;
                    return new BitmapImage(new Uri(uri));
                }
                else
                    return null;
            }
        }

        public ICommand UpdateAlbumCommand { get; }
        public ICommand DiscardCommand { get; }



    }
}
