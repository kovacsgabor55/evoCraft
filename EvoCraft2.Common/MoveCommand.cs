using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoCraft2.Common
{
    public class MoveCommand : Command
    {
        public Unit Unit;
        public ICoordinate Coordinate;

        public MoveCommand(Unit unit, ICoordinate coordinate)
        {
            Unit = unit;
            Coordinate = coordinate;
        }

        internal override void Execute()
        {
           Coordinate coord = Coordinate.GetPosition();

            if (Unit.Target != null)
            {
                if (Unit.Target.X != coord.X || Unit.Target.Y != coord.Y)
                {
                    Unit.Target = coord;
                }
            }
            else
            {
                Unit.Target = coord;
            }            

            int deltaX = Unit.Position.X - Unit.Target.X;
            int deltaY = Unit.Position.Y - Unit.Target.Y;

            if (deltaX > 0) //Left
            {
                if (deltaY > 0) //UpLeft
                {
                    Unit.Position.X -= 1;
                    Unit.Position.Y -= 1;
                }
                else if (deltaY < 0) //DownLeft
                {
                    Unit.Position.X -= 1;
                    Unit.Position.Y += 1;
                }
                else //Left
                {
                    Unit.Position.X -= 1;
                }
            }
            else if (deltaX < 0) //Right
            {
                if (deltaY > 0) //UpRight
                {
                    Unit.Position.X += 1;
                    Unit.Position.Y -= 1;
                }
                else if (deltaY < 0) //DownRight
                {
                    Unit.Position.X += 1;
                    Unit.Position.Y += 1;
                }
                else //Right
                {
                    Unit.Position.X += 1;
                }
            }
            else //deltaX == 0
            {
                if (deltaY > 0)
                {
                    //UP
                    Unit.Position.Y -= 1;
                }
                else if (deltaY < 0)
                {
                    //Down
                    Unit.Position.Y += 1;
                }
                else
                {
                    //No Movement
                }
            }
        }
    }
}
