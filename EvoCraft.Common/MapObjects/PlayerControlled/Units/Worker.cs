using EvoCraft.Common.Map;
using EvoCraft.Common.MapObjects.PlayerControlled.Buildings;
using EvoCraft.Common.MapObjects.Resources;
using EvoCraft.Common.MapObjects.Resources.Animals;
using System;
using System.Collections.Generic;

namespace EvoCraft.Common.MapObjects.PlayerControlled.Units
{
    [Serializable]
    public class Worker : Unit
    {
        private static int WorkerFullHealth = 100;
        private static int WorkerDamage = 10;
        private static int WorkerSpeed = 10;
        private static int WorkerCapacity = 30;
        public static readonly int FoodCost = 100;
        public static readonly int WoodCost = 50;
        public static readonly int GoldCost = 0;
        private static int WorkerTrainingTime = 20;
        private static List<Actions> WorkerPossibleActions = new List<Actions> { Actions.Stop, Actions.BuildBarracs, Actions.BuildWall, Actions.BuildMainHall, Actions.BuildTower, Actions.BuildMedicalTent, Actions.BuildFarm};

        /// <summary>
        /// A munkás maximális tárolási kapacitása
        /// </summary>
         int Capacity
        {
            get
            {
                return myCapacity;
            }
        }

        /// <summary>
        /// A munkásnál épp nállalévő nyersanyag egység.
        /// </summary>
        public int Amount
        {
            get
            {
                return myAmount;
            }
        }

        /// <summary>
        /// The type of the resource carried
        /// </summary>
        public ResourceType CarriedResourceType
        {
            get
            {
                return myCarriedResourceType;
            }
            public set
            {
                myCarriedResourceType = value;
            }
        }
        /// <summary>
        /// Az aktuálisan végzett feladat.
        /// </summary>
        public Order NextOrder
        {
            get
            {
                return myNextOrder;
            }
            set
            {
                myNextOrder = value;
            }
        }

        public Point RememberedReturnTarget = null;
        public Point RememberedGatherTarget;
        
        /// <summary>
        /// Egy munkás létrehozását teszi lehetővé.
        /// </summary>
        public Worker(int PlayerId) 
            : base("Worker", WorkerFullHealth, WorkerDamage, WorkerSpeed, WorkerTrainingTime, PlayerId, null, WorkerPossibleActions, 7,
                  new ResourceSet(GoldCost, FoodCost, WoodCost))
        {
            myNextOrder = Order.None;
            myCapacity = WorkerCapacity;
            myAmount = 0;
        }
        
        /// <summary>
        /// Every action happens here.
        /// </summary>
        public override void Update()
        {
            bool found;
            Point pos = Engine.GetMapObjectPosition(this, out found);

            if (found)
            {
                if (Amount == Capacity && (myNextOrder == Order.GoToFood || myNextOrder == Order.GoToWood || myNextOrder == Order.GoToGold))
                {
                    myNextOrder = Order.ReturnResource;
                    if (RememberedReturnTarget == null)
                    {
                        RememberedReturnTarget = Engine.SearchClosestMainHall(pos);
                    }
                }
                if (Amount == 0 && myNextOrder == Order.ReturnResource)
                {
                    switch (CarriedResourceType)
                    {
                        case ResourceType.Gold: myNextOrder = Order.GoToGold; break;
                        case ResourceType.Wood: myNextOrder = Order.GoToWood; break;
                        case ResourceType.Food: myNextOrder = Order.GoToFood; break;
                    }
                }
                switch (myNextOrder)
                {
                    case Order.ReturnResource:
                        MoveTarget = RememberedReturnTarget; break;
                    case Order.Hunt:
                        bool isDead;
                        MoveTarget = Engine.SearchClosestPeacefulAnimalInRange(pos, SightRange, out isDead);
                        if (isDead)
                        {
                            NextOrder = Order.GoToFood;
                            RememberedGatherTarget = MoveTarget;
                        }
                        break;

                    case Order.GoToFood:
                        bool ResourceIsNotValid = true;
                        foreach (MapObject mo in Engine.Map.GetCellAt(RememberedGatherTarget).MapObjects)
                        {
                            if (mo is Resource)
                            {
                                Resource res = (Resource)mo;
                                if (res.Type == ResourceType.Food)
                                {
                                    if (mo is Animal)
                                    {
                                        Animal animal = (Animal)mo;
                                        if (animal.Dead)
                                        {
                                            ResourceIsNotValid = false;
                                        }
                                    }
                                    else
                                    {
                                        ResourceIsNotValid = false;
                                    }
                                    
                                }
                            }
                        }
                        if (ResourceIsNotValid)
                        {
                            myNextOrder = Order.Hunt;
                            
                            MoveTarget = Engine.SearchClosestPeacefulAnimalInRange(pos, SightRange, out isDead);
                            if (isDead)
                            {
                                NextOrder = Order.GoToFood;
                                RememberedGatherTarget = MoveTarget;
                            }
                        }
                        MoveTarget = RememberedGatherTarget;
                        break;
                    case Order.GoToGold:
                        ResourceIsNotValid = true;
                        foreach (MapObject mo in Engine.Map.GetCellAt(RememberedGatherTarget).MapObjects)
                        {
                            if (mo is Resource)
                            {
                                Resource res = (Resource)mo;
                                if (res.Type == ResourceType.Gold)
                                {
                                    ResourceIsNotValid = false;
                                }
                            }
                        }
                        if (ResourceIsNotValid)
                        {
                            RememberedGatherTarget = Engine.SearchClosestResourceInRange(pos, ResourceType.Gold, SightRange);
                        }
                        MoveTarget = RememberedGatherTarget;
                        break;
                    case Order.GoToWood:
                        ResourceIsNotValid = true;
                        foreach (MapObject mo in Engine.Map.GetCellAt(RememberedGatherTarget).MapObjects)
                        {
                            if (mo is Resource)
                            {
                                Resource res = (Resource)mo;
                                if (res.Type == ResourceType.Wood)
                                {
                                    ResourceIsNotValid = false;
                                }
                            }
                        }
                        if (ResourceIsNotValid)
                        {
                            RememberedGatherTarget = Engine.SearchClosestResourceInRange(pos, ResourceType.Wood, SightRange);
                        }
                        MoveTarget = RememberedGatherTarget;
                        break;
                }


                if (myNextOrder == Order.GoToFood || myNextOrder == Order.GoToGold || myNextOrder == Order.GoToWood)
                {
                    Gather(pos);
                }
                if (myNextOrder == Order.ReturnResource)
                {
                    ReturnResource(pos);
                }
                if (myNextOrder == Order.Build)
                {
                    Build(pos);
                }
                if (myNextOrder == Order.Hunt)
                {
                    Attack(pos);
                }
                
                Move(pos);
            }
        }
        
        public void ResetAmount()
        {
            myAmount = 0;
        }

        /// <summary>
        /// Adds the amount if it can. If it can't it simply ignores the amount.
        /// </summary>
         void AddAmount(int amount)
        {
            if (myCapacity > myAmount)
            {
                myAmount += amount;
                if (myCapacity < myAmount)
                {
                    myAmount = myCapacity;
                }
            }
        }

         int HowMuchCanILoad()
        {
            return myCapacity - myAmount;
        }

        /// <summary>
        /// Nullázza a tárolt mennyiséget.
        /// </summary>
        /// <returns>Az előzőleg tárolt mennyiség</returns>
        public int ReturnAmount()
        {
            int value = myAmount;
            ResetAmount();
            return value;
        }

        public bool OrderABuild(Building building, Point place)
        {
            bool couldBuild = true;
            if (Engine.ThePlayer.Resources.HasEnoughToReduceBy(building.Costs))
            {
                couldBuild = Engine.StartBuilding(building, place);
                if (couldBuild)
                {
                    NextOrder = Order.Build;
                    MoveTarget = place;
                    Engine.ThePlayer.Resources.ReduceBy(building.Costs);
                }
            }
            
            return couldBuild;
        }

        /// <summary>
        /// Gather the targeted resource if it is next to it.
        /// </summary>
        public void Gather( Point pos)
        {
            if (MoveTarget != null && pos.DistanceFrom(MoveTarget) == 1)
            {
                foreach (MapObject mo in Engine.Map.GetCellAt(MoveTarget).MapObjects)
                {
                    if (mo is Resource)
                    {

                        if (mo is Animal)
                        {
                            Animal animal = (Animal)mo;
                            if (animal.Dead)
                            {
                                if ((CarriedResourceType != ResourceType.Food))
                                {
                                    ResetAmount();
                                    CarriedResourceType = ResourceType.Food;
                                }
                                Resource resource = (Resource)mo;
                                int amount = HowMuchCanILoad();
                                if (amount > 3) amount = 3;
                                amount = resource.gatherAmount(amount);
                                AddAmount(amount);
                                
                                break;
                            }
                        }
                        else
                        {
                            if (mo.GetType() == typeof(Mine) && CarriedResourceType != ResourceType.Gold)
                            {
                                ResetAmount();
                                CarriedResourceType = ResourceType.Gold;
                            }
                            if (mo.GetType() == typeof(Tree) && CarriedResourceType != ResourceType.Wood)
                            {
                                ResetAmount();
                                CarriedResourceType = ResourceType.Wood;
                            }
                            if ((mo.GetType() == typeof(Farm))
                                  &&
                                 (CarriedResourceType != ResourceType.Food))
                            {
                                ResetAmount();
                                CarriedResourceType = ResourceType.Food;
                            }



                            Resource resource = (Resource)mo;
                            int amount = HowMuchCanILoad();
                            if (amount > 3) amount = 3;
                            amount = resource.gatherAmount(amount);
                            AddAmount(amount);


                            break;
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Returns the resource if it is next to the targeted Main Hall.
        /// </summary>
        private void ReturnResource(Point pos)
        {
            if (MoveTarget != null && pos.DistanceFrom(MoveTarget) == 1)
            {
                foreach (MapObject mo in Engine.Map.GetCellAt(MoveTarget).MapObjects)
                {
                    if (mo.GetType() == typeof(MainHall))
                    {
                        switch (CarriedResourceType)
                        {
                            case ResourceType.Wood: Engine.ThePlayer.Resources.Wood += ReturnAmount(); break;
                            case ResourceType.Food: Engine.ThePlayer.Resources.Food += ReturnAmount(); break;
                            case ResourceType.Gold: Engine.ThePlayer.Resources.Gold += ReturnAmount(); break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Builds a building if there is one under him.
        /// </summary>
        /// <param name="worker"></param>
        private void Build(Point pos)
        {
            if (MoveTarget != null && pos.DistanceFrom(MoveTarget) == 1)
            {
                foreach (MapObject mo in Engine.Map.GetCellAt(MoveTarget).MapObjects)
                {
                    if (mo is Building)
                    {
                        Building b = (Building)mo;
                        if (b.IsUnderConstruction)
                        {
                            b.beBuilt();
                        }
                        else
                        {
                            myNextOrder = Order.None;
                        }
                        
                    }
                }
            }
        }

        private int myCapacity;
        private int myAmount;
        private ResourceType myCarriedResourceType;
        private Order myNextOrder;

    }
}
