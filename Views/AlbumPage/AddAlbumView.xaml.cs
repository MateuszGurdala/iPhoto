﻿using iPhoto.ViewModels.AlbumsPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iPhoto.Views.AlbumPage
{
    /// <summary>
    /// Interaction logic for AddAlbumView.xaml
    /// </summary>
    public partial class AddAlbumView : UserControl
    {
        public AddAlbumView()
        {
            InitializeComponent();
        }
        public void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus;
        }
    }
}
