using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPhoto.ViewModels.SearchPage
{
    public class PhotoDetailsViewModel : ViewModelBase
    {
        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        private string _album;
        public string Album
        {
            get
            {
                return _album;
            }
            set
            {
                _album = value;
                OnPropertyChanged(nameof(Album));
            }
        }
        private string _tags;
        public string Tags
        {
            get
            {
                return _tags;
            }
            set
            {
                _tags = value;
                OnPropertyChanged(nameof(Tags));
            }
        }
        private DateTime _date;
        public string CreationDate
        {
            get
            {
                return _date.ToShortDateString();
            }
            set
            {
                _date = DateTime.Parse(value);
                OnPropertyChanged(nameof(CreationDate));
            }
        }
        private string _place;
        public string PlaceTaken
        {
            get
            {
                return _place;
            }
            set
            {
                _place = value;
                OnPropertyChanged(nameof(PlaceTaken));
            }
        }
        private string _source;
        public string Source
        {
            get
            {
                return _source;
            }
            set
            {
                _source = value;
                OnPropertyChanged(nameof(Source));
            }
        }
        private int _resolutionWidth;
        public int ResolutionWidth
        {
            get
            {
                return _resolutionWidth;
            }
            set
            {
                _resolutionWidth = value;
                OnPropertyChanged(nameof(ResolutionWidth));
                OnPropertyChanged(nameof(Resolution));
            }
        }
        private int _resolutionHeight;
        public int ResolutionHeight
        {
            get
            {
                return _resolutionHeight;
            }
            set
            {
                _resolutionHeight = value;
                OnPropertyChanged(nameof(ResolutionHeight));
                OnPropertyChanged(nameof(Resolution));
            }
        }
        public string Resolution
        {
            get
            {
                return ResolutionWidth.ToString() + "x" + ResolutionHeight.ToString();
            }
        }
        private float _memorySize;
        public string MemorySize
        {
            get
            {
                return _memorySize.ToString();
            }
            set
            {
                _memorySize = float.Parse(value);
                OnPropertyChanged(nameof(MemorySize));
            }
        }
    }
}
