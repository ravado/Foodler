using System;
using System.Diagnostics;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Foodler.Common.Converters
{
    public class BitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if (value is byte[])
                {
                    if ((value as byte[]).Length > 0)
                    {
                        var image = (value as byte[]).ToImage();
                        return image;
                    }
                }
                var img = new BitmapImage(new Uri("/Assets/Icon/mask-icon-white.png", UriKind.Relative));
                return img;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("BitmapImageConvertor: {0}",ex.Message);
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("Now back convertation is not allowed");
        }
    }
}
