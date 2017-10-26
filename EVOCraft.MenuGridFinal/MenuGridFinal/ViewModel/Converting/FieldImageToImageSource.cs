using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace View
{
    /// <summary>
    /// This helper class converts the SelectionImage enum to the right image resource.
    /// </summary>
    public class FieldImageToImageSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((FieldImage)value)
            {
                case FieldImage.Grass1:
                    return Application.Current.FindResource("Grass1BitmapImage") as BitmapImage;
                case FieldImage.Grass2:
                    return Application.Current.FindResource("Grass2BitmapImage") as BitmapImage;
                case FieldImage.Grass3:
                    return Application.Current.FindResource("Grass3BitmapImage") as BitmapImage;
                case FieldImage.Grass4:
                    return Application.Current.FindResource("Grass4BitmapImage") as BitmapImage;
                case FieldImage.Grass5:
                    return Application.Current.FindResource("Grass5BitmapImage") as BitmapImage;

                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}