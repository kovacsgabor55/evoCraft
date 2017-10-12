using EvoCraft.Common;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace MenuGridFinal
{
    /// <summary>
    /// This helper class converts the MapOjectImage enum to the right image resource.
    /// </summary>
    public class ActionTypeToImageSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((Actions)value)
            {
                case Actions.Move:
                    return Application.Current.FindResource("MoveBitmapImage") as BitmapImage;
                case Actions.Cancel:
                    return Application.Current.FindResource("CancelBitmapImage") as BitmapImage;
                case Actions.Stop:
                    return Application.Current.FindResource("StopBitmapImage") as BitmapImage;
                case Actions.BuildMainHall:
                    return Application.Current.FindResource("BuildTownhallBitmapImage") as BitmapImage;
                case Actions.BuildWall:
                    return Application.Current.FindResource("BuildWallBitmapImage") as BitmapImage;
                case Actions.BuildBarracs:
                    return Application.Current.FindResource("BuildBarracsBitmapImage") as BitmapImage;
                case Actions.TrainWorker:
                    return Application.Current.FindResource("TrainWorkerBitmapImage") as BitmapImage;
                case Actions.TrainSoldier:
                    return Application.Current.FindResource("TrainSoldierBitmapImage") as BitmapImage;
                case Actions.TrainDoctor:
                    return Application.Current.FindResource("TrainDoctorBitmapImage") as BitmapImage;
                case Actions.TrainGunMan:
                    return Application.Current.FindResource("TrainGunmanBitmapImage") as BitmapImage;
                case Actions.TrainPozsiHero:
                    return Application.Current.FindResource("TrainPozsiHeroBitmapImage") as BitmapImage;
                case Actions.BuildMedicalTent:
                    return Application.Current.FindResource("BuildMedicalTentBitmapImage") as BitmapImage;
                case Actions.BuildTower:
                    return Application.Current.FindResource("BuildTowerBitmapImage") as BitmapImage;
                case Actions.AutoHeal:
                    return Application.Current.FindResource("AutoHealBitmapImage") as BitmapImage;
                case Actions.AutoAttack:
                    return Application.Current.FindResource("AutoAttackBitmapImage") as BitmapImage;
                case Actions.BuildFarm:
                    return Application.Current.FindResource("BuildFarmBitmapImage") as BitmapImage;
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