using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using iPhoto.Models;

namespace iPhoto.ViewModels.AccountPage
{
    public class LoggedInAuthViewModel : ViewModelBase
    {
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

        public LoggedInAuthViewModel()
        {
            RecentChanges = new ObservableCollection<RecentChangesInfo>();
            OnlineAlbums = new ObservableCollection<OnlineAlbumViewModel>();

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

            OnlineAlbums.Add(new OnlineAlbumViewModel()
            {
                Color = "Generic",
                LastEdit = "15.05.2022",
                Name = "Piwka",
                PhotoCount = "6",
                Tags = "#piwo#browar"
            });
            OnlineAlbums.Add(new OnlineAlbumViewModel()
            {
                Color = "Blue",
                LastEdit = "24.04.2022",
                Name = "Reszta",
                PhotoCount = "3",
                Tags = "#none"
            });
        }
    }
}
