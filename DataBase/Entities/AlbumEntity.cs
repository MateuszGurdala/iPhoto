using System.Collections.Generic;

namespace iPhoto.DataBase;

public class AlbumEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int PhotoCount { get; set; }
    public string Tags { get; set; }
    public string CreationDate { get; set; }
    public bool IsLocal { get; set; }
    public string ColorGroup { get; set; } 

    public ImageEntity? ImageEntity { get; set; }
}