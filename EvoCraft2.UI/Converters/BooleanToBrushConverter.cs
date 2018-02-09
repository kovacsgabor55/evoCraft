using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace UserControlUnits.Converters
{
    class BooleanToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value)
                {
                    return Brushes.Red;
                }
                else
                {
                    return Brushes.Transparent;
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
