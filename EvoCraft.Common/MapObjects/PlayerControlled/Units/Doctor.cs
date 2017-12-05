using EvoCraft.Common.Map;
using System.Collections.Generic;

namespace EvoCraft.Common.MapObjects.PlayerControlled.Units
{
    public class Doctor: Unit
    {
        public static readonly int FoodCost = 300;
        public static readonly int WoodCost = 0;
        public static readonly int GoldCost = 200;

        public Doctor(int PlayerId): base("Dr. Doge", 50, 10, 1, 20, PlayerId, null, new List<Actions> { Actions.AutoHeal }, 6, new ResourceSet(GoldCost, FoodCost, WoodCost))
        {

        }

        public bool WasGoingAfterAUnit = false;
    }
}
