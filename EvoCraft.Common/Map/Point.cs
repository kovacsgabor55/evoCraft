namespace EvoCraft.Common.Map
{
    /// <summary>
    /// Egy helyt jelölő osztály, aminek hasznos metódusai vannak a működéshez végzett számításokhoz.
    /// </summary>
    public class Point
    {
        public int x;
        public int y;

        /// <summary>
        /// Create a point with x and y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            Point p = (Point)obj;
            return (x == p.x) && (y == p.y);
        }
        
        public override string ToString()
        {
            return "Point: x = " + x + " y = " + y;
        }
    }
}
