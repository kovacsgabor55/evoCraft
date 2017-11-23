namespace EvoCraft.Core
{
    public class Boss : AggressiveAnimal
    {
        public Boss() : base("Grumpy Cat", 1000, 1000, 60) { }

        internal override void MoveWithPathfinding(Point pos)
        {
            bool found;
            MoveTarget = Engine.GetClosestUnitOrBuildingInRange(pos, 8, out found);
            if (MoveTarget != null && found)
            {
                Engine.MoveMapObject(this, Engine.GetDirectionForPathToTargetPosition(pos, MoveTarget), pos);
            }
        }
    }
}