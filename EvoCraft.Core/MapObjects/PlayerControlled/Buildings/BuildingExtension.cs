using EvoCraft.Common.MapObjects.PlayerControlled.Buildings;

namespace EvoCraft.Core
{
    public static class BuildingExtension
    {
        public static void beBuilt<T>(this T building) where T : Building
        {
            if (building.IsUnderConstruction && building.BuildTime > 0)
            {
                building.BuildTime--;
                building.ActualHealthPoints += building.MaximalHealthPoints / building.InitialBuildTime;
            }
            else
            {
                building.IsUnderConstruction = false;

                building.ActualHealthPoints = building.MaximalHealthPoints;
                building.ActionsAvailable = true;
            }       
        }
    }
}