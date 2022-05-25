using GMap.NET.WindowsPresentation;
using iPhoto.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPhoto.ViewModels.PlacesPage
{
    public class PlacesListViewModel : ViewModelBase
    {
        private readonly PlacesViewModel _placesViewModel;
        private readonly DatabaseHandler _databaseHandler;

        public ObservableCollection<PlacesListElementsViewModel> MapMarkersCollection { get; }


        public PlacesListViewModel(PlacesViewModel placesViewModel, DatabaseHandler databaseHandler)
        {
            _placesViewModel = placesViewModel;
            _databaseHandler = databaseHandler;
            MapMarkersCollection = new ObservableCollection<PlacesListElementsViewModel>();
            DisplayAllPlaces();
        }

        public void DisplayAllPlaces()
        {
            foreach (GMapMarker marker in _placesViewModel.MainMap.Markers)
            {
                if (marker.Tag != null)
                {
                    MapMarkersCollection.Add(new PlacesListElementsViewModel(marker, _placesViewModel, _databaseHandler));
                }
              
            }
        }
        public void AddPlaceToList(GMapMarker marker)
        {
            MapMarkersCollection.Add(new PlacesListElementsViewModel(marker, _placesViewModel, _databaseHandler));
        }

        public void RemoveMarkerFromList(GMapMarker marker)
        {
            if (MapMarkersCollection.FirstOrDefault(e => e.Marker.Tag.Equals((string)marker.Tag)) != null)
                MapMarkersCollection.Remove(MapMarkersCollection.FirstOrDefault(e => e.Marker.Tag.Equals((string)marker.Tag)));
        }
    }
}
