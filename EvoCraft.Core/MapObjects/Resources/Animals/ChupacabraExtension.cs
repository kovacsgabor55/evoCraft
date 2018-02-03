using EvoCraft.Common.Map;
using EvoCraft.Common.MapObjects.Resources.Animals;
using System;

namespace EvoCraft.Core.MapObjects.Resources.Animals
{
    public static class ChupacabraExtension
    {
        public static void MoveWithPathfinding(this Chupacabra chupacabra, Point pos)
        {
            if (chupacabra.slowNum > 0)
            {
                chupacabra.slowNum = 0;
                bool found;
                chupacabra.MoveTarget = Engine.GetClosestUnitOrBuildingInRange(pos, 5, out found);
                if (chupacabra.MoveTarget != null && found)
                {
                    Engine.MoveMapObject(chupacabra, Engine.GetDirectionForPathToTargetPosition(pos, chupacabra.MoveTarget), pos);
                }
                else
                {
                    Random rnd = new Random();
                    int direction = rnd.Next(12);
                    switch (direction)
                    {
                        case 0:
                            Engine.MoveMapObject(chupacabra, Direction.Up);
                            break;
                        case 1:
                            Engine.MoveMapObject(chupacabra, Direction.Down);
                            break;
                        case 2:
                            Engine.MoveMapObject(chupacabra, Direction.Left);
                            break;
                        case 3:
                            Engine.MoveMapObject(chupacabra, Direction.Right);
                            break;
                    }
                }
            }
            else
            {
                chupacabra.slowNum++;
            }

        }
    }
}
