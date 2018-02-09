using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;


namespace UserControlUnits.Converters
{

    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string s = value as string;
            var source = new System.Windows.Media.Imaging.BitmapImage();
            source.BeginInit();
            source.UriSource = new Uri(s, UriKind.RelativeOrAbsolute);
            source.EndInit();
            return source;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
