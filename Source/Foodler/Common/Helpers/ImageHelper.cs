using System;
using System.Windows;

namespace Foodler.Common.Helpers
{
    public static class ImageHelper
    {
        public static byte[] LoadImageFromPath(string path)
        {
            var some = ReadFile(path);
            return some;
        }

        private static byte[] ReadFile(string relativePathToFile)
        {
            byte[] fileBytes = null;
            var xml = Application.GetResourceStream(new Uri(relativePathToFile, UriKind.RelativeOrAbsolute));

            if (xml != null)
            {
                fileBytes = xml.Stream.ToBytes();    
            }

            return fileBytes;
        }
    }
}
