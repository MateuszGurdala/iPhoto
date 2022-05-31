using System.Collections.ObjectModel;
using System.Windows.Input;
using iPhoto.Commands.AccountPage;
using iPhoto.Models;
using iPhoto.RemoteDatabase;

namespace iPhoto.ViewModels.AccountPage
{
    public class LoggedInAuthViewModel : ViewModelBase
    {
        private readonly RemoteDatabaseHandler _remoteDatabase;

        private AccountDataModel _accountData;
        public AccountDataModel AccountData
        {
            get
            {
                return _accountData;
            }
            set
            {
                _accountData = value;
                OnPropertyChanged(nameof(AccountData));
            }
        }

        public ObservableCollection<RecentChangesInfo> RecentChanges { get; }
        public ObservableCollection<OnlineAlbumViewModel> OnlineAlbums { get; }
        public AccountViewModel AccountViewModel;
        public ICommand LogOutCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand ToWebPageCommand { get; }
        
        public LoggedInAuthViewModel(AccountViewModel accountViewModel,RemoteDatabaseHandler remoteDatabase)
        {
            AccountViewModel = accountViewModel;
            _remoteDatabase = remoteDatabase;

            LogOutCommand = new LogOutCommand();
            RefreshCommand = new RefreshCommand(this);
            ToWebPageCommand = new ToWebPageCommand();

            RecentChanges = new ObservableCollection<RecentChangesInfo>();
            OnlineAlbums = new ObservableCollection<OnlineAlbumViewModel>();

            RecentChanges.Add(new RecentChangesInfo()
            {
                DateAndTime = "16.05.2022 16:58",
                Message = "User GreysonKrystian added 3 new photos to album CepyPlusPlus"
            });
        }
        public async void LoadAlbums()
        {
            var albums = await _remoteDatabase.LoadAlbums();
            OnlineAlbums.Clear();
            foreach (var album in albums)
            {
                OnlineAlbums.Add(new OnlineAlbumViewModel()
                {
                    PhotoCount = album.PhotoCount.ToString(),
                    Name = album.Name,
                    Color = album.ColorGroup,
                    Tags = album.RawTags,
                    LastEdit = album.CreationDate.ToString().Substring(0, 10),
                }) ;
            }
            _remoteDatabase.LoadAllData();
        }
        public void SetHandler()
        {
            _remoteDatabase.SetHandler();
        }
        public void Clear()
        {
            _remoteDatabase.Clear();
            RecentChanges.Clear();
            OnlineAlbums.Clear();
            AccountData = null;
        }
        public async void GetUserData()
        {
            AccountData = await _remoteDatabase.GetUserData();
            UpdateAlbumCount();
        }
        public void UpdateAlbumCount()
        {
            AccountData.AlbumCount = _remoteDatabase.Albums.Count.ToString();

        }
    }
}
