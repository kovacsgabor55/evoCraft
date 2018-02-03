using System;
using System.Collections.Generic;

namespace EvoCraft.Common.MapObjects.PlayerControlled.Units
{
    [Serializable]
    public class Soldier : Unit
    {
        public static readonly int FoodCost = 200;
        public static readonly int WoodCost = 0;
        public static readonly int GoldCost = 100;

        public Soldier(int PlayerId) : base("Soldier", 350, 50, 1, 20, PlayerId, null, new List<Actions> { Actions.AutoAttack }, 9, new ResourceSet(GoldCost, FoodCost, WoodCost)) { }
        
        public bool WasChasingEnemy = false;
    }
}
