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
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split('\\');
            int lenght = 0;

            for (int i = 0; i < path.Length; i++)
            {
                lenght += 1;
                if (path[i] == "iPhoto")
                {
                    break;
                }
            }

            return String.Join("\\", path, 0, lenght);
        }
        public static string GetTestImagesDirectory()
        {
            return GetProjectDirectoryPath() + "\\Data\\TestImages";
        }
        public static string GetDatabaseDirectory()
        {
            return GetProjectDirectoryPath() + "\\DataBase";
        }
        public static BitmapImage LoadBitmapImage(string path, double decodePixelWidth)
        {
            BitmapImage image = new BitmapImage();

            image.BeginInit();
            image.UriSource = new Uri(path);
            image.DecodePixelWidth = (int)decodePixelWidth;
            image.EndInit();

            image.Freeze();

            return image;
        }
    }
}
