using iPhoto.ViewModels;
using iPhoto.ViewModels.SearchPage;
using iPhoto.Views.SearchPage;

namespace iPhoto.Commands.PhotoSearchResultOptions
{
    public class ChangePhotoDetailsCommand : CommandBase
    {
        private readonly PhotoSearchResultViewModel _viewModel;
        private readonly AlbumViewModel _albumVM;
        public ChangePhotoDetailsCommand(PhotoSearchResultViewModel viewModel)
        {
            _viewModel = viewModel;;
        }
        public override void Execute(object parameter)
        {
            var dataContext = new ChangePhotoDetailsViewModel(_viewModel);
            var popup = new AddPhotoPopupView(dataContext);
        }
    }
}
