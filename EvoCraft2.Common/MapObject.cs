namespace EvoCraft2.Common
{
    public class MapObject : ICoordinate
    {
        public Coordinate Position { get; set; }
        public Coordinate GetPosition()
        {
            return Position;
        }
    }
}
