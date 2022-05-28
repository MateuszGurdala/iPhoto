using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace iPhoto.Views
{
    /// <summary>
    /// Interaction logic for PlacesView.xaml
    /// </summary>
    public partial class PlacesView : UserControl
    {
        private readonly GMapMarker mainMarker;



        public ICommand GetMapPositionCommand
        {
            get { return (ICommand)GetValue(GetMapPositionCommandProperty); }
            set { SetValue(GetMapPositionCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GetMapPositionCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GetMapPositionCommandProperty =
            DependencyProperty.Register("GetMapPositionCommand", typeof(ICommand), typeof(PlacesView), new PropertyMetadata(null));



        public ICommand AddMapMarkerCommand
        {
            get { return (ICommand)GetValue(AddMapMarkerCommandProperty); }
            set { SetValue(AddMapMarkerCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AddMapMarkerCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddMapMarkerCommandProperty =
            DependencyProperty.Register("AddMapMarkerCommand", typeof(ICommand), typeof(PlacesView), new PropertyMetadata(null));



        public PlacesView()
        {
            InitializeComponent();
            


/*            PointLatLng point = new PointLatLng(52.2297, 21.0122);
            GMapMarker test_marker = new GMapMarker(point);
            test_marker.Shape = new Ellipse
            {
                Width = 10,
                Height = 10,
                Stroke = Brushes.Black,
                StrokeThickness = 1.5,
                Fill = Brushes.Green,
                Name = "MyMarker"
            };
            test_marker.Offset = new Point(-5, -5);
            test_marker.ZIndex = int.MaxValue;
            MainMap.Markers.Add(test_marker);*/

        }

        private void MainMap_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(Map);
            if (GetMapPositionCommand != null)
            {
                GetMapPositionCommand.Execute(position);
            }
            /*
                        var markerToRemove = MainMap.Markers.SingleOrDefault(e => ((FrameworkElement)e.Shape).Name == "MyMarker");
                        if (markerToRemove != null)
                        {
                            MainMap.Markers.Remove(markerToRemove);
                        }*/
        }
    }
}
