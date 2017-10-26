using EvoCraft.Core;
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
    public class GameStateToImageSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((GameState)value)
            {
                case GameState.Victory:
                    return Application.Current.FindResource("VictoryBitmapImage") as BitmapImage;
                case GameState.Defeat:
                    return Application.Current.FindResource("DefeatBitmapImage") as BitmapImage;

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
