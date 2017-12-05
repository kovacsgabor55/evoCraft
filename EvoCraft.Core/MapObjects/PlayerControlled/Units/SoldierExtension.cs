using EvoCraft.Common.Map;
using EvoCraft.Common.MapObjects.PlayerControlled.Units;

namespace EvoCraft.Core.MapObjects.PlayerControlled.Units
{
    public static class SoldierExtension
    {
        public static void Update(this Soldier soldier, Point pos)
        {
            if (soldier.AlertMode)
            {
                Point p = Engine.GetClosestAggressiveAnimalInRange(pos, 5);
                if (p != null)
                {
                    soldier.MoveTarget = p;
                    soldier.WasChasingEnemy = true;
                }
                else
                {
                    if (soldier.WasChasingEnemy)
                    {
                        soldier.MoveTarget = null;
                        soldier.WasChasingEnemy = false;
                    }
                }
            }
            soldier.Attack(pos);
            soldier.Move(pos);
        }
    }
}
