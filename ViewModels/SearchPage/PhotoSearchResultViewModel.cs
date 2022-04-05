using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using iPhoto.Commands;
using iPhoto.Models;

namespace iPhoto.ViewModels
{
    public class PhotoSearchResultViewModel: ViewModelBase
    {
        public readonly PhotoSearchResultModel PhotoData;
        public BitmapImage ImagePreviewSource { get; }
        public string PhotoTitle { get; }
        public ICommand ClickSearchResultCommand { get; }
        public ICommand ClickSearchResultOptionsCommand { get; }
        public ICommand PreviewPhotoCommand { get; }

        private bool _isClicked;
        public bool IsClicked
        {
            get { return _isClicked; }
            set
            {
                _isClicked = value;
                OnPropertyChanged(nameof(IsClicked));
            }
        }

        public PhotoSearchResultViewModel(string uri, ObservableCollection<PhotoSearchResultViewModel> searchResults)
        {
            PhotoData = new PhotoSearchResultModel(uri);

            ClickSearchResultCommand = new ClickSearchResultCommand(searchResults);
            ClickSearchResultOptionsCommand = new ClickSearchResultOptionsCommand();
            PreviewPhotoCommand = new PreviewPhotoCommand();

            ImagePreviewSource = PhotoData.PreviewImage;
            PhotoTitle = PhotoData.Title;
            IsClicked = false;

        }
    }
}
