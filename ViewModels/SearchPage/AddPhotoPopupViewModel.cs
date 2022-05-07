using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using iPhoto.Commands.SearchPage;
using iPhoto.UtilityClasses;
using iPhoto.Views.SearchPage;
#pragma warning disable CS8618

namespace iPhoto.ViewModels.SearchPage
{
    public class AddPhotoPopupViewModel: ViewModelBase
    {
        public ICommand DiscardCommand { get; }
        public ICommand CommitCommand { get; }

        public AddPhotoPopupView ParentView { get; set; }
        public BitmapImage Image { get; set; }
        public PhotoAdder 
            
            
            
            
            
            PhotoAdder { get; set; }
        public ObservableCollection<string> AlbumList { get; set; }

        public AddPhotoPopupViewModel()
        {
            DiscardCommand = new DiscardCommand();
            CommitCommand = new CommitCommand(false);
        }
    }
}
