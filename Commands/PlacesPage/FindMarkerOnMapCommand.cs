using GMap.NET.WindowsPresentation;
using iPhoto.ViewModels;
using iPhoto.ViewModels.PlacesPage;
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
        private readonly PlacesListElementsViewModel _placesListElementsViewModel;
        public FindMarkerOnMapCommand(PlacesViewModel placesViewModel, PlacesListElementsViewModel placesListElementsViewModel =null)
        {
            _placesViewModel = placesViewModel;
            _placesListElementsViewModel = placesListElementsViewModel;
        }
        public override void Execute(object parameter)
        {
            string name;
            if (_placesListElementsViewModel == null)
            {
                AddMarkerView view = (AddMarkerView)parameter;
                name = view.PlaceName.Text;
            }
            else
            {
                name = (string)_placesListElementsViewModel.Marker.Tag;
            }
            GMapMarker markerToChange = _placesViewModel.MainMap.Markers.SingleOrDefault(e => (string)e.Tag == name);
            if (markerToChange == null)
                return;
             var Position = markerToChange.Position;
            _placesViewModel.MainMap.Zoom = 15;
            _placesViewModel.MainMap.Position = Position;
        }
    }
}
