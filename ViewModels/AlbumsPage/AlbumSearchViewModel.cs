using iPhoto.Commands.AlbumPage;
using System.Windows.Input;

namespace iPhoto.ViewModels.AlbumsPage
{
    public class AlbumSearchViewModel : ViewModelBase
    {
        public ICommand ClearAlbumSearchParamsCommand { get; }
        public AlbumSearchViewModel()
        {
            ClearAlbumSearchParamsCommand = new ClearAlbumSearchParamsCommand();
        }
    }
}
