using System.Windows.Controls.Primitives;
using iPhoto.ViewModels.AlbumsPage;

namespace iPhoto.Views.AlbumPage
{
    public partial class AddPhotoToAlbumPopupView : Popup
    {
        public AddPhotoToAlbumPopupView(AddPhotoToAlbumPopupViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            dataContext.ParentView = this;
        }
    }
}
