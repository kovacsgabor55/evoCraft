using EvoCraft.Common.Map;
using EvoCraft.Common.MapObjects.PlayerControlled;

namespace EvoCraft.Common.MapObjects.Resources.Animals
{
    public abstract class AggressiveAnimal : Animal
    {
        public AggressiveAnimal(string Label, int maxCapacity, int maximalHealthPoints, int damage) : base(Label, maxCapacity, maximalHealthPoints)
        {
            Damage = damage;
        }

        public int Damage { get; set; }

        public Point MoveTarget { get; set; }
    }
}
