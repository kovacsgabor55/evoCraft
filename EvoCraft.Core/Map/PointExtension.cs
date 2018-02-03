using EvoCraft.Common.Map;
using System;
using System.Collections.Generic;

namespace EvoCraft.Core
{
    public static class PointExtension
    {
        /// <summary>
        /// Checks if a Point is in the List based on equality.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Meegyezik e az x vagy az y koordinátája.
        /// </summary>
        public static bool IsOnTheSameAxisParallelLine(this Point point, Point p)
        {
            return point.x == p.x || point.y == p.y;
        }

        /// <summary>
        /// Mennyi a távolsága egy másik ponttól.
        /// </summary>
        public static int DistanceFrom(this Point point, Point other)
        {
            return Math.Abs(point.x - other.x) + Math.Abs(point.y - other.y);
        }
    }
}


