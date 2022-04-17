using iPhoto.Views.SearchPage;

namespace iPhoto.Commands.SearchPage
{
    public class DiscardCommand: CommandBase
    {
        public override void Execute(object parameter)
        {
            (parameter as AddPhotoPopupView)!.IsOpen = false;
        }
    }
}
