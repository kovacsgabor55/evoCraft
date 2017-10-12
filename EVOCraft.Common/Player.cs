namespace EvoCraft.Common
{
    public class Player : Profile
    {
        private int myScore;

        public ResourceSet Resources { get; set; }

        public int Score
        {
            get { return myScore; }
            internal set { myScore = value; }
        }

        public byte MyTeam
        {
            get;
            internal set;
        }

        internal Player(int food, int gold, int wood)
        {
            Resources = new ResourceSet(gold, food, wood);
            myScore = 0;
        }
    }
}
