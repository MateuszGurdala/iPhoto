using System;
using iPhoto.UtilityClasses;
using iPhoto.ViewModels.SearchPage;

namespace iPhoto.Commands.SearchPage
{
    public class CommitCommand : CommandBase
    {
        //TODO: ADD DATA VERIFICATION
        public override void Execute(object parameter)
        {
            var viewModel = parameter as AddPhotoPopupViewModel;

            var title = viewModel!.ParentView.Title.ContentTextBox.Text;
            var album = viewModel!.ParentView.Album.Text;
            var rawTags = viewModel!.ParentView.RawTags.ContentTextBox.Text;
            //var creationDateString = viewModel!.ParentView.CreationDateString./*ContentTextBox*/.Text;
            var creationDateString = viewModel!.ParentView.CreationDateString.Text;
            var placeTaken = viewModel!.ParentView.PlaceTaken.ContentTextBox.Text;

            viewModel.PhotoAdder.AddPhoto(title, album, rawTags, creationDateString, placeTaken);
        }
    }
}
