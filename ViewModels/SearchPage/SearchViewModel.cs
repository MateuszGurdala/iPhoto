using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using iPhoto.Commands;
using iPhoto.Commands.SearchPage;
using iPhoto.DataBase;
using iPhoto.UtilityClasses;
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
        public ObservableCollection<PhotoSearchResultViewModel> PhotoSearchResultsCollection { get; }
        public DatabaseHandler DatabaseHandler; //MG 15.04 added db handler class
        public SearchEngine SearchEngine; //MG 27.04 Added
        public PhotoDetailsViewModel PhotoDetails { get; }  //MG 26.04 Added photo details

        public SearchViewModel(DatabaseHandler database, PhotoDetailsWindowView photoDetailsWindow)
        {
            PhotoSearchResultsCollection = new ObservableCollection<PhotoSearchResultViewModel>();
            DatabaseHandler = database;


            ExtendSearchMenuCommand = new ExtendSearchMenuCommand();
            ExtendPhotoDetailsCommand = new ExtendPhotoDetailsCommand(photoDetailsWindow);
            AddPhotoCommand = new AddPhotoCommand(DatabaseHandler);

            PhotoDetails = new PhotoDetailsViewModel(photoDetailsWindow, ExtendPhotoDetailsCommand as ExtendPhotoDetailsCommand);
            photoDetailsWindow.DataContext = PhotoDetails;

            SearchEngine = new SearchEngine(DatabaseHandler, this);
            SearchCommand = new SearchCommand(this, SearchEngine);

            //MG 16.04
            //DELETE THIS - ONLY FOR TESTING PURPOSES
            var photo = DatabaseHandler.Photos.FirstOrDefault(e => e.Id == 1);
            var image = DatabaseHandler.Images.FirstOrDefault(e => e.Id == photo!.ImageId);
            var album = DatabaseHandler.Albums.FirstOrDefault(e => e.Id == photo!.AlbumId);
            var place = DatabaseHandler.Places.FirstOrDefault(e => e.Id == photo!.PlaceId);
            PhotoSearchResultsCollection.Add(new PhotoSearchResultViewModel(photo!, image!, album!, place!, this));
            //~MG 16.04
        }
    }
}
