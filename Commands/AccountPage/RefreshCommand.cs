using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iPhoto.ViewModels.AccountPage;

namespace iPhoto.Commands.AccountPage
{
    public class RefreshCommand: CommandBase
    {
        private LoggedInAuthViewModel _inAuthViewModel;
        public RefreshCommand(LoggedInAuthViewModel viewModel)
        {
            _inAuthViewModel = viewModel;
        }
        public override void Execute(object parameter)
        {
            _inAuthViewModel.LoadAlbums();
        }
    }
}
