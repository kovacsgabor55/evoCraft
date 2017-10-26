using System.Collections.Generic;

namespace EvoCraft.Core
{
    public class Doctor: Unit
    {
        public static readonly int FoodCost = 300;
        public static readonly int WoodCost = 0;
        public static readonly int GoldCost = 200;

        public Doctor(int PlayerId): base("Dr. Doge", 50, 10, 1, 20, PlayerId, null, new List<Actions> { Actions.AutoHeal }, 6, new ResourceSet(GoldCost, FoodCost, WoodCost))
        {

        }


        private bool WasGoingAfterAUnit = false;

        public override void Update()
        {
            bool found;
            Point pos = Engine.GetMapObjectPosition(this, out found);
            if (found)
            {
                if (AlertMode)
                {
                    Point p = Engine.GetClosestInjuredUnitInRange(pos, SightRange);
                    if (p != null)
                    {
                        MoveTarget = p;
                        WasGoingAfterAUnit = true;
                    }
                    else
                    {
                        if (WasGoingAfterAUnit)
                        {
                            MoveTarget = null;
                            WasGoingAfterAUnit = false;
                        }
                    }
                }
                Heal(pos);
                Move(pos);
            }
        }

        /// <summary>
        /// Heal the target if next to it.
        /// </summary>
        internal void Heal(Point pos)
        {
            if (MoveTarget != null && pos.DistanceFrom(MoveTarget) == 1)
            {
                foreach (MapObject mo in Engine.Map.GetCellAt(MoveTarget).MapObjects)
                {
                    if (mo.GetType().IsSubclassOf(typeof(Unit)))
                    {
                        Unit a = (Unit)mo;
                        a.TakeHealing(Damage);
                    }
                }
            }
        }

    }
}
