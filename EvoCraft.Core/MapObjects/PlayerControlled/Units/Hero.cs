using EvoCraft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoCraft.Core
{
    public class Hero: Unit
    {
        public static readonly int FoodCost = 500;
        public static readonly int WoodCost = 0;
        public static readonly int GoldCost = 1500;

        public Hero(int PlayerId): base("Hero", 1000, 75, 1, 60, PlayerId, null, new List<Actions> { Actions.AutoAttack }, 11, new ResourceSet(GoldCost, FoodCost, WoodCost))
        {

        }

        private bool WasChasingEnemy = false;

        public override void Update()
        {
            bool found;
            Point pos = Engine.GetMapObjectPosition(this, out found);
            if (found)
            {
                if (AlertMode)
                {
                    Point p = Engine.GetClosestAggressiveAnimalInRange(pos, 5);
                    if (p != null)
                    {
                        MoveTarget = p;
                        WasChasingEnemy = true;
                    }
                    else
                    {
                        if (WasChasingEnemy)
                        {
                            MoveTarget = null;
                            WasChasingEnemy = false;
                        }
                    }
                }
                Attack(pos);
                Move(pos);
            }
        }
    }
}
