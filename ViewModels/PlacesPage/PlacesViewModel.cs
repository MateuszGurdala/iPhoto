using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using iPhoto.Commands.PlacesPage;
using iPhoto.DataBase;
using iPhoto.ViewModels.PlacesPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace iPhoto.ViewModels
{
    public class PlacesViewModel : ViewModelBase
    {
        private readonly DatabaseHandler _databaseHandler;
        public GMapControl MainMap { get; }
        public GMapMarker PreviewMarker { get; }


        public ICommand GetMapPositionCommand { get; }

        public ICommand SwitchPlacesPanelCommand { get; }

        public PlacesListViewModel PlacesListViewModel { get; }

        public AddMarkerViewModel AddMarkerViewModel { get; }

        private ViewModelBase? _sidePlaceViewModel;
        public ViewModelBase SidePlaceViewModel
        {
            get
            {
                return _sidePlaceViewModel;
            }
            set
            {
                _sidePlaceViewModel = value;
                OnPropertyChanged(nameof(SidePlaceViewModel));
            }
        }

        public PlacesViewModel(DatabaseHandler databaseHandler)
        {
            _databaseHandler = databaseHandler;
            MainMap = new GMapControl
            {
                MapProvider = GMapProviders.GoogleMap,
                Position = new PointLatLng(52.2297, 21.0122),
                ShowCenter = false,
                Zoom = 9,
                MaxZoom = 22,
                MinZoom = 3
            };
            // Marker for previewing location
            GMapMarker mainMarker = new GMapMarker(MainMap.Position);
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
            PreviewMarker = mainMarker;
            addMarkersToMap();

            PlacesListViewModel = new PlacesListViewModel(this, databaseHandler);
            AddMarkerViewModel = new AddMarkerViewModel(this, databaseHandler);

            SidePlaceViewModel = AddMarkerViewModel;

            GetMapPositionCommand = new GetMapPositionCommand(AddMarkerViewModel, this);
            SwitchPlacesPanelCommand = new SwitchPlacesPanelCommand(this);

        }

        private void addMarkersToMap()
        {
            foreach(var place in _databaseHandler.Places)
            {
                if (place.Latitude == null && place.Longitude == null)
                    continue;
#pragma warning disable CS8629
                GMapMarker marker = new GMapMarker(new PointLatLng((double)place.Latitude, (double)place.Longitude));
                SolidColorBrush markerColor = (SolidColorBrush)new BrushConverter().ConvertFromString(place.MapMarkerColor);
#pragma warning restore CS8629
                {
                    marker.Shape = new Ellipse
                    {
                        Width = 10,
                        Height = 10,
                        Stroke = Brushes.Black,
                        Fill = markerColor,
                        StrokeThickness = 1.5
                    };
                    marker.Offset = new Point(-5, -5);
                    marker.ZIndex = int.MaxValue;
                    marker.Tag = place.Name;
                    MainMap.Markers.Add(marker);
                }
            }
        }
    }
}
