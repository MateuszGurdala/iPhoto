using System;
using System.Windows.Automation;
using System.Windows.Media.Imaging;
using iPhoto.UtilityClasses;

namespace iPhoto.ViewModels.AccountPage
{
    public class OnlineAlbumViewModel : ViewModelBase
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private int _photoCount;
        public string PhotoCount
        {
            get
            {
                return _photoCount.ToString();
            }
            set
            {
                _photoCount = int.Parse(value);
                OnPropertyChanged(nameof(PhotoCount));
            }
        }
        private string _lastEdit;
        public string LastEdit
        {
            get
            {
                return _lastEdit;
            }
            set
            {
                _lastEdit = value;
                OnPropertyChanged(nameof(LastEdit));
            }
        }
        private string _tags;
        public string Tags
        {
            get
            {
                return String.Join(" #",_tags.Split("#"));
            }
            set 
            {
                _tags = value;
                OnPropertyChanged(nameof(Tags));
            }
        }
        private string _colorAlbum;

        public string Color
        {
            set
            {
                _colorAlbum = value;
                OnPropertyChanged(nameof(ColorAlbum));
            }
        }
        public BitmapImage ColorAlbum
        {
            get
            {
                var path = DataHandler.GetAlbumIconsDirectoryPath() + _colorAlbum + "Album.png";
                return DataHandler.LoadBitmapImage(path, 100);
            }
        }
    }
}
