using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace iPhoto.ViewModels.AlbumsPage
{
    public class AlbumSearchViewModel : ViewModelBase
    {
        private string nameText;
        public string? NameText 
        {   
            get
            {
                return nameText;
            }
            set
            {
                if (!string.Equals(nameText, value))
                {
                    nameText = value;
                    OnPropertyChanged(nameof(NameText));
                }
            }
        }

        public string? TagsText { get; set; }
        public string? ColorText { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

/*        public string SelectedColor { 
            get
            {
                if (SelectedColor == null)
                {
                    return "Any";
                }
                return SelectedColor.ToString();
            }
            set
            {
                SelectedColor = value.ToString();
            }
        }*/
        public AlbumSearchViewModel()
        {
            nameText = "*Name";
            TagsText = "*All";
            StartDate = null;
            EndDate = null;
            ColorText = "Green";
            //SelectedColor = "Any";
        }
    }
}
