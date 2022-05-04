using System;
using System.IO;
using System.Linq;
using iPhoto.UtilityClasses;
using iPhoto.ViewModels;

namespace iPhoto.Commands.SearchPage
{
    public class DeletePhotoCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var viewModel = parameter as PhotoSearchResultViewModel;
            RemovePhotoFromAlbum(viewModel);
            File.Delete(DataHandler.GetDatabaseImageDirectory() + "\\" + viewModel!.GetImageSource());
            viewModel!.Database.RemovePhoto(viewModel.GetPhotoId());
            viewModel.Database.RemoveImage(viewModel.GetImageId());
            viewModel.PhotoSearchResultsCollection.Remove(viewModel);           
        }
        private void RemovePhotoFromAlbum(PhotoSearchResultViewModel viewModel)
        {
            var album = viewModel.Database.Albums.FirstOrDefault(e => e.Id == viewModel.GetAlbumId());
            var photo = viewModel.Database.Photos.FirstOrDefault(e => e.Id == viewModel.GetPhotoId());
            album.PhotoEntities.Remove(photo);
            //album.TotalSize -= photo. 04.05 KG TODO
            album.PhotoCount--;
        }
    }
}
