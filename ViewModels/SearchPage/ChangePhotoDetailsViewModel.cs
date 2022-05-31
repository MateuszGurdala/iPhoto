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
        public readonly PhotoSearchResultViewModel SearchResultViewModel;

        public ICommand DiscardCommand { get; }
        public ICommand CommitCommand { get; }

        public AddPhotoPopupView ParentView { get; set; }
        public BitmapImage Image { get; set; }
        public ObservableCollection<string> AlbumList { get; set; }
        public PhotoAdder PhotoAdder { get; set; }
        public int PhotoId { get; set; }

        public ObservableCollection<string> PlacesList { get; set;}


        public ChangePhotoDetailsViewModel(PhotoSearchResultViewModel photoSearchResultViewModel)
        {
            SearchResultViewModel = photoSearchResultViewModel;

            DiscardCommand = new DiscardCommand();
            CommitCommand = new CommitCommand(true);

            Image = SearchResultViewModel.Image;
            AlbumList = SearchResultViewModel.Database.GetAlbumList(false);
            PlacesList = SearchResultViewModel.Database.GetPlacesList(false);
            PhotoId = SearchResultViewModel.GetPhotoId();
            PhotoAdder = new PhotoAdder(SearchResultViewModel.Database, null);
            PhotoAdder.Popup = ParentView;

        }
        public void UpdateViewData()
        {
            ParentView.Title.ContentTextBox.Text = SearchResultViewModel.GetPhotoData().Title;
            ParentView.Album.Text = SearchResultViewModel.GetPhotoData().AlbumData.Name;
            ParentView.RawTags.ContentTextBox.Text = SearchResultViewModel.GetPhotoData().PhotoData.RawTags;
            ParentView.CreationDateString.Text = SearchResultViewModel.GetPhotoData().PhotoData.DateTaken.ToString();
            ParentView.PlaceTaken.Text = SearchResultViewModel.GetPhotoData().PlaceData.Name;

            if (ParentView.Title.ContentTextBox.Text != ParentView.Title.EntryText)
            {
                ParentView.Title.ContentTextBox.Opacity = 1;
            }
            if (ParentView.RawTags.ContentTextBox.Text != ParentView.RawTags.EntryText)
            {
                ParentView.RawTags.ContentTextBox.Opacity = 1;
            }
/*            if (ParentView.PlaceTaken.ContentTextBox.Text != ParentView.PlaceTaken.EntryText)
            {
                ParentView.PlaceTaken.ContentTextBox.Opacity = 1;
            }*/
        }
    }
}
