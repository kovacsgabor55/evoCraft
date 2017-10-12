namespace EvoCraft.Common
{
    public class ResourceSet
    {
        public int Gold { get; set; }
        public int Wood { get; set; }
        public int Food { get; set; }
        
        public ResourceSet(int Gold, int Food, int Wood)
        {
            this.Gold = Gold;
            this.Food = Food;
            this.Wood = Wood;
        }
        
        public void ReduceBy(ResourceSet other)
        {
            this.Gold -= other.Gold;
            this.Wood -= other.Wood;
            this.Food -= other.Food;
        }
        public bool HasEnoughToReduceBy(ResourceSet other)
        {
            return  this.Gold >= other.Gold &&
                    this.Wood >= other.Wood &&
                    this.Food >= other.Food;
        }
        public void Add(ResourceSet other)
        {
            this.Gold += other.Gold;
            this.Wood += other.Wood;
            this.Food += other.Food;
        }
        public void AddGivenTimes(ResourceSet other, int times)
        {
            this.Gold += other.Gold * times;
            this.Wood += other.Wood * times;
            this.Food += other.Food * times;
        }
    }
}
