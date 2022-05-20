using System.Collections.Generic;

namespace iPhoto.DataBase;

public class ImageEntity
{
    public int Id { get; set; }
    public string Source { get; set; }
    public int ResolutionWidth { get; set; }
    public int ResolutionHeight { get; set; }
    public double Size { get; set; }
    public PhotoEntity PhotoEntity { get; set; }
    
    public AlbumEntity AlbumEntity { get; set; }
}