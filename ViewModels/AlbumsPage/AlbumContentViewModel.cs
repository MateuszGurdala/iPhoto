using iPhoto.Commands;
using iPhoto.Commands.AlbumPage;
using iPhoto.Commands.SearchPage;
using iPhoto.DataBase;
using iPhoto.UtilityClasses;
using iPhoto.ViewModels.SearchPage;
using iPhoto.Views.AlbumPage;
using iPhoto.Views.SearchPage;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace iPhoto.ViewModels.AlbumsPage
{
    public class AlbumContentViewModel : ViewModelBase
    {
        //TODO CAN BE MERGED WITH SEARCH VIEWMODEL KG
        //public ICommand SearchCommand { get; }
        public ICommand ExtendSearchMenuCommand { get; }
        public ICommand ExtendPhotoDetailsCommand { get; }
        public ICommand AddPhotoToAlbumCommand { get; }
        public ObservableCollection<PhotoSearchResultViewModel> PhotoSearchResultsCollection { get; }
        // KG 2.05 Methods for album content handling
        public Album CurrentAlbum { get; }
        public ICommand NavigateCommand { get; }

        public static string NavigateParam { get; } = "Albums";
        private readonly PhotoDetailsWindowView _photoDetailsWindow;
        public string AlbumText 
        {
            get
            {
                return $"CURRENT ALBUM: {CurrentAlbum.Name}";
            }
        }
        // 
        
        public ObservableCollection<string> AlbumList
        {
            get
            {
                return DatabaseHandler.GetAlbumList();
            }
        }
        public DatabaseHandler DatabaseHandler; //MG 15.04 added db handler class
        //public SearchEngine SearchEngine; //MG 27.04 Added
        public PhotoDetailsViewModel PhotoDetails { get; }  //MG 26.04 Added photo details

        public AlbumContentViewModel(DatabaseHandler database, PhotoDetailsWindowView photoDetailsWindow, MainWindowViewModel mainWindowVM, Album currentAlbum)
        {
            PhotoSearchResultsCollection = new ObservableCollection<PhotoSearchResultViewModel>();
            DatabaseHandler = database;
            ExtendSearchMenuCommand = new ExtendSearchMenuCommand();
            ExtendPhotoDetailsCommand = new ExtendPhotoDetailsCommand(photoDetailsWindow);
            AddPhotoToAlbumCommand = new AddPhotoToAlbumCommand(DatabaseHandler, currentAlbum, this );

            PhotoDetails = new PhotoDetailsViewModel(photoDetailsWindow, ExtendPhotoDetailsCommand as ExtendPhotoDetailsCommand);
            _photoDetailsWindow = photoDetailsWindow;
            photoDetailsWindow.DataContext = PhotoDetails;

           // SearchEngine = new SearchEngine(DatabaseHandler, this);
          //  SearchCommand = new SearchCommand(this, SearchEngine);
#pragma warning disable CS8601 // Possible null reference assignment.
            CurrentAlbum = currentAlbum;
#pragma warning restore CS8601 // Possible null reference assignment.
            NavigateCommand = new NavigateCommand(mainWindowVM);
            LoadAllAlbumPhotos();
        }
        /// <summary>
        ///  DELETE THIS AFTER IMPLEMENTING ALBUM SEARCH ENGINE <DUPLICATE>
        /// </summary>
        public async void LoadAllAlbumPhotos()
        {
            DatabaseHandler.LoadAllData();
            var currentAlbum = DatabaseHandler.Albums.FirstOrDefault(e => e.Id == CurrentAlbum.Id);
            foreach (Photo photo in currentAlbum.PhotoEntities)
            {
                PhotoSearchResultsCollection.Add(new PhotoSearchResultViewModel(photo,
                    DatabaseHandler.Images.FirstOrDefault(y => y.Id == photo.ImageId),
                    DatabaseHandler.Albums.FirstOrDefault(y => y.Id == CurrentAlbum.Id),
                    DatabaseHandler.Places.FirstOrDefault(y => y.Id == photo.PlaceId),
                    new SearchViewModel(DatabaseHandler, _photoDetailsWindow)
                    ));
                await Task.Delay(10);
            }
        }
    }
}
