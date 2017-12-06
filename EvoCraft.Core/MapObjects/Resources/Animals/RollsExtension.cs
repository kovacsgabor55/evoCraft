using EvoCraft.Common.Map;
using EvoCraft.Common.MapObjects.Resources.Animals;

namespace EvoCraft.Core.MapObjects.Resources.Animals
{
    public static class RollsExtension
    {
        public static void MoveWithPathfinding(this Rolls rolls, Point pos)
        {
            bool found;
            rolls.MoveTarget = Engine.GetClosestUnitOrBuildingInRange(pos, 14, out found);
            if (rolls.MoveTarget != null && found)
            {
                Engine.MoveMapObject(rolls, Engine.GetDirectionForPathToTargetPosition(pos, rolls.MoveTarget), pos);
            }
        }
    }
}
