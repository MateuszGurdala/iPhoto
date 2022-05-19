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

        public BitmapImage? AlbumCover
        {
            get
            {
                if (GetAlbumCover() != null)
                {
                    return DataHandler.LoadBitmapImage(GetAlbumCover(), 200);
                }
                return null;
            }
        }
        public AlbumSearchResultModel AlbumData { get; }
        public BitmapImage AlbumIcon => DataHandler.LoadBitmapImage(GetAlbumIcon(), 100);
        public bool IsClicked { get; set;}
        private string GetAlbumIcon()
        {
            if (AlbumData.ColorGroup == "" || AlbumData.ColorGroup == "None")
            {
                return DataHandler.GetAlbumIconsDirectoryPath() + "GenericAlbum.png";
            }
            else
            {
                return DataHandler.GetAlbumIconsDirectoryPath() + AlbumData.ColorGroup + "Album.png";
            }
        }
        private string GetAlbumCover()
        {
            if (AlbumData.CoverPhoto != null)
            {
                return DataHandler.GetDatabaseImageDirectory() + "\\" + AlbumData.CoverPhoto.Source;
            }
            else
            {
                return null;
            }
        }

        public string AlbumNameText
        {
            get
            {
                string header = "Album Name: ";
                if (AlbumData.Name.Length > 19)
                {
                    return string.Concat(header, AlbumData.Name.AsSpan(0, 16), "...");
                }
                else
                {
                    return string.Concat(header, AlbumData.Name);
                }
            }
        }

        public string PhotoCountText
        {
            get
            {
                string header = "Photo count: ";
                string photoCount = AlbumData.PhotoCount.ToString();
                return header + photoCount;
            }
        
        }
        public string TagsText
        {
            get
            {
                string output = "Tags: ";
                foreach (string tag in AlbumData.Tags)
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
                return header + AlbumData.CreationDate.ToShortDateString();
            }
        }
        public string ColorGroupText
        {
            get
            {
                string header = "Current color group: ";
                return header + AlbumData.ColorGroup;
            }
        }
        public string MemorySizeText
        {
            get
            {
                string header = "Total size: ";
                string sizeUnit = "MB";
                return string.Concat(header,AlbumData.TotalMemorySize.ToString(), sizeUnit);
            }
        }
        public int AlbumId
        {
            get => AlbumData.Id;
        }

        public ICommand ShowAlbumContentCommand { get; }
        public ICommand EditAlbumCommand { get; }
        public ICommand DeleteAlbumCommand { get; }
        public AlbumSearchResultViewModel(DatabaseHandler database, PhotoDetailsWindowView photoDetailsWindow, Album album, List<PhotoEntity> photoEntities, MainWindowViewModel mainWindowViewModel, AlbumViewModel albumViewModel)
        {
            AlbumData = new AlbumSearchResultModel(album, photoEntities);
            IsClicked = false;
            ShowAlbumContentCommand = new ShowAlbumContentCommand(database, photoDetailsWindow, mainWindowViewModel, album, albumViewModel);
            EditAlbumCommand = new EditAlbumCommand(albumViewModel, album);
            DeleteAlbumCommand = new DeleteAlbumCommand(albumViewModel, album);
        }
        
    }
}
