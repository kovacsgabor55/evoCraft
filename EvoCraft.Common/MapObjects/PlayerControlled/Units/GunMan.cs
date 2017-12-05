using EvoCraft.Common.Map;
using System;
using System.Collections.Generic;

namespace EvoCraft.Common.MapObjects.PlayerControlled.Units
{
    [Serializable]
    public class GunMan : Unit
    {
        public static readonly int FoodCost = 200;
        public static readonly int WoodCost = 100;
        public static readonly int GoldCost = 250;

        public GunMan(int PlayerId) : base("Gun Man", 200, 40, 1, 20, PlayerId, null, new List<Actions> { }, 8, new ResourceSet(GoldCost, FoodCost, WoodCost)) { }
        
        public int ShootCooldown = 9;
        public int ActualShootCooldown;
        public Point Target { get; set; }
        public int slowNum = 0;
    }
}
