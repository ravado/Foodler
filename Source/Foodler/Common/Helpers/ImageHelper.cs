using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Windows.Storage;

namespace Foodler.Common.Helpers
{
    public static class ImageHelper
    {
        public static byte[] LoadImageFromPath(string path)
        {
            var some = ReadFile(path);
            return some.Result;
        }

        private static async Task<byte[]> ReadFile(string relativePathToFile)
        {
            // Get the local folder.
            var u = new Uri(relativePathToFile, UriKind.Relative);

            var local = ApplicationData.Current.LocalFolder;
            byte[] fileBytes = null;
            if (local != null)
            {
                using (var fs = File.OpenRead(local.Path + relativePathToFile))
                {
                    var bytes = new byte[fs.Length];
                    fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                    fileBytes = bytes;
                }
            }

            return fileBytes;
        }
    }
}
