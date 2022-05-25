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
        public IPhotoSearchVM SearchViewModel { get; }
        private readonly PhotoSearchResultModel _photoData;

        public BitmapImage ImagePreviewSource
        {
            get
            {
                return _photoData.PreviewImage;
            }
            set
            {
                _photoData.PreviewImage = value;
                OnPropertyChanged(nameof(ImagePreviewSource));
            }
        }
            
        public BitmapImage Image => _photoData.GetImage();
        //MG 26.04 Fixed title display settings
        public string PhotoTitle
        {
            get
            {
                if (_photoData.Title.Length > 19)
                {
                    return _photoData.Title.Substring(0, 16) + "...";
                }
                else
                {
                    return _photoData.Title;
                }
            }
        }
        public DatabaseHandler Database => SearchViewModel.DatabaseHandler;
        public ObservableCollection<PhotoSearchResultViewModel> PhotoSearchResultsCollection =>
            SearchViewModel.PhotoSearchResultsCollection;
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
        public PhotoSearchResultViewModel(Photo photoData, Image imageData, Album albumData, Place placeData, IPhotoSearchVM searchViewModel)
        {
            SearchViewModel = searchViewModel;
            _photoData = new PhotoSearchResultModel(photoData, imageData, albumData, placeData);

            PreviewPhotoCommand = new PreviewPhotoCommand();
            ClickSearchResultCommand = new ClickSearchResultCommand(SearchViewModel.PhotoSearchResultsCollection, searchViewModel.PhotoDetails);
            ClickSearchResultOptionsCommand = new ClickSearchResultOptionsCommand();

            IsClicked = false;
        }

        public int GetPhotoId()
        {
            return _photoData.PhotoData.Id;
        }
        public int GetImageId()
        {
            return _photoData.ImageData.Id;
        }
        public int GetAlbumId()
        {
            return _photoData.AlbumData.Id;
        }
        public string GetImageSource()
        {
            return _photoData.ImageData.Source;
        }
        public PhotoSearchResultModel GetPhotoData()
        {
            return _photoData;
        }
        public void LoadPreviewImage()
        {
            ImagePreviewSource = _photoData.PreviewImage;
        }
    }
}
