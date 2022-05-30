using iPhoto.UtilityClasses;
using iPhoto.ViewModels.AccountPage;

namespace iPhoto.Commands.AccountPage
{
    public class LogInCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var viewModel = parameter as LogInViewModel;

            var username = viewModel.UsernameText;
            var password = viewModel.SecurePassword;

            ApiAuthorizationHandler.Authorize(username, password);

            if (ApiAuthorizationHandler.IsLoggedIn == true)
            {
                viewModel.AccountViewModel.LoggedInViewModel.SetHandler();
                viewModel.AccountViewModel.CurrentViewModel = viewModel.AccountViewModel.LoggedInViewModel;
                viewModel.AccountViewModel.LoggedInViewModel.LoadAlbums();
                viewModel.AccountViewModel.LoggedInViewModel.GetUserData();
            }
            //Process.Start(new ProcessStartInfo
            //{
            //    FileName = "http://weiti.pl",
            //    UseShellExecute = true
            //});
        }
    }
}
