namespace EvoCraft.Common
{
    public class Tower : Building
    {
        public static int FoodCost = 0;
        public static int GoldCost = 200;
        public static int WoodCost = 200;

        public Tower(int PlayerId) : this(PlayerId, true) { }
        public Tower(int PlayerId, bool UnderConstruction) 
            : base("Tower", 100, 10, PlayerId, null, UnderConstruction, 4, new ResourceSet(GoldCost, FoodCost, WoodCost))
        {
            if (!UnderConstruction)
            {
                FinishBuilding();
                
            }
        }

        private int ShootCooldown = 8;
        private int ActualShootCooldown;
        public Point Target { get; set; }

        public override void Update()
        {
            if (IsUnderConstruction && BuildTime == 0)
            {
                IsUnderConstruction = false;
                FinishBuilding();
            }
            else
            {
                if (ShootCooldown <= ActualShootCooldown)
                {
                    ActualShootCooldown = 0;
                    bool found;
                    Point pos = Engine.GetMapObjectPosition(this, out found);
                    Target = Engine.GetClosestAggressiveAnimalInRange(pos, SightRange-1);

                    if (found && Target != null)
                    {
                        Engine.Map.GetCellAt(pos.x, pos.y).MapObjects.Add(new Bullet(pos, Target, 50, 10));
                    }
                }
                else
                {
                    ActualShootCooldown++;
                }
                
            }
        }

        internal override void FinishBuilding()
        {
            ActualHealthPoints = MaximalHealthPoints;
            SightRange = 11;
        }
    }
}
