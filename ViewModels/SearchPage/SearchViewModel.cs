using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using iPhoto.Commands;
using iPhoto.Commands.SearchPage;
using iPhoto.DataBase;

namespace iPhoto.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        public ICommand SearchCommand { get; }
        public ICommand ExtendSearchMenuCommand { get; }
        public ICommand AddPhotoCommand { get; }
        public ObservableCollection<PhotoSearchResultViewModel> PhotoSearchResultsCollection { get; }
        //MG 15.04 added db handler class
        public DatabaseHandler DatabaseHandler;

        public SearchViewModel(DatabaseHandler database)
        {
            PhotoSearchResultsCollection = new ObservableCollection<PhotoSearchResultViewModel>();
            DatabaseHandler = database;

            SearchCommand = new SearchCommand(this);
            ExtendSearchMenuCommand = new ExtendSearchMenuCommand();
            AddPhotoCommand = new AddPhotoCommand(DatabaseHandler);

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
