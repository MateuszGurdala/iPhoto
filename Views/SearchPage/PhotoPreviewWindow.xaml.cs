using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using iPhoto.UtilityClasses;

namespace iPhoto.Views.UserControls
{
    public partial class PhotoPreviewWindow : Window
    {
        private Window _mainWindow;

        public PhotoPreviewWindow(Window mainWindow, BitmapImage bitmapImage)
        {
            InitializeComponent();

            _mainWindow = mainWindow;
            Image.Source = bitmapImage;
            SetPosition();

            SetSize();
            Show();
        }

        private void SetPosition()
        {
            this.Left = _mainWindow.Left;
            this.Top = _mainWindow.Top;
        }
        private void SetSize()
        {
            this.Width = _mainWindow.Width;
            this.Height = _mainWindow.Height;
        }
        private void ClosePreview(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }       
}
