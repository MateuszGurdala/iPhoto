using System;
using iPhoto.DataBase;
using iPhoto.Views;

namespace iPhoto.Models
{
    public class SearchParams
    {
        private string? _title;
        private string? _photoAlbum;
        private string? _tags;
        private string? _startDate;
        private string? _endDate;
        private string? _location;

        public SearchParams(PhotoSearchOptionsView searchOptions)
        {
            _title = searchOptions.Title.ContentTextBox.Text;
            _photoAlbum = searchOptions.Album.Text;
            _tags = searchOptions.Tags.ContentTextBox.Text;
            //_startDate = searchOptions.StartDate.ContentTextBox.Text;
            _startDate = searchOptions.StartDate.Text;
            //_endDate = searchOptions.EndDate.ContentTextBox.Text;
            _endDate = searchOptions.EndDate.Text;
            _location = searchOptions.Location.ContentTextBox.Text;

            CheckForNull();
        }
        public SearchParams(Album album)
        {
            BuildDefaultSearchParamsForAlbum(album.Name);
        }

        public SearchParams()
        {

        }

        public void BuildDefaultSearchParamsForAlbum(string albumName)
        {
            _title = null;
            _photoAlbum = albumName;
            _tags = null;
            _startDate = null;
            _endDate = null;
            _location = null;
        }

        private void CheckForNull()
        {
            _title = (_title[0] == '*') ? null : _title;
            _photoAlbum = (_photoAlbum[0] == '*') ? null : _photoAlbum;
            _tags = (_tags[0] == '*') ? null : _tags;
            _startDate = (_startDate == "") ? null : _startDate;
            _endDate = (_endDate == "") ? null : _endDate;
            _location = (_location[0] == '*') ? null : _location;
        }
        public string? GetTitleParam()
        {
            return _title;
        }
        public string? GetTagsParam()
        {
            return _tags;
        }
        public string? GetStartDateParam()
        {
            return _startDate;
        }
        public string? GetAlbumParam()
        {
            return _photoAlbum;
        }
        public string? GetEndDateParam()
        {
            return _endDate;
        }
        public string? GetLocationParam()
        {
            return _location;
        }
        public bool IsNull()
        {
            if (DateFormatValid() == false || TagsFormatValid() == false)
            {
                return true;
            }
            var parameters = new string?[] {_title, _photoAlbum, _tags, _startDate, _endDate, _location};
            foreach (var param in parameters)
            {
                if (param is not null)
                {
                    return false;
                }
            }
            return true;
        }
        private bool DateFormatValid()
        {
            if (_startDate != null)
            {
                try
                {
                    DateTime.Parse(_startDate);
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            if (_endDate != null)
            {
                try
                {
                    DateTime.Parse(_endDate);
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return true;
        }
        private bool TagsFormatValid()
        {
            if (_tags != null && _tags[0] != '#')
            {
                return false;
            }

            return true;
        }
    }
}
