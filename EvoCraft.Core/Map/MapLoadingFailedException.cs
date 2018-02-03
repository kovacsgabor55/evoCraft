using System;

namespace EvoCraft.Core
{
    /// <summary>
    /// Egy általánosító Exception annak a céljából, hogy egyértelmű legyen, hogy mit dobhat a MapLoader.
    /// </summary>
    public class MapLoadingFailedException : Exception
    {
        public MapLoadingFailedException()
        {
        }

        public MapLoadingFailedException(string message)
        : base(message)
        {
        }

        public MapLoadingFailedException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
