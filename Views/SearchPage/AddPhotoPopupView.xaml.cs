using System.Windows.Controls.Primitives;
using iPhoto.ViewModels.SearchPage;

namespace iPhoto.Views.SearchPage
{
    public partial class AddPhotoPopupView : Popup
    {
        public AddPhotoPopupView(AddPhotoPopupViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            dataContext.ParentView = this;
        }
        public AddPhotoPopupView(ChangePhotoDetailsViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            dataContext.ParentView = this;
            dataContext.UpdateViewData();
        }
    }
}
