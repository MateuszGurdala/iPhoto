using System;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace iPhoto.UtilityClasses
{
    public static class DataHandler
    {
        public static string GetSideMenuIconsDirectoryPath()
        {
            return GetProjectDirectoryPath() + "\\Graphics\\Icons\\";
        }
        private static string GetProjectDirectoryPath()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!.Split('\\');
            int length = 0;

            for (int i = 0; i < path.Length; i++)
            {
                length += 1;
                if (path[i] == "iPhoto")
                {
                    break;
                }
            }

            return String.Join("\\", path, 0, length);
        }
        public static string GetTestImagesDirectory()
        {
            return GetProjectDirectoryPath() + "\\Data\\TestImages";
        }
        public static string GetDatabaseDirectory()
        {
            return GetProjectDirectoryPath() + "\\DataBase";
        }
        public static string GetDatabaseImageDirectory()
        {
            return GetProjectDirectoryPath() + "\\DataBase\\Images";
        }
        public static BitmapImage LoadBitmapImage(string path, double? decodePixelWidth)
        {
            BitmapImage image = new BitmapImage();

            image.BeginInit();
            image.UriSource = new Uri(path);
            image.CacheOption = BitmapCacheOption.OnLoad;
            if (decodePixelWidth != null)
            {
                image.DecodePixelWidth = (int)decodePixelWidth;
            }
            image.EndInit();

            image.Freeze();

            return image;
        }
    }
}
