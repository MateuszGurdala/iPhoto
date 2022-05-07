using System;
using System.Windows;
using System.Windows.Controls;

namespace iPhoto.Views.UserControls
{
    public partial class TextBoxWithTitle : UserControl
    {
        public string EntryText
        {
            get { return (string)GetValue(EntryTextProperty); }
            set { SetValue(EntryTextProperty, value); }
        }
        public static readonly DependencyProperty EntryTextProperty =
            DependencyProperty.Register("EntryText", typeof(string), typeof(TextBoxWithTitle), new PropertyMetadata(string.Empty));
        public int TextWidth
        {
            get { return (int)GetValue(TextWidthProperty); }
            set { SetValue(TextWidthProperty, value); }
        }
        public static readonly DependencyProperty TextWidthProperty =
            DependencyProperty.Register("TextWidth", typeof(int), typeof(TextBoxWithTitle), new PropertyMetadata(0));

        public TextBoxWithTitle()
        {
            InitializeComponent();
        }

        public void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == String.Empty)
            {
                textBox.Opacity = 0.5;
                textBox.Text = EntryText;
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == EntryText)
            {
                textBox.Opacity = 1;
                textBox.Text = String.Empty;
            }
        }
    }
}
