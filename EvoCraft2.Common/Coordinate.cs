using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoCraft2.Common
{
    public class Coordinate //:ICoordinate
    {
        public int X;
        public int Y;

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return "X: " + X + ", Y: " + Y;
        }
    }    

    public class DummyCoordinate : ICoordinate
    {
        public DummyCoordinate(int x, int y)
        {
            Coord = new Coordinate(x, y);
        }

        public Coordinate Coord { get; set; }
        public Coordinate GetPosition()
        {
            return Coord;
        }
    }
}
