using System.Collections.ObjectModel;
using iPhoto.ViewModels;

namespace iPhoto.Commands
{
    public class ClickSearchResultCommand : CommandBase
    {
        private readonly ObservableCollection<PhotoSearchResultViewModel> _searchResults;

        public ClickSearchResultCommand(ObservableCollection<PhotoSearchResultViewModel> searchResults)
        {
            _searchResults = searchResults;
        }
        public override void Execute(object? parameter)
        {
            foreach (var photoSearchResultViewModel in _searchResults)
            {
                photoSearchResultViewModel.IsClicked = false;
            }

            ((parameter as PhotoSearchResultViewModel)!).IsClicked = true;
        }
    }
}
