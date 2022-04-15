using iPhoto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPhoto.UtilityClasses
{
    // Krystian 10.04.22
    public class NavigationStore
    {
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OncurrentViewModelChanged();
            }
        }
        public event Action CurrentViewModelChanged;
        private void OncurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
