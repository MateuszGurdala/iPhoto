using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using iPhoto.Commands;
using iPhoto.Commands.SearchPage;
using iPhoto.DataBase;
using iPhoto.UtilityClasses;
using iPhoto.ViewModels.AlbumsPage;
using iPhoto.ViewModels.SearchPage;
using iPhoto.Views.SearchPage;

namespace iPhoto.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        public ICommand SearchCommand { get; }
        public ICommand ExtendSearchMenuCommand { get; }
        public ICommand ExtendPhotoDetailsCommand { get; }
        public ICommand AddPhotoCommand { get; }
        public ObservableCollection<PhotoSearchResultViewModel> PhotoSearchResultsCollection { get; set; }
        public ObservableCollection<string> AlbumList
        {
            get
            {
                return DatabaseHandler.GetAlbumList();
            }
        }
        public DatabaseHandler DatabaseHandler; //MG 15.04 added db handler class
        public SearchEngine SearchEngine; //MG 27.04 Added
        public PhotoDetailsViewModel PhotoDetails { get; }  //MG 26.04 Added photo details

        public SearchViewModel(DatabaseHandler database, PhotoDetailsWindowView photoDetailsWindow)
        {
            PhotoSearchResultsCollection = new ObservableCollection<PhotoSearchResultViewModel>() { };
            DatabaseHandler = database;
            ExtendSearchMenuCommand = new ExtendSearchMenuCommand();
            ExtendPhotoDetailsCommand = new ExtendPhotoDetailsCommand(photoDetailsWindow);
            AddPhotoCommand = new AddPhotoCommand(DatabaseHandler);

            PhotoDetails = new PhotoDetailsViewModel(photoDetailsWindow, ExtendPhotoDetailsCommand as ExtendPhotoDetailsCommand);
            photoDetailsWindow.DataContext = PhotoDetails;

            SearchEngine = new SearchEngine(DatabaseHandler, this);
            SearchCommand = new SearchCommand(this, SearchEngine);
        }
    }
}
