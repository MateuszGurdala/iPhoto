using System.Windows;
using iPhoto.ViewModels;
using System.Windows.Controls;

namespace iPhoto.Views
{
    public partial class ChromeButtonsView : UserControl
    {
        public int[] ResizeDimensions
        {
            get { return (int[])GetValue(ResizeDimensionsProperty); }
            set { SetValue(ResizeDimensionsProperty, value); }
        }
        public static readonly DependencyProperty ResizeDimensionsProperty =
            DependencyProperty.Register("ResizeDimensions", typeof(int[]), typeof(ChromeButtonsView));

        public ChromeButtonsView()
        {
            InitializeComponent();
            DataContext = new UtilityButtonsViewModel();
        }
    }
}
