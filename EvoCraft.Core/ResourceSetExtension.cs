using EvoCraft.Common;

namespace EvoCraft.Core
{
    public static class ResourceSetExtension
    {
        public static void ReduceBy(this ResourceSet resourceSet, ResourceSet other)
        {
            resourceSet.Gold -= other.Gold;
            resourceSet.Wood -= other.Wood;
            resourceSet.Food -= other.Food;
        }
        public static bool HasEnoughToReduceBy(this ResourceSet resourceSet, ResourceSet other)
        {
            return resourceSet.Gold >= other.Gold &&
                    resourceSet.Wood >= other.Wood &&
                    resourceSet.Food >= other.Food;
        }
        public static void Add(this ResourceSet resourceSet, ResourceSet other)
        {
            resourceSet.Gold += other.Gold;
            resourceSet.Wood += other.Wood;
            resourceSet.Food += other.Food;
        }
        public static void AddGivenTimes(this ResourceSet resourceSet, ResourceSet other, int times)
        {
            resourceSet.Gold += other.Gold * times;
            resourceSet.Wood += other.Wood * times;
            resourceSet.Food += other.Food * times;
        }
    }
}
