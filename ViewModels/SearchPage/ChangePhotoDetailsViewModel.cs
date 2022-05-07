using iPhoto.Commands.SearchPage;
using iPhoto.UtilityClasses;
using iPhoto.Views.SearchPage;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace iPhoto.ViewModels.SearchPage
{
    public class ChangePhotoDetailsViewModel : ViewModelBase
    {
        private readonly PhotoSearchResultViewModel _searchResultViewModel;

        public ICommand DiscardCommand { get; }
        public ICommand CommitCommand { get; }

        public AddPhotoPopupView ParentView { get; set; }
        public BitmapImage Image { get; set; }
        public ObservableCollection<string> AlbumList { get; set; }
        public PhotoAdder PhotoAdder { get; set; }
        public int PhotoId { get; set; }


        public ChangePhotoDetailsViewModel(PhotoSearchResultViewModel photoSearchResultViewModel)
        {
            _searchResultViewModel = photoSearchResultViewModel;

            DiscardCommand = new DiscardCommand();
            CommitCommand = new CommitCommand(true);

            Image = _searchResultViewModel.Image;
            AlbumList = _searchResultViewModel.Database.GetAlbumList(false);
            PhotoId = _searchResultViewModel.GetPhotoId();
            PhotoAdder = new PhotoAdder(_searchResultViewModel.Database, null);
            PhotoAdder.Popup = ParentView;

        }
        public void UpdateViewData()
        {
            ParentView.Title.ContentTextBox.Text = _searchResultViewModel.GetPhotoData().Title;
            ParentView.Album.Text = _searchResultViewModel.GetPhotoData().AlbumData.Name;
            ParentView.RawTags.ContentTextBox.Text = _searchResultViewModel.GetPhotoData().PhotoData.RawTags;
            ParentView.CreationDateString.Text = _searchResultViewModel.GetPhotoData().PhotoData.DateTaken.ToString();
            ParentView.PlaceTaken.ContentTextBox.Text = _searchResultViewModel.GetPhotoData().PlaceData.Name;

            if (ParentView.Title.ContentTextBox.Text != ParentView.Title.EntryText)
            {
                ParentView.Title.ContentTextBox.Opacity = 1;
            }
            if (ParentView.RawTags.ContentTextBox.Text != ParentView.RawTags.EntryText)
            {
                ParentView.RawTags.ContentTextBox.Opacity = 1;
            }
            if (ParentView.PlaceTaken.ContentTextBox.Text != ParentView.PlaceTaken.EntryText)
            {
                ParentView.PlaceTaken.ContentTextBox.Opacity = 1;
            }
        }
    }
}
