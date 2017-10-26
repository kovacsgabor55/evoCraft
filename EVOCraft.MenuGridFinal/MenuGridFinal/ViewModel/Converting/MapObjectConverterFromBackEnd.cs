using EvoCraft.Core;
using System;

namespace View
{
    /// <summary>
    /// Class that helps the conversion of the type of a MapObject to a MapObjectImage Enum.
    /// </summary>
    public class MapObjectConverterFromBackEnd
    {
        public static MapObjectImage Convert(MapObject mapObj)
        {
            // Rock
            if (mapObj.GetType() == typeof(Rock))
            {
                return MapObjectImage.Rock;
            }
            // Setting image if there is a unit
            if (mapObj.GetType() == typeof(Worker))
            {
                return MapObjectImage.Worker;
            }
            
            if (mapObj.GetType() == typeof(Soldier))
            {
                return MapObjectImage.Soldier;
            }
            if (mapObj.GetType() == typeof(Hero))
            {
                return MapObjectImage.Hero;
            }
            if (mapObj.GetType() == typeof(Doctor))
            {
                return MapObjectImage.Doctor;
            }
            if (mapObj.GetType() == typeof(GunMan))
            {
                return MapObjectImage.GunMan;
            }
            // Buildings
            if (mapObj.GetType().IsSubclassOf(typeof(Building)))
            {
                Building b = (Building)mapObj;
                if (b.IsUnderConstruction)
                {
                    return MapObjectImage.Construction;
                }
                else
                {
                    if (mapObj.GetType() == typeof(MainHall))
                    {
                        return MapObjectImage.MainHall;
                    }
                    if (mapObj.GetType() == typeof(Wall))
                    {
                        return MapObjectImage.Wall;
                    }
                    if (mapObj.GetType() == typeof(Barracks))
                    {
                        return MapObjectImage.Barrack;
                    }
                    if (mapObj.GetType() == typeof(Tower))
                    {
                        return MapObjectImage.Tower;
                    }
                    if (mapObj.GetType() == typeof(MedicalTent))
                    {
                        return MapObjectImage.MedicalTent;
                    }
                    if (mapObj.GetType() == typeof(FarmBuilding))
                    {
                        return MapObjectImage.Farm;
                    }
                }
                
            }
            
            // Resources
            if (mapObj.GetType() == typeof(Llama))
            {
                Animal animal = (Animal)mapObj;
                if (animal.Dead)
                {
                    return MapObjectImage.DeadLlama;
                }
                else
                {
                    return MapObjectImage.Llama;
                }
            }
            else if (mapObj.GetType() == typeof(Sloth))
            {
                Animal animal = (Animal)mapObj;
                if (animal.Dead)
                {
                    return MapObjectImage.DeadSloth;
                }
                else
                {
                    return MapObjectImage.Sloth;
                }
            }
            else if (mapObj.GetType() == typeof(Chupacabra))
            {
                Animal animal = (Animal)mapObj;
                if (animal.Dead)
                {
                    return MapObjectImage.DeadChupacabra;
                }
                else
                {
                    return MapObjectImage.Chupacabra;
                }
            }
            else if (mapObj.GetType() == typeof(Boss))
            {
                Animal animal = (Animal)mapObj;
                if (animal.Dead)
                {
                    return MapObjectImage.DeadBoss;
                }
                else
                {
                    return MapObjectImage.Boss;
                }
            }
            else if (mapObj.GetType() == typeof(Rolls))
            {
                Animal animal = (Animal)mapObj;
                if (animal.Dead)
                {
                    return MapObjectImage.DeadRolls;
                }
                else
                {
                    return MapObjectImage.Rolls;
                }
            }
            else if (mapObj.GetType() == typeof(Tree))
            {
                Tree tree = (Tree)mapObj;
                if (tree.HasFullCapacity())
                {
                    return MapObjectImage.Tree;
                }
                else
                {
                    return MapObjectImage.TreeCut;
                }
                
            }
            else if (mapObj.GetType() == typeof(Mine))
            {
                return MapObjectImage.Mine;
            }
            else if (mapObj.GetType() == typeof(Farm))
            {
                return MapObjectImage.Farm;
            }
            else if (mapObj.GetType() == typeof(Bullet))
            {
                return MapObjectImage.Bullet;
            }

            return MapObjectImage.None;
        }
    }
}
