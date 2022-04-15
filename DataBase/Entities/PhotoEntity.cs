namespace iPhoto.DataBase;

public class PhotoEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int AlbumEntityId { get; set; }
    public AlbumEntity AlbumEntity { get; set; }
    public string Tags { get; set; }
    public string DateTaken { get; set; }
    public int PlaceEntityId { get; set; }
    public PlaceEntity PlaceEntity { get; set; }
    public int ImageEntityId { get; set; }
    public ImageEntity ImageEntity { get; set; }
}