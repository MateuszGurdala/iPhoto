using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System;
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
            MainMap.MapProvider = GMapProviders.GoogleMap;
            MainMap.Position = new PointLatLng(54.6961334816182, 25.2985095977783);
            MainMap.ShowCenter = false;

            mainMarker = new GMapMarker(MainMap.Position);
            {
                mainMarker.Shape = new Ellipse
                {
                    Width = 10,
                    Height = 10,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1.5
                };
                mainMarker.Offset = new Point(-5, -5);
                mainMarker.ZIndex = int.MaxValue;
                MainMap.Markers.Add(mainMarker);
            }
        }

        private void MainMap_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(MainMap);
            if (GetMapPositionCommand != null)
           {    
                GetMapPositionCommand.Execute(position);
           }
           mainMarker.Position = MainMap.FromLocalToLatLng((int)position.X, (int)position.Y);
        }
    }
}
