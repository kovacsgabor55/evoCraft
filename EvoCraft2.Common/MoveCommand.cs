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
        }
    }
}
