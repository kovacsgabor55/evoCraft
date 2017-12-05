namespace EvoCraft.Common.MapObjects.Resources.Animals
{
    public class Chupacabra: AggressiveAnimal
    {
        public Chupacabra() : base("Chupacabra", 200, 300, 35) { }

        private int slowNum = 0;

        public override void MoveWithPathfinding(Point pos)
        {
            
            if (slowNum > 0)
            {
                slowNum = 0;
                bool found;
                MoveTarget = Engine.GetClosestUnitOrBuildingInRange(pos, 5, out found);
                if (MoveTarget != null && found)
                {
                    Engine.MoveMapObject(this, Engine.GetDirectionForPathToTargetPosition(pos, MoveTarget), pos);
                }
                else
                {
                    int direction = randomNum.Next(12);
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

                
            }
            else
            {
                slowNum++;
            }

        }
    }
}