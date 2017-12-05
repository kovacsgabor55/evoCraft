using System.Collections.Generic;

namespace EvoCraft.Common.MapObjects.PlayerControlled.Units
{
    public class Hero: Unit
    {
        public static readonly int FoodCost = 500;
        public static readonly int WoodCost = 0;
        public static readonly int GoldCost = 1500;

        public Hero(int PlayerId): base("Hero", 1000, 75, 1, 60, PlayerId, null, new List<Actions> { Actions.AutoAttack }, 11, new ResourceSet(GoldCost, FoodCost, WoodCost))
        {

        }

        public bool WasChasingEnemy = false;
    }
}
