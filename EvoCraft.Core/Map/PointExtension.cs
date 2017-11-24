using EvoCraft.Common;
using System.Collections.Generic;

namespace EvoCraft.Core
{
    public static class PointExtension
    {
        public static bool IsInList(this Point point, List<Point> points)
        {
            foreach (Point p in points)
            {
                if (p.Equals(point))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
