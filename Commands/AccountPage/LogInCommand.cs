using iPhoto.ViewModels.AccountPage;
using iPhoto.Views.UserControls;
using System.Diagnostics;
using System.Windows.Controls;

namespace iPhoto.Commands.AccountPage
{
    public class LogInCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var viewModel = parameter as LogInViewModel;

            var username = viewModel.UsernameText;
            var password = viewModel.SecurePassword;

            viewModel.

            //Process.Start(new ProcessStartInfo
            //{
            //    FileName = "http://weiti.pl",
            //    UseShellExecute = true
            //});
        }
    }
}
