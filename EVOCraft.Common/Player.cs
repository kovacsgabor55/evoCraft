using EvoCraft.Common;

namespace EvoCraft.Core
{
    public class Player : Profile
    {
        public int myScore;

        public ResourceSet Resources { get; set; }

        public int Score
        {
            get { return myScore; }
            set { myScore = value; }
        }

        public byte MyTeam
        {
            get;
            set;
        }

        public Player(int food, int gold, int wood)
        {
            Resources = new ResourceSet(gold, food, wood);
            myScore = 0;
        }
    }
}
