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
    }
}
