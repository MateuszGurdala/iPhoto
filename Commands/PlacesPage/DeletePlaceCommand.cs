using iPhoto.DataBase;
using iPhoto.ViewModels;
using iPhoto.ViewModels.PlacesPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPhoto.Commands.PlacesPage
{
    public class DeletePlaceCommand : CommandBase
    {
        private readonly PlacesViewModel _placesViewModel;
        private readonly DatabaseHandler _databaseHandler;
        private readonly PlacesListElementsViewModel _placesListElementsViewModel;

        public DeletePlaceCommand(PlacesViewModel placesViewModel, DatabaseHandler databaseHandler, PlacesListElementsViewModel placesListElementsViewModel)
        {
            _placesViewModel = placesViewModel;
            _databaseHandler = databaseHandler;
            _placesListElementsViewModel = placesListElementsViewModel;
        }

        public override void Execute(object parameter)
        {
            foreach (Photo photo in _databaseHandler.Photos)
            {
                if (photo.PlaceId == _databaseHandler.Places.FirstOrDefault(e => e.Name == (string)_placesListElementsViewModel.Marker.Tag).Id)
                {
                    _databaseHandler.UpdatePhoto(photo.Id, null, null, null, null, "NoPlace");
                }
            }
            _placesViewModel.PlacesListViewModel.RemoveMarkerFromList(_placesListElementsViewModel.Marker);
            _placesViewModel.MainMap.Markers.Remove(_placesListElementsViewModel.Marker);
            _databaseHandler.RemovePlace(_databaseHandler.Places.FirstOrDefault(e => e.Name == (string)_placesListElementsViewModel.Marker.Tag).Id);
        }
    }
}
