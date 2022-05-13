using iPhoto.Views.AlbumPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace iPhoto.Models
{
    public class AlbumSearchParams
    {
        public string? Name { get; set; }
        public string[]? Tags { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Color { get; set; }


        public AlbumSearchParams(AlbumSearchView searchView)
        {
                Name = searchView.Name.ContentTextBox.Text;
                if (searchView.Tags.ContentTextBox.Text != "*")
                {
                    Tags = ParseTags(searchView.Tags.ContentTextBox.Text);
                }
                else
                {
                    Tags = null;
                }
                if (searchView.StartDate.Text != "")
                {
                    StartDate = ConvertToDatetime(searchView.StartDate.Text);
                }
                if (searchView.StartDate.Text != "")
                {
                    EndDate = ConvertToDatetime(searchView.EndDate.Text);
                }
                Color = ((Rectangle)searchView.ColorsComboBox.SelectedItem).Name;

                CheckForNull();
        }

        private string[] ParseTags(string tagsLine)
        {
            Tags = tagsLine.Split(',');
            Array.ForEach(Tags, e => e.Trim());
            return Tags;
        }
        /// <summary>
        /// Converts <paramref name="date"/> to proper date used by program
        /// </summary>
        /// <param name="date"></param>
        /// <returns>
        /// <paramref name="date"/> in date format 
        /// </returns>
        private DateTime ConvertToDatetime(string date)
        {
            return DateTime.ParseExact(date, "dd/mm/yyyy", null);
        }

        /// <summary>
        /// Checks if elements are empty search values
        /// </summary>
        private void CheckForNull()
        {
            Name = (Name[0] == '*') ? null : Name;
            Color = (Color == "") ? null : Color;
        }
    }
}
