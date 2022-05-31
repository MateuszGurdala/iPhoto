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


        /// <summary>
        /// Add photo inside existing album
        /// </summary>
        /// <param name="databaseHandler"> database handler to database where new photos are added</param>
        /// <param name="currentAlbum"> target album where photos will be added </param>
        /// <param name="albumVm"> viewmodel of album where photos are added </param>
        public AddPhotoToAlbumCommand(DatabaseHandler databaseHandler, Album currentAlbum, AlbumContentViewModel albumVm)
        {
            _databaseHandler = databaseHandler;
            _currentAlbum = currentAlbum;
            _albumVm = albumVm;
        }


        /// <summary>
        /// execute command and add photos to album
        /// </summary>
        /// <param name="parameter"> not used </param>
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
        /// <summary>
        /// handles system file explorer where photos are chosen.  
        /// </summary>
        /// <param name="fileDialog"> file Dialog handler</param>
        private void ConfigureDialog(OpenFileDialog fileDialog)
        {
            fileDialog.Filter = "Image files (*.png;)|*.png;";
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            fileDialog.Multiselect = true;
        }
    }
}
