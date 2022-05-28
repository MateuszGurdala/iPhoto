using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPhoto.DataBase
{
    public class Place
    {
        private readonly PlaceEntity _placeEntity;
        public int Id
        {
            get => _placeEntity.Id;
            set => _placeEntity.Id = value;
        }
        public string Name
        {
            get => _placeEntity.Name;
            set => _placeEntity.Name = value;
        }
        public double? Latitude
        {
            get => _placeEntity.Latitude;
            set => _placeEntity.Latitude = value;
        }
        public double? Longitude
        {
            get => _placeEntity.Longitude;
            set => _placeEntity.Longitude = value;
        }

        public string? MapMarkerColor
        {
            get => _placeEntity.MapMarkerColor;
            set => _placeEntity.MapMarkerColor = value;
        }


        public Place(PlaceEntity placeEntity)
        {
            _placeEntity = placeEntity;
        }
        public Place(int id, string name, double? lat, double? lon, string? markerColor)
        {
            _placeEntity = new PlaceEntity();
            Id = id;
            Name = name;
            Latitude = lat;
            Longitude = lon;
            MapMarkerColor = markerColor;
        }

        public PlaceEntity GetEntity()
        {
            return _placeEntity;
        }
        public override string ToString()
        {
            return $"{Id} | {Name} | {Latitude.ToString() ?? "null"} | {Latitude.ToString() ?? "null"}";
        }
    }
}
