using iPhoto.DataBase;
using iPhoto.UtilityClasses;
using iPhoto.ViewModels.SearchPage;

using System.Collections.ObjectModel;
using System.Windows.Input;

namespace iPhoto.ViewModels
{
    public interface IPhotoSearchVM
    {
        public ObservableCollection<PhotoSearchResultViewModel> PhotoSearchResultsCollection { get; set; }
        public DatabaseHandler DatabaseHandler { get; }
        public PhotoDetailsViewModel PhotoDetails { get; }
        public ICommand ClearSearchParamsCommand { get; }
        public ObservableCollection<string> AlbumList { get; }
        public ICommand SearchCommand { get; }
        public SearchEngine SearchEngine { get; }

    }
}
