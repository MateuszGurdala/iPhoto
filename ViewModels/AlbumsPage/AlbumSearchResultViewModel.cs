using iPhoto.DataBase;
using iPhoto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using iPhoto.UtilityClasses;
using iPhoto.Commands.AlbumPage;
using iPhoto.Commands;
using iPhoto.Views.SearchPage;

namespace iPhoto.ViewModels.AlbumsPage
{
    public class AlbumSearchResultViewModel : ViewModelBase
    {
        // ALBUM PROPERTY ! TODO
        /*        public void AddAlbumCover(PhotoEntity coverPhoto)
                {
                    if (!PhotoEntities.Contains(coverPhoto))
                    {
                        throw new InvalidDataException("Given cover photo does not exists in given album")
                    }
                }*/
        private readonly AlbumSearchResultModel _albumData;
        public BitmapImage AlbumIcon => DataHandler.LoadBitmapImage(GetAlbumIcon(), 100);
        public bool IsClicked { get; set;}
        private string GetAlbumIcon()
        {
            if (_albumData.ColorGroup == "" || _albumData.ColorGroup == "None")
            {
                return DataHandler.GetAlbumIconsDirectoryPath() + "GenericAlbum.png";
            }
            else
            {
                return DataHandler.GetAlbumIconsDirectoryPath() + _albumData.ColorGroup + "Album.png";
            }
        }
        public string AlbumNameText
        {
            get
            {
                string header = "Album Name: ";
                if (_albumData.Name.Length > 19)
                {
                    return string.Concat(header, _albumData.Name.AsSpan(0, 16), "...");
                }
                else
                {
                    return string.Concat(header, _albumData.Name);
                }
            }
        }

        public string PhotoCountText
        {
            get
            {
                string header = "Photo count: ";
                string photoCount = _albumData.PhotoCount.ToString();
                return header + photoCount;
            }
        
        }
        public string TagsText
        {
            get
            {
                string output = "Tags: ";
                foreach (string tag in _albumData.Tags)
                {
                    output += tag;
                    output += ", ";
                }
                output = output.Trim(' ');
                output = output.Trim(',');
                return output;
            }
        }
                
        public string CreationDateText
        { 
            get
            {
                string header = "Creation date: ";
                return header + _albumData.CreationDate.ToShortDateString();
            }
        }
        public string ColorGroupText
        {
            get
            {
                string header = "Current color group: ";
                return header + _albumData.ColorGroup;
            }
        }
        public string MemorySizeText
        {
            get
            {
                string header = "Total memory size: ";
                string sizeUnit = "MB";
                return string.Concat(header,_albumData.TotalMemorySize.ToString(), sizeUnit);
            }
        }
        public ICommand ShowAlbumDetailsCommand { get; }
        public ICommand ShowAlbumContentCommand { get; }
        public ICommand ShowAlbumOptionsCommand { get; }
        public AlbumSearchResultViewModel(DatabaseHandler database, PhotoDetailsWindowView photoDetailsWindow, Album album, List<PhotoEntity> photoEntities, MainWindowViewModel mainWindowViewModel)
        {
            _albumData = new AlbumSearchResultModel(album, photoEntities);
            IsClicked = false;
            ShowAlbumContentCommand = new ShowAlbumContentCommand(database, photoDetailsWindow, mainWindowViewModel, album);
        }
        
    }
}
