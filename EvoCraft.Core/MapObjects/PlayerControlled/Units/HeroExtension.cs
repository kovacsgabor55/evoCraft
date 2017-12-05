using EvoCraft.Common.Map;
using EvoCraft.Common.MapObjects.PlayerControlled.Units;

namespace EvoCraft.Core.MapObjects.PlayerControlled.Units
{
    public static class HeroExtension
    {
        public static void Update(this Hero hero, Point pos)
        {
            if (hero.AlertMode)
            {
                Point p = Engine.GetClosestAggressiveAnimalInRange(pos, 5);
                if (p != null)
                {
                    hero.MoveTarget = p;
                    hero.WasChasingEnemy = true;
                }
                else
                {
                    if (hero.WasChasingEnemy)
                    {
                        hero.MoveTarget = null;
                        hero.WasChasingEnemy = false;
                    }
                }
            }
            hero.Attack(pos);
            hero.Move(pos);
        }
    }
}
