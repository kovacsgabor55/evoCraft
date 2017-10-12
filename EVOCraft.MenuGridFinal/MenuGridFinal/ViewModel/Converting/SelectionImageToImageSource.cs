using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace MenuGridFinal
{
    /// <summary>
    /// This helper class converts the SelectionImage enum to the right image resource.
    /// </summary>
    public class SelectionImageToImageSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((SelectionImage)value)
            {
                case SelectionImage.Selected:
                    return Application.Current.FindResource("SelectionBitmapImage") as BitmapImage;
                case SelectionImage.MoveTarget:
                    return Application.Current.FindResource("TargetBitmapImage") as BitmapImage;
                case SelectionImage.SpawnFlag:
                    return Application.Current.FindResource("FlagBitmapImage") as BitmapImage;

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
