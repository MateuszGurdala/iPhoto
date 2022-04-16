using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using iPhoto.Commands;
using iPhoto.DataBase;
using iPhoto.Models;

namespace iPhoto.ViewModels
{
    public class PhotoSearchResultViewModel : ViewModelBase
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

        //MG 16.04 implemented handling database data format 
        public PhotoSearchResultViewModel(Photo photoData, Image imageData, Album albumData, Place placeData, ObservableCollection<PhotoSearchResultViewModel> searchResults)
        {
            PhotoData = new PhotoSearchResultModel(photoData, imageData, albumData, placeData);

            PreviewPhotoCommand = new PreviewPhotoCommand();
            ClickSearchResultCommand = new ClickSearchResultCommand(searchResults);
            ClickSearchResultOptionsCommand = new ClickSearchResultOptionsCommand();

            ImagePreviewSource = PhotoData.PreviewImage;
            PhotoTitle = PhotoData.Title;
            IsClicked = false;
        }
    }
}
