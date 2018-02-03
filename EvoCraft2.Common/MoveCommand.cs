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
           Coordinate coord = Coordinate.GetCoordinate();

            int deltaX = Unit.Position.Coordinates[0] - Unit.Target.Coordinates[0];
            int deltaY = Unit.Position.Coordinates[1] - Unit.Target.Coordinates[1];

            if (deltaX > 0) //Left
            {
                if (deltaY > 0) //UpLeft
                {
                    Unit.Position.Coordinates[0] -= 1;
                    Unit.Position.Coordinates[1] -= 1;
                }
                else if (deltaY < 0) //DownLeft
                {
                    Unit.Position.Coordinates[0] -= 1;
                    Unit.Position.Coordinates[1] += 1;
                }
                else //Left
                {
                    Unit.Position.Coordinates[0] -= 1;
                }
            }
            else if (deltaX < 0) //Right
            {
                if (deltaY > 0) //UpRight
                {
                    Unit.Position.Coordinates[0] += 1;
                    Unit.Position.Coordinates[1] -= 1;
                }
                else if (deltaY < 0) //DownRight
                {
                    Unit.Position.Coordinates[0] += 1;
                    Unit.Position.Coordinates[1] += 1;
                }
                else //Right
                {
                    Unit.Position.Coordinates[0] += 1;
                }
            }
            else //deltaX == 0
            {
                if (deltaY > 0)
                {
                    //UP
                    Unit.Position.Coordinates[1] -= 1;
                }
                else if (deltaY < 0)
                {
                    //Down
                    Unit.Position.Coordinates[1] += 1;
                }
                else
                {
                    //No Movement
                }
            }
        }
    }
}
