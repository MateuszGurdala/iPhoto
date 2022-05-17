using iPhoto.ViewModels.AccountPage;
using iPhoto.RemoteDatabase;

namespace iPhoto.ViewModels
{
    public class AccountViewModel : ViewModelBase 
    {
        private ViewModelBase? _currentViewModel;
        private RemoteDatabaseHandler _remoteDatabaseHandler;
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
            CurrentViewModel = new LogInViewModel();
            //CurrentViewModel = new LoggedInAuthViewModel(remoteDatabase);
        }
    }
}
