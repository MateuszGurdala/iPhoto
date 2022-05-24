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
    public class AddMapMarkerCommand : CommandBase
    {
        private readonly AddMarkerViewModel _addMarkerViewModel;

        public AddMapMarkerCommand(AddMarkerViewModel addMarkerViewModel)
        {
            _addMarkerViewModel = addMarkerViewModel;
        }

        public override void Execute(object parameter)
        {

        }

    }
}
