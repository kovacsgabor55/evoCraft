using System;
using System.Collections.Generic;

namespace EvoCraft.Common
{
    [Serializable]
    public class Soldier : Unit
    {
        public static readonly int FoodCost = 200;
        public static readonly int WoodCost = 0;
        public static readonly int GoldCost = 100;

        public Soldier(int PlayerId) : base("Soldier", 350, 50, 1, 20, PlayerId, null, new List<Actions> { Actions.AutoAttack }, 9, new ResourceSet(GoldCost, FoodCost, WoodCost)) { }
        
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
