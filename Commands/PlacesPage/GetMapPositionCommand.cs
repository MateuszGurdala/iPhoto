using GMap.NET;
using iPhoto.ViewModels;
using iPhoto.ViewModels.PlacesPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;



namespace iPhoto.Commands.PlacesPage
{
    public class GetMapPositionCommand : CommandBase
    {
        private readonly AddMarkerViewModel _addMarkerViewModel;

        private readonly PlacesViewModel _placesViewModel;

        public GetMapPositionCommand(AddMarkerViewModel addMarkerViewModel, PlacesViewModel placesViewModel)
        {
            _addMarkerViewModel = addMarkerViewModel;
            _placesViewModel = placesViewModel;

        }
        public override void Execute(object parameter)
        {
            Point clickedPoint = (Point)parameter;
            var coordinates = _placesViewModel.MainMap.FromLocalToLatLng((int)clickedPoint.X, (int)clickedPoint.Y);
            double Latitude = coordinates.Lat;
            double Longtitude = coordinates.Lng;
            _addMarkerViewModel.LatitudeText = Math.Round(Latitude, 5).ToString();
            _addMarkerViewModel.LongtitudeText = Math.Round(Longtitude, 5).ToString();
            _placesViewModel.PreviewMarker.Position = coordinates;

        }
    }
}
