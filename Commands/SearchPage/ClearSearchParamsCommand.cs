using iPhoto.Views;
using System;

namespace iPhoto.Commands.SearchPage
{
    public class ClearSearchParamsCommand : CommandBase
    {
        private readonly bool _isInsideAlbum;

        public ClearSearchParamsCommand(bool isInsideAlbum)
        {
            _isInsideAlbum = isInsideAlbum;
        }
        public override void Execute(object parameter)
        {
            var view = parameter as PhotoSearchOptionsView;
            view.Title.ContentTextBox.Clear();
            if (!_isInsideAlbum)
            {
                view.Album.SelectedValue = "*";
            }
            view.Tags.ContentTextBox.Clear();
            view.StartDate.Text = "";
            view.EndDate.Text = "";
            view.Location.ContentTextBox.Clear();

            view.Title.TextBox_LostFocus(view.Title.ContentTextBox, new System.Windows.RoutedEventArgs());
            view.Tags.TextBox_LostFocus(view.Tags.ContentTextBox, new System.Windows.RoutedEventArgs());
            view.Location.TextBox_LostFocus(view.Location.ContentTextBox, new System.Windows.RoutedEventArgs());
        }
    }
}
