using iPhoto.ViewModels;
using iPhoto.ViewModels.PlacesPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPhoto.Commands.PlacesPage
{
    public class SwitchPlacesPanelCommand : CommandBase
    {

        private readonly PlacesViewModel _placesViewModel;

        public SwitchPlacesPanelCommand(PlacesViewModel placesViewModel)
        {
            _placesViewModel = placesViewModel;
        }

        public override void Execute(object parameter)
        {
            if(_placesViewModel.SidePlaceViewModel.GetType() == typeof(AddMarkerViewModel))
            {
                _placesViewModel.SidePlaceViewModel = _placesViewModel.PlacesListViewModel;
            }
            else
            {
                _placesViewModel.SidePlaceViewModel = _placesViewModel.AddMarkerViewModel;
            }
        }
    }
}
