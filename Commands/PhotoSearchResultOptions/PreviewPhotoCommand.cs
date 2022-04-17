using iPhoto.ViewModels;
using iPhoto.Views.UserControls;
using System.Windows;

namespace iPhoto.Commands
{
    public class PreviewPhotoCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var clickedPhoto = parameter as PhotoSearchResultViewModel;
            var popup = new PhotoPreviewWindow(Application.Current.MainWindow, clickedPhoto.PhotoData.GetImage());
        }
    }
}
