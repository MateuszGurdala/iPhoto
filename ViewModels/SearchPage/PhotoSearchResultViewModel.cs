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
        private readonly SearchViewModel _searchViewModel;
        private readonly PhotoSearchResultModel _photoData;

        public BitmapImage ImagePreviewSource => _photoData.PreviewImage;
        public BitmapImage Image => _photoData.GetImage();
        public string PhotoTitle => _photoData.Title;
        public DatabaseHandler Database => _searchViewModel.DatabaseHandler;

        public ObservableCollection<PhotoSearchResultViewModel> PhotoSearchResultsCollection =>
            _searchViewModel.PhotoSearchResultsCollection;

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
        public PhotoSearchResultViewModel(Photo photoData, Image imageData, Album albumData, Place placeData, SearchViewModel searchViewModel)
        {
            _searchViewModel = searchViewModel;
            _photoData = new PhotoSearchResultModel(photoData, imageData, albumData, placeData);

            PreviewPhotoCommand = new PreviewPhotoCommand();
            ClickSearchResultCommand = new ClickSearchResultCommand(_searchViewModel.PhotoSearchResultsCollection);
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

        public string GetImageSource()
        {
            return _photoData.ImageData.Source;
        }
    }
}
