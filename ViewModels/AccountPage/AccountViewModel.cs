using iPhoto.ViewModels.AccountPage;
using iPhoto.RemoteDatabase;

namespace iPhoto.ViewModels
{
    public class AccountViewModel : ViewModelBase
    {
        private ViewModelBase? _currentViewModel;
        private RemoteDatabaseHandler _remoteDatabaseHandler;

        public LogInViewModel LogInViewModel;
        public LoggedInAuthViewModel LoggedInViewModel;

        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }
        public AccountViewModel(RemoteDatabaseHandler remoteDatabase)
        {
            _remoteDatabaseHandler = remoteDatabase;

            LoggedInViewModel = new LoggedInAuthViewModel(this, remoteDatabase);
            LogInViewModel = new LogInViewModel(this);

            CurrentViewModel = LogInViewModel;
        }
    }
}
