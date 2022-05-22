using System;
using iPhoto.DataBase;
using iPhoto.UtilityClasses;
using iPhoto.ViewModels;
using iPhoto.ViewModels.SearchPage;

namespace iPhoto.Commands.SearchPage
{
    public class CommitCommand : CommandBase
    {
        //TODO: ADD DATA VERIFICATION
        private readonly bool _update;
        public CommitCommand(bool update)
        {
            _update = update;
        }
        public override void Execute(object parameter)
        {
            if (_update)
            {
                var viewModel = parameter as ChangePhotoDetailsViewModel;

                var id = viewModel!.PhotoId;
                var title = viewModel!.ParentView.Title.ContentTextBox.Text;
                var album = viewModel!.ParentView.Album.Text;
                var rawTags = viewModel!.ParentView.RawTags.ContentTextBox.Text;
                var creationDateString = viewModel!.ParentView.CreationDateString.Text;
                var placeTaken = viewModel!.ParentView.PlaceTaken.ContentTextBox.Text;

                viewModel.PhotoAdder.UpdatePhoto(id,title, album, rawTags, creationDateString, placeTaken);
                viewModel.ParentView.IsOpen = false;

                if (id <= 1000)
                {
                    viewModel.SearchResultViewModel.SearchViewModel.SearchEngine.UpdateSearchResults();
                }
            }
            else
            {
                var viewModel = parameter as AddPhotoPopupViewModel;

                var title = viewModel!.ParentView.Title.ContentTextBox.Text;
                var album = viewModel!.ParentView.Album.Text;
                var rawTags = viewModel!.ParentView.RawTags.ContentTextBox.Text;
                var creationDateString = viewModel!.ParentView.CreationDateString.Text;
                var placeTaken = viewModel!.ParentView.PlaceTaken.ContentTextBox.Text;

                viewModel.PhotoAdder.AddPhoto(title, album, rawTags, creationDateString, placeTaken);
            }
        }
    }
}
