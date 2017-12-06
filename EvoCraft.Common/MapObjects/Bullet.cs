using EvoCraft.Common.Map;
using EvoCraft.Common.MapObjects.Resources.Animals;

namespace EvoCraft.Common.MapObjects
{
    public class Bullet : MapObject
    {
        public int Damage;
        public int LifeTime;
        public Point Target;

        public Bullet(Point from, Point to, int Damage, int lifeTime) : base (BlockType.NoBlock, 0, "Bullet")
        {
            this.Damage = Damage;
            this.LifeTime = lifeTime;
        }
    }
}
