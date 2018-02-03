using EvoCraft.Common.Map;
using EvoCraft.Common.MapObjects;
using EvoCraft.Common.MapObjects.PlayerControlled.Units;
using EvoCraft.Common.MapObjects.Resources.Animals;
using EvoCraft.Core.MapObjects.Resources.Animals;

namespace EvoCraft.Core.MapObjects.PlayerControlled.Units
{
    public static class UnitExtension
    {
        public static void Update(this Unit unit, Point pos)
        {
            Move(unit, pos);
            Attack(unit, pos);
        }

        public static void Move(this Unit unit, Point pos)
        {
            if (unit.MoveTarget != null)
            {
                Engine.MoveMapObject(unit, Engine.GetDirectionForPathToTargetPosition(pos, unit.MoveTarget), pos);
            }
        }

        /// <summary>
        /// Attack the target if next to it.
        /// </summary>
        public static void Attack(this Unit unit, Point pos)
        {
            if (unit.MoveTarget != null && pos.DistanceFrom(unit.MoveTarget) == 1)
            {
                foreach (MapObject mo in Engine.Map.GetCellAt(unit.MoveTarget).MapObjects)
                {
                    if (mo is Animal)
                    {
                        Animal a = (Animal)mo;
                        a.TakeDamage(unit.Damage);
                    }
                }
            }
        }
    }
}
