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
            albumSearchView.Name.ContentTextBox.Clear();
            albumSearchView.Tags.ContentTextBox.Clear();
            albumSearchView.StartDate.Text = "";
            albumSearchView.EndDate.Text = "";
            albumSearchView.ColorsComboBox.SelectedIndex = 0;
            albumSearchView.Name.TextBox_LostFocus(albumSearchView.Name.ContentTextBox, new System.Windows.RoutedEventArgs());
            albumSearchView.Tags.TextBox_LostFocus(albumSearchView.Tags.ContentTextBox, new System.Windows.RoutedEventArgs());
        }
    }
}
