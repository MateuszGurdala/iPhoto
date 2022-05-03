using iPhoto.ViewModels.AlbumsPage;
using iPhoto.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPhoto.Commands.AlbumSearchResultOptions
{
    /// <summary>
    /// Command, on execute shows Popup which includes options for given album
    /// </summary>
    public class ShowAlbumOptionsCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var viewModel = parameter as AlbumSearchResultViewModel;
            
        }
    }
}
