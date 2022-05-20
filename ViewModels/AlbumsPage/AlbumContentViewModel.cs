using iPhoto.Commands;
using iPhoto.Commands.AlbumPage;
using iPhoto.Commands.SearchPage;
using iPhoto.DataBase;
using iPhoto.Models;
using iPhoto.UtilityClasses;
using iPhoto.ViewModels.SearchPage;
using iPhoto.Views.AlbumPage;
using iPhoto.Views.SearchPage;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using iPhoto.RemoteDatabase;

namespace iPhoto.ViewModels.AlbumsPage
{
    public class AlbumContentViewModel : ViewModelBase, IPhotoSearchVM
    {
        public ICommand ExtendSearchMenuCommand { get; }
        public ICommand ExtendPhotoDetailsCommand { get; }
        public ICommand AddPhotoToAlbumCommand { get; }
        public ObservableCollection<PhotoSearchResultViewModel> PhotoSearchResultsCollection { get; set; }
        // KG 12.05 Methods for album content handling
        public Album CurrentAlbum { get; }

        public ICommand ClearSearchParamsCommand { get; }
        public ObservableCollection<string> AlbumList // returns current album
        {
            get
            {
                string[] currentAlbum = { CurrentAlbum.Name };
                return new ObservableCollection<string>(currentAlbum);
            }
        }
        public ICommand SearchCommand { get; }
        //

        public ICommand NavigateBackToAlbumsCommand { get; }

        public static string NavigateParam { get; } = "Albums";
        private readonly PhotoDetailsWindowView _photoDetailsWindow;
        public string AlbumText 
        {
            get
            {
                return $"CURRENT ALBUM: {CurrentAlbum.Name}";
            }
        }

        public DatabaseHandler DatabaseHandler { get;}
        public RemoteDatabaseHandler RemoteDatabaseHandler { get;}
        public PhotoDetailsViewModel PhotoDetails { get; }
        public SearchEngine SearchEngine { get; }

        public AlbumContentViewModel(DatabaseHandler database,RemoteDatabaseHandler remoteDatabase, PhotoDetailsWindowView photoDetailsWindow, MainWindowViewModel mainWindowVM, Album currentAlbum, AlbumViewModel albumViewModel)
        {
            PhotoSearchResultsCollection = new ObservableCollection<PhotoSearchResultViewModel>();
            DatabaseHandler = database;
            RemoteDatabaseHandler = remoteDatabase;
            PhotoDetails = new PhotoDetailsViewModel(photoDetailsWindow, ExtendPhotoDetailsCommand as ExtendPhotoDetailsCommand);
            _photoDetailsWindow = photoDetailsWindow;
            photoDetailsWindow.DataContext = PhotoDetails;
            SearchEngine = new SearchEngine(DatabaseHandler, remoteDatabase , this);      
            CurrentAlbum = currentAlbum;


                // COMMANDS
                SearchCommand = new SearchCommand(SearchEngine, this);
            ExtendSearchMenuCommand = new ExtendSearchMenuCommand();
            ExtendPhotoDetailsCommand = new ExtendPhotoDetailsCommand(photoDetailsWindow);
            AddPhotoToAlbumCommand = new AddPhotoToAlbumCommand(DatabaseHandler, currentAlbum, this);
            NavigateBackToAlbumsCommand = new NavigateBackToAlbumsCommand(mainWindowVM, albumViewModel, currentAlbum);
            ClearSearchParamsCommand = new ClearSearchParamsCommand(true);


            // inital photos load
            SearchEngine.LoadParams(new SearchParams(CurrentAlbum));
            SearchEngine.GetSearchResults(true);
            //LoadAllAlbumPhotos();

        }
        /// <summary>
        ///  Diplay in GUI all photos that are in given album. 
        /// </summary>
        public async void LoadAllAlbumPhotos()
        { 
            foreach (Photo photo in CurrentAlbum.PhotoEntities)
            {
                if (PhotoSearchResultsCollection.All(y => y.GetImageId() != photo.ImageId))
                {
                    PhotoSearchResultsCollection.Add(new PhotoSearchResultViewModel(photo,
                        DatabaseHandler.Images.FirstOrDefault(y => y.Id == photo.ImageId),
                        DatabaseHandler.Albums.FirstOrDefault(y => y.Id == CurrentAlbum.Id),
                        DatabaseHandler.Places.FirstOrDefault(y => y.Id == photo.PlaceId),
                        new SearchViewModel(DatabaseHandler, RemoteDatabaseHandler, _photoDetailsWindow)
                        ));
                }
                await Task.Delay(10);
            }
            foreach (var VM in PhotoSearchResultsCollection)
            {
                VM.SearchViewModel.PhotoSearchResultsCollection = PhotoSearchResultsCollection;
            }
        }
    }
}
