using EvoCraft.Common.Map;

namespace EvoCraft.Common.MapObjects.Resources.Animals
{
    public class Chupacabra: AggressiveAnimal
    {
        public Chupacabra() : base("Chupacabra", 200, 300, 35) { }

        public int slowNum = 0;
    }
}