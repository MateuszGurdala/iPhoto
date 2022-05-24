using GMap.NET;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace iPhoto.ViewModels.PlacesPage
{
    public class AddMarkerViewModel : ViewModelBase
    {

        private readonly PlacesViewModel _placesViewModel;
        public ObservableCollection<GMapMarker> Markers;


        private string latitudeText;
        public string LatitudeText
        {
            get => latitudeText;
            set
            {
                latitudeText = value;
                OnPropertyChanged(nameof(LatitudeText));
            }
        }

        private string longtitudeText;

        public string LongtitudeText
        {
            get => longtitudeText;
            set
            {
                longtitudeText = value;
                OnPropertyChanged(nameof(LongtitudeText));
            }
        }

        public AddMarkerViewModel(PlacesViewModel placesViewModel)
        {
            latitudeText = "LAT...";
            longtitudeText = "LONG...";
            _placesViewModel = placesViewModel;
        }
    }
}
