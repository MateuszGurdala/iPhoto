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
    /// Interaction logic for AlbumSearchResultView.xaml
    /// </summary>
    public partial class AlbumSearchResultView : UserControl
    {
/*        public string AlbumName
        {
            get { return (string)GetValue(AlbumNameProperty); }
            set { SetValue(AlbumNameProperty, value); }
        }
        public static readonly DependencyProperty AlbumNameProperty =
            DependencyProperty.Register("AlbumName", typeof(string), typeof(AlbumSearchResultView));*/

        /*        public string AlbumColor
                {
                    get { return (string)GetValue(AlbumColorProperty); }
                    set { SetValue(AlbumColorProperty, value); }
                }
                public static readonly DependencyProperty AlbumColorProperty =
*//*                    DependencyProperty.Register("AlbumColor", typeof(string), typeof(AlbumSearchResultView));*//*
        public BitmapImage AlbumIcon
        {
            get { return (BitmapImage)GetValue(AlbumIconProperty); }
            set { SetValue(AlbumIconProperty, value); }
        }
        public static readonly DependencyProperty AlbumIconProperty =
            DependencyProperty.Register("AlbumIcon", typeof(BitmapImage), typeof(PhotoSearchResultView));*/
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(AlbumSearchResultView));

        public object Parameter
        {
            get { return (object)GetValue(ParameterProperty); }
            set { SetValue(ParameterProperty, value); }
        }
        public static readonly DependencyProperty ParameterProperty =
            DependencyProperty.Register("Parameter", typeof(object), typeof(AlbumSearchResultView));


        public AlbumSearchResultView()
        {
            InitializeComponent();
        }
    }
}
