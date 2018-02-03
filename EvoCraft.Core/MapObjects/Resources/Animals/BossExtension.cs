using EvoCraft.Common.Map;
using EvoCraft.Common.MapObjects.Resources.Animals;

namespace EvoCraft.Core.MapObjects.Resources.Animals
{
    public static class BossExtension
    {
        public static void MoveWithPathfinding(this Boss boss, Point pos)
        {
            bool found;
            boss.MoveTarget = Engine.GetClosestUnitOrBuildingInRange(pos, 8, out found);
            if (boss.MoveTarget != null && found)
            {
                Engine.MoveMapObject(boss, Engine.GetDirectionForPathToTargetPosition(pos, boss.MoveTarget), pos);
            }
        }
    }
}
