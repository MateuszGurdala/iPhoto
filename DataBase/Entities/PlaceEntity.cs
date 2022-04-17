using System.Collections.Generic;

namespace iPhoto.DataBase;

public class PlaceEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public List<PhotoEntity> PhotoEntities { get; set; }
}