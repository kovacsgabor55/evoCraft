namespace EvoCraft.Core
{
    public class Rolls : AggressiveAnimal
    {
        public Rolls() : base("Rolls Safe", 500, 400, 70) { }

        internal override void MoveWithPathfinding(Point pos)
        {
            bool found;
            MoveTarget = Engine.GetClosestUnitOrBuildingInRange(pos, 14, out found);
            if (MoveTarget != null && found)
            {
                Engine.MoveMapObject(this, Engine.GetDirectionForPathToTargetPosition(pos, MoveTarget), pos);
            }
        }
    }
}
