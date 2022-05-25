using GMap.NET.WindowsPresentation;
using iPhoto.Commands.PlacesPage;
using iPhoto.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iPhoto.ViewModels.PlacesPage
{
    public class PlacesListElementsViewModel : ViewModelBase
    {

        public string PlaceNameText => "Name: " + _marker.Tag;
        public string PlacePhotoCount
        {
            get
            {
                return "Photos here: " + 0.ToString();
            }
        }
        public string LatitudeText => "Lat: " + _marker.Position.Lat.ToString();

        public string LongtitudeText => "Lng: " + _marker.Position.Lng.ToString();

        private bool _isClicked;
        public bool IsClicked
        {
            get { return _isClicked; }
            set
            {
                _isClicked = value;
                OnPropertyChanged(nameof(IsClicked));
            }
        }

        public ICommand DeletePlaceCommand { get; }
        public ICommand FindMarkerOnMapCommand { get; }
        public GMapMarker Marker => _marker;
        private readonly GMapMarker _marker;

        private readonly PlacesViewModel _placesViewModel;
        private readonly DatabaseHandler _databaseHandler;

        public PlacesListElementsViewModel(GMapMarker marker, PlacesViewModel placesViewModel, DatabaseHandler databaseHandler)
        {
            _placesViewModel = placesViewModel;
            _databaseHandler = databaseHandler;
            _marker = marker;
            DeletePlaceCommand = new DeletePlaceCommand(_placesViewModel, _databaseHandler, this);
            FindMarkerOnMapCommand = new FindMarkerOnMapCommand(_placesViewModel, this);
        }

    }
}
