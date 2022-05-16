using iPhoto.Commands.AccountPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using iPhoto.ViewModels.AccountPage;

namespace iPhoto.ViewModels
{
    public class AccountViewModel : ViewModelBase 
    {
        private ViewModelBase? _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }
        public AccountViewModel()
        {
            //CurrentViewModel = new LogInViewModel();
            CurrentViewModel = new LoggedInAuthViewModel();
        }
    }
}
