using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using iPhoto.Views;
using iPhoto.Views.SearchPage;

namespace iPhoto.Commands.SearchPage
{
    public class ShowPhotoDetailsWindowCommand: CommandBase
    {
        private readonly PhotoDetailsWindowView _photoDetailsWindow;
        private readonly ICommand _extendPhotoDetailsCommand;
        public ShowPhotoDetailsWindowCommand(PhotoDetailsWindowView photoDetailsWindow, ExtendPhotoDetailsCommand photoDetailsCommand)
        {
            _photoDetailsWindow = photoDetailsWindow;
            _extendPhotoDetailsCommand = photoDetailsCommand;
        }

        public override void Execute(object parameter)
        {
            var photoDetailsView = parameter as PhotoDetailsSideView;
            var searchView = GetSearchView(photoDetailsView);
            _photoDetailsWindow.Left = (1920 - 362) / 2;
            _photoDetailsWindow.Top = (1080 - 312) / 2;
            _photoDetailsWindow.Show();
            _extendPhotoDetailsCommand.Execute(searchView!.PhotoDetailsColumn);
        }
        private SearchView GetSearchView(PhotoDetailsSideView photoDetailsView)
        {
            var parentGrid = VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(photoDetailsView)) as Grid;
            var searchView = VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(parentGrid))) as SearchView;
            return searchView;
        }
    }
}
