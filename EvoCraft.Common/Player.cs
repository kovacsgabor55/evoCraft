using EvoCraft.Common;

namespace EvoCraft.Core
{
    public class Player : Profile
    {
        private int myScore;

        public ResourceSet Resources { get; set; }

        public int Score
        {
            get { return myScore; }
            public set { myScore = value; }
        }

        public byte MyTeam
        {
            get;
            public set;
        }

        public Player(int food, int gold, int wood)
        {
            Resources = new ResourceSet(gold, food, wood);
            myScore = 0;
        }
    }
}
