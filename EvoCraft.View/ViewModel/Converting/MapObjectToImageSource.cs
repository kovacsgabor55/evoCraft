using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace View
{
    /// <summary>
    /// This helper class converts the MapOjectImage enum to the right image resource.
    /// </summary>
    public class MapObjectToImageSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((MapObjectImage)value)
            {
                case MapObjectImage.Worker:
                    return Application.Current.FindResource("DogeBitmapImage") as BitmapImage;
                case MapObjectImage.Soldier:
                    return Application.Current.FindResource("SoldierBitmapImage") as BitmapImage;
                case MapObjectImage.MainHall:
                    return Application.Current.FindResource("TownHallRedBitmapImage") as BitmapImage;
                case MapObjectImage.Wall:
                    return Application.Current.FindResource("WallBitmapImage") as BitmapImage;
                case MapObjectImage.Barrack:
                    return Application.Current.FindResource("BarrackBitmapImage") as BitmapImage;
                case MapObjectImage.Construction:
                    return Application.Current.FindResource("ConstructionBitmapImage") as BitmapImage;
                case MapObjectImage.Doctor:
                    return Application.Current.FindResource("DoctorDogeBitmapImage") as BitmapImage;
                case MapObjectImage.Hero:
                    return Application.Current.FindResource("PozsiHeroBitmapImage") as BitmapImage;
                case MapObjectImage.MedicalTent:
                    return Application.Current.FindResource("MedicalTentBitmapImage") as BitmapImage;
                case MapObjectImage.Tower:
                    return Application.Current.FindResource("TowerBitmapImage") as BitmapImage;
                case MapObjectImage.Rolls:
                    return Application.Current.FindResource("RollsBitmapImage") as BitmapImage;
                case MapObjectImage.DeadRolls:
                    return Application.Current.FindResource("DeadRollsBitmapImage") as BitmapImage;

                case MapObjectImage.Tree:
                    return Application.Current.FindResource("TreeBitmapImage") as BitmapImage;
                case MapObjectImage.Rock:
                    return Application.Current.FindResource("RockBitmapImage") as BitmapImage;
                case MapObjectImage.Farm:
                    return Application.Current.FindResource("FarmBitmapImage") as BitmapImage;
                case MapObjectImage.Mine:
                    return Application.Current.FindResource("MineBitmapImage") as BitmapImage;
                case MapObjectImage.Chupacabra:
                    return Application.Current.FindResource("ChupacabraBitmapImage") as BitmapImage;
                case MapObjectImage.Llama:
                    return Application.Current.FindResource("LlamaBitmapImage") as BitmapImage;
                case MapObjectImage.Sloth:
                    return Application.Current.FindResource("SlothBitmapImage") as BitmapImage;
                case MapObjectImage.Boss:
                    return Application.Current.FindResource("BossBitmapImage") as BitmapImage;
                case MapObjectImage.DeadBoss:
                    return Application.Current.FindResource("BossDeadBitmapImage") as BitmapImage;
                case MapObjectImage.DeadChupacabra:
                    return Application.Current.FindResource("DeadChupyBitmapImage") as BitmapImage;
                case MapObjectImage.DeadLlama:
                    return Application.Current.FindResource("DeadLlamaBitmapImage") as BitmapImage;
                case MapObjectImage.DeadSloth:
                    return Application.Current.FindResource("DeadSlothBitmapImage") as BitmapImage;
                case MapObjectImage.Bullet:
                    return Application.Current.FindResource("BulletBitmapImage") as BitmapImage;
                case MapObjectImage.TreeCut:
                    return Application.Current.FindResource("TreeCutBitmapImage") as BitmapImage;
                case MapObjectImage.GunMan:
                    return Application.Current.FindResource("GunmanBitmapImage") as BitmapImage;
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
