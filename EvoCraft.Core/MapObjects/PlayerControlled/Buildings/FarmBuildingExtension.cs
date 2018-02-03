using EvoCraft.Common.Map;
using EvoCraft.Common.MapObjects.PlayerControlled.Buildings;
using EvoCraft.Common.MapObjects.Resources;

namespace EvoCraft.Core.MapObjects.PlayerControlled.Buildings
{
    public static class FarmBuildingExtension
    {
        // it changes into a farm resource when it is finished.
        public static void FinishBuilding(this FarmBuilding farmBuilding, Point pos)
        {
            Engine.Map.GetCellAt(pos).MapObjects.Add(new Farm());
            Engine.DestroyMapObject(farmBuilding);
        }
    }
}
