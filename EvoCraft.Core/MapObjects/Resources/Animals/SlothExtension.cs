using EvoCraft.Common.Map;
using EvoCraft.Common.MapObjects.Resources.Animals;
using System;

namespace EvoCraft.Core.MapObjects.Resources.Animals
{
    public static class SlothExtension
    {
        public static void Move(this Sloth sloth)
        {
            if (sloth.slowNum > sloth.limit)
            {
                Random rnd = new Random();

                sloth.slowNum = 0;
                sloth.limit = rnd.Next(4) + 3;
                int direction = rnd.Next(4);
                switch (direction)
                {
                    case 0:
                        Engine.MoveMapObject(sloth, Direction.Up);
                        break;
                    case 1:
                        Engine.MoveMapObject(sloth, Direction.Down);
                        break;
                    case 2:
                        Engine.MoveMapObject(sloth, Direction.Left);
                        break;
                    case 3:
                        Engine.MoveMapObject(sloth, Direction.Right);
                        break;
                }
            }
            else
            {
                sloth.slowNum++;
            }
        }
    }
}
