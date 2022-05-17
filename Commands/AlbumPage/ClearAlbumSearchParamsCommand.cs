using iPhoto.Views.AlbumPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPhoto.Commands.AlbumPage
{
    public class ClearAlbumSearchParamsCommand : CommandBase
    {

        public override void Execute(object parameter)
        {
            AlbumSearchView albumSearchView = (AlbumSearchView)parameter;
            albumSearchView.Name.ContentTextBox.Text = "";
            albumSearchView.Tags.ContentTextBox.Text = "";
            albumSearchView.StartDate.Text = "";
            albumSearchView.EndDate.Text = "";
            albumSearchView.ColorsComboBox.SelectedIndex = 0;
        }
    }
}
