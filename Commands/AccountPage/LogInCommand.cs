using iPhoto.UtilityClasses;
using iPhoto.ViewModels.AccountPage;

namespace iPhoto.Commands.AccountPage
{
    public class LogInCommand : CommandBase
    {
        private LogInViewModel _viewModel;
        public override void Execute(object parameter)
        {
            _viewModel = parameter as LogInViewModel;

            var username = _viewModel.UsernameText;
            var password = _viewModel.SecurePassword;

            ApiAuthorizationHandler.Authorize(username, password, this);
        }

        public void StartSession()
        {
            if (ApiAuthorizationHandler.IsLoggedIn == true)
            {
                _viewModel.AccountViewModel.LoggedInViewModel.SetHandler();
                _viewModel.AccountViewModel.CurrentViewModel = _viewModel.AccountViewModel.LoggedInViewModel;
                _viewModel.AccountViewModel.LoggedInViewModel.LoadAlbums();
                _viewModel.AccountViewModel.LoggedInViewModel.GetUserData();
            }
        }
    }
}
