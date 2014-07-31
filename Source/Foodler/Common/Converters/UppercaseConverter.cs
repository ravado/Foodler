using System;
using System.Diagnostics;
using System.Windows.Data;

namespace Foodler.Common.Converters
{
    public class UppercaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                var str = value as string;
                if (String.IsNullOrEmpty(str))
                    return String.Empty;

                return str.ToUpper(culture);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UppercaseConverter: {0}", ex.Message);
                return String.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("Back convertation is not possible");
        }
    }
}
