using iPhoto.ViewModels.AlbumsPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPhoto.Commands.AlbumPage
{
    public class CommitWithGivenAlbumCommand : CommandBase
    {
        private readonly AlbumContentViewModel _albumVm;

        public CommitWithGivenAlbumCommand(AlbumContentViewModel albumVm)
        {
            _albumVm = albumVm;
        }
        public override void Execute(object parameter)
        {
            var viewModel = parameter as AddPhotoToAlbumPopupViewModel;

            var title = viewModel!.ParentView.Title.ContentTextBox.Text;
            var album = viewModel!.CurrentAlbum.Name;
            var rawTags = viewModel!.ParentView.RawTags.ContentTextBox.Text;
            var creationDateString = viewModel!.ParentView.CreationDateString.ContentTextBox.Text;
            var placeTaken = viewModel!.ParentView.PlaceTaken.ContentTextBox.Text;

            viewModel.PhotoAdder.AddPhoto(title, album, rawTags, creationDateString, placeTaken);
            _albumVm.LoadAllAlbumPhotos();
        }
    }
}
