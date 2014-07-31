using System;
using System.Windows;
using System.Windows.Data;

namespace Foodler.Common.Converters
{
    public class InVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var converted = value as bool?;
            if (value != null && converted != null && converted.Value)
                return Visibility.Collapsed;

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var converted = value as Visibility?;
            if (value != null && converted != null && converted == Visibility.Visible)
                return false;

            return true;
        }
    }
}
