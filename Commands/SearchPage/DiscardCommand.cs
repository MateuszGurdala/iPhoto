using iPhoto.Views.AlbumPage;
using iPhoto.Views.SearchPage;
using System;
namespace iPhoto.Commands.SearchPage
{
    public class DiscardCommand: CommandBase
    {
        public override void Execute(object parameter)
        {
            if(parameter as AddPhotoPopupView != null)
            { 
                (parameter as AddPhotoPopupView)!.IsOpen = false;
            }
            else
            {
                (parameter as AddPhotoToAlbumPopupView)!.IsOpen = false;
            }
        }
    }
}
