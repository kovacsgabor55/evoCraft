using EvoCraft.Common;
using System;
using System.Collections.Generic;

namespace EvoCraft.Core
{
    [Serializable]
    public class GunMan : Unit
    {
        public static readonly int FoodCost = 200;
        public static readonly int WoodCost = 100;
        public static readonly int GoldCost = 250;

        public GunMan(int PlayerId) : base("Gun Man", 200, 40, 1, 20, PlayerId, null, new List<Actions> { }, 8, new ResourceSet(GoldCost, FoodCost, WoodCost)) { }
        
        private int ShootCooldown = 9;
        private int ActualShootCooldown;
        public Point Target { get; set; }
        private int slowNum = 0;

        public override void Update()
        {
            bool found;
            Point pos = Engine.GetMapObjectPosition(this, out found);
            if (found)
            {
                if (ShootCooldown <= ActualShootCooldown)
                {
                    ActualShootCooldown = 0;
                    Target = Engine.GetClosestAggressiveAnimalInRange(pos, SightRange - 1);

                    if (Target != null)
                    {
                        Engine.Map.GetCellAt(pos.x, pos.y).MapObjects.Add(new Bullet(pos, Target, Damage, 10));
                    }
                }
                else
                {
                    ActualShootCooldown++;
                }
                if (slowNum > 0)
                {
                    Move(pos);
                    slowNum = 0;
                }
                else
                {
                    slowNum++;
                }
                
            }
        }

    }
}
