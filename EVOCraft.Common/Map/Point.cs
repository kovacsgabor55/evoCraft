using System;
using System.Collections.Generic;

namespace EvoCraft.Core
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

        /// <summary>
        /// Checks if a Point is in the List based on equality.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        internal bool IsInList(List<Point> points)
        {
            foreach(Point p in points)
            {
                if (p.Equals(this))
                {
                    return true;
                }
            }
            return false;
        }
        
        public override string ToString()
        {
            return "Point: x = " + x + " y = " + y;
        }
        /// <summary>
        /// Mennyi a távolsága egy másik ponttól.
        /// </summary>
        public int DistanceFrom(Point other)
        {
            return Math.Abs(this.x - other.x) + Math.Abs(this.y - other.y);
        }

        /// <summary>
        /// Meegyezik e az x vagy az y koordinátája.
        /// </summary>
        public bool IsOnTheSameAxisParallelLine(Point p)
        {
            return this.x == p.x || this.y == p.y;
        }
    }
}
