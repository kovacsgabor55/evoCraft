namespace EvoCraft2.Common
{
    public class MapObject : ICoordinate
    {
        public Coordinate CoordinateVal { get; set; }
        public Coordinate GetCoordinate()
        {
            return CoordinateVal;
        }
    }
}
