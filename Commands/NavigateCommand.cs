using iPhoto.ViewModels;

namespace iPhoto.Commands
{
    public class NavigateCommand : CommandBase
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        public NavigateCommand(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }
        public override void Execute(object parameter)
        {
            var direction = parameter as string;

            switch (direction)
            {
                case "Home":
                    {
                        _mainWindowViewModel.MainViewModel = _mainWindowViewModel.HomePageViewModel;
                        break;
                    }
                case "Search":
                    {
                        _mainWindowViewModel.MainViewModel = _mainWindowViewModel.SearchViewModel;
                        break;
                    }
                case "Albums":
                    {
                        _mainWindowViewModel.MainViewModel = _mainWindowViewModel.AlbumsViewModel;
                        break;
                    }
                case "Account":
                    {
                        _mainWindowViewModel.MainViewModel = _mainWindowViewModel.AccountViewModel;
                        break;
                    }
                case "Settings":
                    {
                        _mainWindowViewModel.MainViewModel = _mainWindowViewModel.SettingsViewModel;
                        break;
                    }
            }
        }
    }
}
