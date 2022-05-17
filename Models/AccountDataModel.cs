namespace iPhoto.Models
{
    public class AccountDataModel
    {
        public string Account { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        private int _albumCount;

        public string AlbumCount
        {
            get
            {
                return _albumCount.ToString();
            }
            set
            {
                _albumCount = int.Parse(value);
            }
        }
        public string Email { get; set; }
        public string LastLoggedOn { get; set; }
    }
}
