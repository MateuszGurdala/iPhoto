using iPhoto.Views;

namespace iPhoto.Models
{
    public class SearchData
    {
        private string? _title;
        private string? _photoAlbum;
        private string? _tags;
        private string? _startDate;
        private string? _endDate;
        private string? _location;

        public string Title
        {
            get { return _title; }
        }

        public SearchData(PhotoSearchOptionsView searchOptions)
        {
            _title = searchOptions.Title.ContentTextBox.Text;
            _photoAlbum = searchOptions.Album.ContentTextBox.Text;
            _tags = searchOptions.Tags.ContentTextBox.Text;
            _startDate = searchOptions.StartDate.ContentTextBox.Text;
            _endDate = searchOptions.EndDate.ContentTextBox.Text;
            _location = searchOptions.Location.ContentTextBox.Text;

            CheckForNull();
        }
        private void CheckForNull()
        {
            _title = (_title[0] == '*') ? null : _title;
            _photoAlbum = (_photoAlbum[0] == '*') ? null : _photoAlbum;
            _tags = (_tags[0] == '*') ? null : _tags;
            _startDate = (_startDate[0] == '*') ? null : _startDate;
            _endDate = (_endDate[0] == '*') ? null : _endDate;
            _location = (_location[0] == '*') ? null : _location;
        }
    }
}
