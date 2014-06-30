using System;
using System.Windows.Data;

namespace Foodler.Common.Converters
{
    public class CostWithCurrencyConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is decimal)
            {
                var dec = (decimal) value;

                return String.Format("{0} {1}", dec, culture.NumberFormat.CurrencySymbol);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
