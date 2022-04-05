using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using iPhoto.UtilityClasses;

namespace iPhoto.Views.UserControls
{
    public partial class CustomButton : UserControl
    {
        public int CornerRadius
        {
            get { return (int)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(int), typeof(CustomButton));
        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(string), typeof(CustomButton));
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(CustomButton));
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(CustomButton));

        public BitmapImage ImageBitmap { get; }

        private double _squeezeRatio = 0.9;
        public CustomButton()
        {
            InitializeComponent();
        }

        private void ButtonDown(object sender, MouseEventArgs e)
        {
            var grid = (sender as CustomButton)?.Grid;

            if (grid != null)
            {
                grid.Width *= _squeezeRatio;
                grid.Height *= _squeezeRatio;
            }
        }

        private void ButtonUp(object sender, MouseEventArgs e)
        {
            var grid = (sender as CustomButton)?.Grid;

            if (grid != null)
            {
                grid.Width /= _squeezeRatio;
                grid.Height /= _squeezeRatio;
            }
        }

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            var border = (sender as CustomButton)?.Border;
            border.Background = (LinearGradientBrush)this.FindResource("ButtonHighlightGradient");
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            var border = (sender as CustomButton)?.Border;
            border.Background = (LinearGradientBrush)this.FindResource("DarkLightGreenGradient");

        }
    }
}
