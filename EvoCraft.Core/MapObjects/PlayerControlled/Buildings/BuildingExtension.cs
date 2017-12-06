using EvoCraft.Common.MapObjects.PlayerControlled.Buildings;

namespace EvoCraft.Core
{
    public static class BuildingExtension
    {
        public static void beBuilt(this Building building)
        {
            if (building.IsUnderConstruction && building.BuildTime > 0)
            {
                building.BuildTime--;
                building.ActualHealthPoints += building.MaximalHealthPoints / building.InitialBuildTime;
            }
        }
    }
}