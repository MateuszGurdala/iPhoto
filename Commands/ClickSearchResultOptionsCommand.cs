using iPhoto.ViewModels;
using iPhoto.Views;

namespace iPhoto.Commands
{
    public class ClickSearchResultOptionsCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            (parameter as PhotoSearchResultViewModel).ClickSearchResultCommand.Execute(parameter);
            var popup = new PhotoSearchResultOptionsView();
        }
    }
}
