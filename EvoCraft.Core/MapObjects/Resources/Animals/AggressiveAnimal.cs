using EvoCraft.Common;

namespace EvoCraft.Core
{
    public abstract class AggressiveAnimal : Animal
    {
        internal AggressiveAnimal(string Label, int maxCapacity, int maximalHealthPoints, int damage) : base(Label, maxCapacity, maximalHealthPoints)
        {
            Damage = damage;
        }

        public int Damage { get; set; }

        public Point MoveTarget { get; set; }

        /// <summary>
        /// Attack the target if next to it.
        /// </summary>
        internal void Attack(Point pos)
        {
            if (MoveTarget != null && pos.DistanceFrom(MoveTarget) == 1)
            {
                PlayerControlled playerctrl = null;
                foreach (MapObject mo in Engine.Map.GetCellAt(MoveTarget).MapObjects)
                {
                    if (mo.GetType().IsSubclassOf(typeof(PlayerControlled)))
                    {
                        playerctrl = (PlayerControlled)mo;
                        playerctrl.TakeDamage(Damage);
                        break;
                    }
                }
                if (playerctrl != null && playerctrl.ActualHealthPoints <= 0)
                {
                    Engine.DestroyMapObject(playerctrl, MoveTarget);
                }
            }
        }

        public override void Update()
        {
            bool found;
            Point pos = Engine.GetMapObjectPosition(this, out found);
            if (found)
            {
                if (!Dead)
                {
                    Attack(pos);
                    MoveWithPathfinding(pos);
                }
                else
                {
                    Decay();
                    if (Capacity <= 0)
                    {
                        Engine.DestroyMapObject(this);
                    }
                }
            }
            
        }

        internal virtual void MoveWithPathfinding(Point pos)
        {
            if (MoveTarget != null)
            {
                Engine.MoveMapObject(this, Engine.GetDirectionForPathToTargetPosition(pos, MoveTarget), pos);
            }
        }

    }
}
