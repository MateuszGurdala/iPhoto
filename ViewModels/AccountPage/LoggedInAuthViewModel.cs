using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
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

        public LoggedInAuthViewModel(RemoteDatabaseHandler remoteDatabase)
        {
            _remoteDatabase = remoteDatabase;
            RecentChanges = new ObservableCollection<RecentChangesInfo>();
            OnlineAlbums = new ObservableCollection<OnlineAlbumViewModel>();

            _remoteDatabase.LoadAllData();
            LoadAlbums();

            AccountData = new AccountDataModel()
            {
                Account = "Admin",
                AlbumCount = "2",
                Email = "igor.potezny@gmail.com",
                LastLoggedOn = "16.05.2022",
                Name = "Igor",
                Surname = "Kraszor"
            };

            RecentChanges.Add(new RecentChangesInfo()
            {
                DateAndTime = "16.05.2022 16:58",
                Message = "User GreysonKrystian added 3 new photos to album CepyPlusPlus"
            });
        }
        public async void LoadAlbums()
        {
            await Task.Delay(1000);
            foreach (var album in _remoteDatabase.Albums)
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
        }
    }
}
