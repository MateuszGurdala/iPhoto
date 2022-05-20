﻿using iPhoto.DataBase;
using iPhoto.ViewModels;
using iPhoto.Views.AlbumPage;
using System.Linq;
using System.Windows.Shapes;

namespace iPhoto.Commands.AlbumPage
{
    internal class UpdateAlbumCommand : CommandBase
    {

        private readonly AlbumViewModel _albumViewModel;
        private readonly Album _album;

        public UpdateAlbumCommand(AlbumViewModel albumViewModel, Album album)
        {
            _albumViewModel = albumViewModel;
            _album = album;
        }

        public override void Execute(object parameter)
        {
            EditAlbumPopupView view = parameter as EditAlbumPopupView;

            string newColorGroup = ((Rectangle)view.ColorsComboBox.SelectedItem).Name;
            string newName = view.Name.ContentTextBox.Text;
            int? imageId;
            if (_albumViewModel.DatabaseHandler.Photos.FirstOrDefault(e => e.Title == (string)view.PhotosComboBox.SelectedItem) != null)
            {
                imageId = _albumViewModel.DatabaseHandler.Photos.FirstOrDefault(e => e.Title == (string)view.PhotosComboBox.SelectedItem).ImageId;
            }
            else
            {
                imageId = null;
            }
            _albumViewModel.DatabaseHandler.UpdateAlbum(_album.Id, newName, null, null, null, null, newColorGroup, imageId);
            _album.ColorGroup = newColorGroup;
            _album.Name = newName;
            _albumViewModel.ReloadAlbumView(_album);
            view.IsOpen = false;
        }
    }
}
