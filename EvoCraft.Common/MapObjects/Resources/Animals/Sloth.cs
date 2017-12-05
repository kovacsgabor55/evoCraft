﻿namespace EvoCraft.Common.MapObjects.Resources.Animals
{
    public class Sloth: Animal
    {
        public Sloth() : base("Sloth", 200, 20) { }

        public int slowNum = 0;
        public int limit = 5;

        public override void Move()
        {
            if (slowNum > limit)
            {
                slowNum = 0;
                limit = randomNum.Next(4) + 3;
                int direction = randomNum.Next(4);
                switch (direction)
                {
                    case 0:
                        Engine.MoveMapObject(this, Direction.Up);
                        break;
                    case 1:
                        Engine.MoveMapObject(this, Direction.Down);
                        break;
                    case 2:
                        Engine.MoveMapObject(this, Direction.Left);
                        break;
                    case 3:
                        Engine.MoveMapObject(this, Direction.Right);
                        break;
                }
            }
            else
            {
                slowNum++;
            }
            
        }
    }
}
