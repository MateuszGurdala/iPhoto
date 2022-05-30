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
            var searchViewModel = ((MainWindowViewModel)App.Current.MainWindow.DataContext).SearchViewModel;

            var id = viewModel.GetPhotoId();

            if (searchViewModel.DatabaseHandler.Photos.FirstOrDefault(e => e.Id == id) != null)
            {
                File.Delete(DataHandler.GetDatabaseImageDirectory() + "\\" + viewModel!.GetImageSource());
                viewModel!.Database.RemovePhoto(viewModel.GetPhotoId());
                viewModel.Database.RemoveImage(viewModel.GetImageId());
            }
            else if (searchViewModel.RemoteDatabaseHandler.Photos.FirstOrDefault(e => e.Id == id) != null)
            {
                searchViewModel.RemoteDatabaseHandler.DeletePhoto(id);
            }
            viewModel.PhotoSearchResultsCollection.Remove(viewModel);
        }
    }
}
