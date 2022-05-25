using GMap.NET;
using GMap.NET.WindowsPresentation;
using iPhoto.DataBase;
using iPhoto.ViewModels;
using iPhoto.ViewModels.PlacesPage;
using iPhoto.Views.PlacesPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace iPhoto.Commands.PlacesPage
{
    public class AddMapMarkerCommand : CommandBase
    {
        private readonly PlacesViewModel _placesViewModel;

        private readonly DatabaseHandler _databaseHandler;

        public AddMapMarkerCommand(PlacesViewModel placesViewModel, DatabaseHandler databaseHandler)
        {
            _placesViewModel = placesViewModel;
            _databaseHandler = databaseHandler;
        }

        public override void Execute(object parameter)
        {
            AddMarkerView view = (AddMarkerView)parameter;
            if (double.TryParse(view.Latitude.Text, out _) && double.TryParse(view.Longtitude.Text, out _))
            {
                double lat = double.Parse(view.Latitude.Text);
                double lon = double.Parse(view.Longtitude.Text);
                string color = ((Rectangle)view.AlbumColorsComboBox.SelectedValue).Name;
                string name = view.PlaceName.Text;
                SolidColorBrush markerColor = (SolidColorBrush)new BrushConverter().ConvertFromString(color);
                PointLatLng point = new PointLatLng(lat, lon);
                GMapMarker markerToAdd = new GMapMarker(point);
                markerToAdd.Shape = new Ellipse
                {
                    Width = 10,
                    Height = 10,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1.5,
                    Fill = markerColor
                };
                markerToAdd.Tag = name;
                markerToAdd.Offset = new Point(-5, -5);
                markerToAdd.ZIndex = int.MaxValue;
                _placesViewModel.MainMap.Markers.Add(markerToAdd);
                _databaseHandler.AddPlace(name, lat, lon, color);
                _placesViewModel.PlacesListViewModel.AddPlaceToList(markerToAdd);
            }
        }

    }
}
