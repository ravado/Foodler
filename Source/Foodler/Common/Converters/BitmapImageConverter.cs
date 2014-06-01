using System;
using System.Windows.Data;

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
                    return (value as byte[]).ToImage();
                }

                return null;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("Now back convertation is not allowed");
        }
    }
}
