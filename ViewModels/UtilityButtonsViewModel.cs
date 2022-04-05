using iPhoto.Commands;
using System.Windows.Controls;
using System.Windows.Input;

namespace iPhoto.ViewModels
{
    public class UtilityButtonsViewModel : ViewModelBase
    {
        public ICommand CloseApp { get; }
        public ICommand Minimize { get; }
        public ICommand Resize { get;}
        public ICommand Fullscreen { get; }

        public UtilityButtonsViewModel()
        {
            CloseApp = new CloseAppCommand();
            Resize = new ResizeCommand();
            Fullscreen = new MaximizeCommand();
            Minimize = new MinimizeCommand();
        }
    }
}
