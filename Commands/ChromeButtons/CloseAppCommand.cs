using System.Windows;

namespace iPhoto.Commands
{
    public class CloseAppCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
