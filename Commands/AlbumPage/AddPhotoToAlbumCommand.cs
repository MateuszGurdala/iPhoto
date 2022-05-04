using System;
using iPhoto.DataBase;
using iPhoto.UtilityClasses;
using iPhoto.ViewModels.AlbumsPage;
using Microsoft.Win32;

namespace iPhoto.Commands.AlbumPage
{
    public class AddPhotoToAlbumCommand : CommandBase
    {
        private readonly DatabaseHandler _databaseHandler;
        private readonly Album _currentAlbum;
        private readonly AlbumContentViewModel _albumVm;
        public AddPhotoToAlbumCommand(DatabaseHandler databaseHandler, Album currentAlbum, AlbumContentViewModel albumVm)
        {
            _databaseHandler = databaseHandler;
            _currentAlbum = currentAlbum;
            _albumVm = albumVm;
        }

        public override void Execute(object parameter)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            ConfigureDialog(fileDialog);

            if (fileDialog.ShowDialog() != true)
            {
                return;
            }

            foreach (var fileName in fileDialog.FileNames)
            {
                var photoAdder = new PhotoAdder(_databaseHandler, fileName);
                photoAdder.GetPhotoData(_currentAlbum, _albumVm);
            }
        }
        private void ConfigureDialog(OpenFileDialog fileDialog)
        {
            fileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            fileDialog.Multiselect = true;
        }
    }
}
