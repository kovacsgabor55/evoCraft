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
    public class AllowBuildImageToImageSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((AllowBuildImage)value)
            {
                case AllowBuildImage.Allow:
                    return Application.Current.FindResource("AllowFieldBitmapImage") as BitmapImage;
                case AllowBuildImage.Block:
                    return Application.Current.FindResource("BlockFieldBitmapImage") as BitmapImage;

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
