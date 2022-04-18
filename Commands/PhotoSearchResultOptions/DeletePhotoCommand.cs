using System;
using System.IO;
using iPhoto.UtilityClasses;
using iPhoto.ViewModels;

namespace iPhoto.Commands.SearchPage
{
    public class DeletePhotoCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var viewModel = parameter as PhotoSearchResultViewModel;
            File.Delete(DataHandler.GetDatabaseImageDirectory() + "\\" + viewModel.GetImageSource());

            viewModel!.Database.RemovePhoto(viewModel.GetPhotoId());
            viewModel.Database.RemoveImage(viewModel.GetImageId());

            viewModel.PhotoSearchResultsCollection.Remove(viewModel);
        }
    }
}
