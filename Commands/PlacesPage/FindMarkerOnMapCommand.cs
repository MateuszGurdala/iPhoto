using GMap.NET.WindowsPresentation;
using iPhoto.ViewModels;
using iPhoto.Views.PlacesPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPhoto.Commands.PlacesPage
{
    public class FindMarkerOnMapCommand : CommandBase
    {
        private readonly PlacesViewModel _placesViewModel;
        public FindMarkerOnMapCommand(PlacesViewModel placesViewModel)
        {
            _placesViewModel = placesViewModel;
        }
        public override void Execute(object parameter)
        {
            AddMarkerView view = (AddMarkerView)parameter;
            string name = view.PlaceName.Text;
            GMapMarker markerToChange = _placesViewModel.MainMap.Markers.SingleOrDefault(e => (string)e.Tag == name);
            if (markerToChange == null)
                return;
             var Position = markerToChange.Position;
            _placesViewModel.MainMap.Zoom = 12;
            _placesViewModel.MainMap.Position = Position;
        }
    }
}
