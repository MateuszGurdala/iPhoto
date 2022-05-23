using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace iPhoto.DataBase
{
    public class Image
    {
        private readonly ImageEntity _imageEntity;
        public int Id
        {
            get => _imageEntity.Id;
            set => _imageEntity.Id = value;
        }
        public string Source
        {
            get => _imageEntity.Source;
            set => _imageEntity.Source = value;
        }
        public int ResolutionWidth
        {
            get => _imageEntity.ResolutionWidth;
            set => _imageEntity.ResolutionWidth = value;
        }
        public int ResolutionHeight
        {
            get => _imageEntity.ResolutionHeight;
            set => _imageEntity.ResolutionHeight = value;
        }
        public double Size
        {
            get => _imageEntity.Size;
            set => _imageEntity.Size = value;
        }

        public List<byte> RemoteImageData { get; set; } = new List<byte>();

        public Image(ImageEntity imageEntity)
        {
            _imageEntity = imageEntity;
        }
        public Image(int id, string source, int width, int height, double size)
        {
            _imageEntity = new ImageEntity();

            if (width < 0)
            {
                throw new InvalidDataException("Image width cannot be negative.");
            }
            if (height < 0)
            {
                throw new InvalidDataException("Image height cannot be negative.");
            }
            if (size < 0)
            {
                throw new InvalidDataException("Image size cannot be negative.");
            }

            Id = id;
            Source = source;
            ResolutionHeight = height;
            ResolutionWidth = width;
            Size = size;
        }

        public ImageEntity GetEntity()
        {
            return _imageEntity;
        }
        public override string ToString()
        {
            return $"{Id} | {Source} | {ResolutionWidth} | {ResolutionHeight} | {Size}";
        }
    }
}
