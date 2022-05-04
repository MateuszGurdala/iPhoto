using iPhoto.Views.AlbumPage;
using iPhoto.Views.SearchPage;

namespace iPhoto.Commands.SearchPage
{
    public class DiscardCommand: CommandBase
    {
        public override void Execute(object parameter)
        {
            try
            {
                (parameter as AddPhotoPopupView)!.IsOpen = false;
            }
            catch
            {
                (parameter as AddPhotoToAlbumPopupView)!.IsOpen = false;
            }
        }
    }
}
