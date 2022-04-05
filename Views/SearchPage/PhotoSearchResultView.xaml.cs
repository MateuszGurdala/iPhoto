using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace iPhoto.Views
{
    public partial class PhotoSearchResultView : UserControl
    {
        public BitmapImage ImagePreviewSource
        {
            get { return (BitmapImage)GetValue(ImagePreviewSourceProperty); }
            set { SetValue(ImagePreviewSourceProperty, value); }
        }
        public static readonly DependencyProperty ImagePreviewSourceProperty =
            DependencyProperty.Register("ImagePreviewSource", typeof(BitmapImage), typeof(PhotoSearchResultView));

        public string PhotoTitle
        {
            get { return (string)GetValue(PhotoTitleProperty); }
            set { SetValue(PhotoTitleProperty, value); }
        }
        public static readonly DependencyProperty PhotoTitleProperty =
            DependencyProperty.Register("PhotoTitle", typeof(string), typeof(PhotoSearchResultView));
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(PhotoSearchResultView));

        public object Parameter
        {
            get { return (object)GetValue(ParameterProperty); }
            set { SetValue(ParameterProperty, value); }
        }
        public static readonly DependencyProperty ParameterProperty =
            DependencyProperty.Register("Parameter", typeof(object), typeof(PhotoSearchResultView));
        public PhotoSearchResultView()
        {
            InitializeComponent();
        }
    }
}
