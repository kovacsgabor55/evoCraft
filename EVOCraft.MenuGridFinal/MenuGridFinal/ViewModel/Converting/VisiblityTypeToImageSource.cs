using EvoCraft.Core;
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
    public class VisiblityTypeToImageSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((VisibilityType)value)
            {
                case VisibilityType.Unexplored:
                    return Application.Current.FindResource("BlackSquareBitmapImage") as BitmapImage;
                case VisibilityType.Explored:
                    return Application.Current.FindResource("BlackSquareTransparentBitmapImage") as BitmapImage;
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