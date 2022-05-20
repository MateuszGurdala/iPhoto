using iPhoto.DataBase;
using iPhoto.ViewModels.AlbumsPage;
using iPhoto.Views.AlbumPage;
using iPhoto.Views.SearchPage;
using System;
namespace iPhoto.Commands.SearchPage
{
    public class DiscardCommand: CommandBase
    {
        private readonly int? _initAlbumCoverId;
        private readonly string _initAlbumColorGroup;
        private readonly Album _album;
        public DiscardCommand()
        {

        }

        public DiscardCommand(EditAlbumPopupViewModel editVM, Album album)
        {
            _initAlbumCoverId = editVM.InitAlbumCoverId;
            _initAlbumColorGroup = editVM.InitColorGroup;
            _album = album;
        }
        public override void Execute(object parameter)
        {
            if(parameter as AddPhotoPopupView != null)
            { 
                (parameter as AddPhotoPopupView)!.IsOpen = false;
            }
            else if(parameter as AddPhotoToAlbumPopupView != null)
            {
                (parameter as AddPhotoToAlbumPopupView)!.IsOpen = false;
            }
            else
            {
                (parameter as EditAlbumPopupView)!.IsOpen = false;
                _album.ColorGroup = _initAlbumColorGroup;
                _album.CoverPhotoId = _initAlbumCoverId;
            }
        }
    }
}
