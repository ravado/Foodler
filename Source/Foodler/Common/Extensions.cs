using System;
using System.IO;
using System.Windows.Media.Imaging;
using ImageExtensions = System.Windows.Media.Imaging.Extensions;
namespace Foodler.Common
{
    public static class Extensions
    {
        /// <summary>
        /// Convert byte array to image
        /// </summary>
        /// <param name="bytes">Image bytes</param>
        /// <returns>Converted image or null if unsuccessful</returns>
        public static BitmapImage ToImage(this byte[] bytes)
        {
            try
            {
                using (var stream = new MemoryStream(bytes))
                {
                    var image = new BitmapImage();
                    image.SetSource(stream);
                    return image;
                }
            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// Convert any stream to bytes
        /// </summary>
        /// <param name="input">Stream to convert</param>
        /// <returns>Bytes array from stream</returns>
        public static byte[] ToBytes(this Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static byte[] ToBytes(this BitmapImage bitmapImage)
        {
            using (var ms = new MemoryStream())
            {
                
                var btmMap = new WriteableBitmap
                    (bitmapImage.DecodePixelWidth, bitmapImage.DecodePixelHeight);

                // write an image into the stream
                btmMap.SaveJpeg(ms, 64, 64, 0, 100);

                return ms.ToArray();
            }
        }

        /// <summary>
        /// Make an image object from uri path to image
        /// </summary>
        /// <param name="url">Path to image</param>
        /// <returns>Image object</returns>
        public static BitmapImage GetImage(this string url)
        {
            return new BitmapImage(new Uri(url, UriKind.Relative));
        }
    }
}
