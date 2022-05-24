using GMap.NET;
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
        public GetMapPositionCommand(AddMarkerViewModel addMarkerViewModel)
        {
            _addMarkerViewModel = addMarkerViewModel;

        }
        public override void Execute(object parameter)
        {
            double Latitude = ((PointLatLng)parameter).Lat;
            double Longtitude = ((PointLatLng)parameter).Lng;
            _addMarkerViewModel.LatitudeText = Math.Round(Latitude, 5).ToString();
            _addMarkerViewModel.LongtitudeText = Math.Round(Longtitude, 5).ToString();
        }
    }
}
