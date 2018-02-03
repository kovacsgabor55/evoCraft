using EvoCraft.Common.Map;
using EvoCraft.Common.MapObjects;
using EvoCraft.Common.MapObjects.Resources.Animals;
using EvoCraft.Core.MapObjects.Resources.Animals;

namespace EvoCraft.Core.MapObjects
{
    public static class BulletExtension
    {
        public static void Update(this Bullet bullet)
        {
            bullet.LifeTime--;
            bool found;
            Point pos = Engine.GetMapObjectPosition(bullet, out found);
            if (found)
            {
                Point p = Engine.GetClosestAggressiveAnimalInRange(pos, 10);
                if (p != null)
                {
                    bullet.Target = p;
                }
                else
                {
                    bullet.Target = new Point(pos.x + 1, pos.y + 1);
                }
                Move(bullet, pos);
                Move(bullet, pos);
                Hit(bullet, pos);
                if (bullet.LifeTime <= 0)
                {
                    Engine.DestroyMapObject(bullet, pos);
                }
            }

        }

        public static void Hit(this Bullet bullet, Point pos)
        {
            bool hit = false;
            foreach (MapObject mo in Engine.Map.GetCellAt(pos).MapObjects)
            {
                if (mo is AggressiveAnimal)
                {
                    AggressiveAnimal a = (AggressiveAnimal)mo;
                    a.TakeDamage(bullet.Damage);
                    hit = true;
                }
            }
            if (hit)
            {
                Engine.DestroyMapObject(bullet, pos);
            }

        }

        public static void Move(this Bullet bullet, Point pos)
        {
            if (bullet.Target != null)
            {
                Engine.MoveMapObject(bullet, Engine.GetDirectionForPathToTargetPosition(pos, bullet.Target, bullet.BlockType), pos);
            }
        }
    }
}
