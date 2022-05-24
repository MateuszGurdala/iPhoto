using iPhoto.Commands.PlacesPage;
using iPhoto.DataBase;
using iPhoto.ViewModels.PlacesPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iPhoto.ViewModels
{
    public class PlacesViewModel : ViewModelBase
    {
        public ICommand AddMapMarkerCommand { get; }

        public ICommand GetMapPositionCommand { get; }

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

            PlacesListViewModel = new PlacesListViewModel();
            AddMarkerViewModel = new AddMarkerViewModel(this);

            SidePlaceViewModel = AddMarkerViewModel;

            AddMapMarkerCommand = new AddMapMarkerCommand(AddMarkerViewModel);
            GetMapPositionCommand = new GetMapPositionCommand(AddMarkerViewModel);
        }
    }
}
