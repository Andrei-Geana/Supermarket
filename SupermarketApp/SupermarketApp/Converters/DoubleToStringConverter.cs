using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace SupermarketApp.Converters
{
    public class DoubleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            double doubleValue;
            if (double.TryParse(value.ToString(), out doubleValue))
            {
                return doubleValue.ToString(culture);
            }
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(value.ToString()))
                return 0;

            double doubleValue;
            if((value as string).EndsWith("."))
            {
                value = (value as string).Append('0');
            }
            if (double.TryParse(value.ToString(), NumberStyles.Any, culture, out doubleValue))
            {
                return doubleValue;
            }
            return DependencyProperty.UnsetValue;
        }
    }
}