using EvoCraft.Common.Map;
using EvoCraft.Common.MapObjects;
using EvoCraft.Common.MapObjects.PlayerControlled.Buildings;
using EvoCraft.Common.MapObjects.PlayerControlled.Units;
using EvoCraft.Common.MapObjects.Resources;
using EvoCraft.Common.MapObjects.Resources.Animals;

namespace EvoCraft.Core.MapObjects.PlayerControlled.Units
{
    public static class WorkerExtension
    {
        public static void Update(this Worker worker, Point pos)
        {
            if (worker.Amount == worker.Capacity && (worker.myNextOrder == Order.GoToFood || worker.myNextOrder == Order.GoToWood || worker.myNextOrder == Order.GoToGold))
            {
                worker.myNextOrder = Order.ReturnResource;
                if (worker.RememberedReturnTarget == null)
                {
                    worker.RememberedReturnTarget = Engine.SearchClosestMainHall(pos);
                }
            }
            if (worker.Amount == 0 && worker.myNextOrder == Order.ReturnResource)
            {
                switch (worker.CarriedResourceType)
                {
                    case ResourceType.Gold: worker.myNextOrder = Order.GoToGold; break;
                    case ResourceType.Wood: worker.myNextOrder = Order.GoToWood; break;
                    case ResourceType.Food: worker.myNextOrder = Order.GoToFood; break;
                }
            }
            switch (worker.myNextOrder)
            {
                case Order.ReturnResource:
                    worker.MoveTarget = worker.RememberedReturnTarget; break;
                case Order.Hunt:
                    bool isDead;
                    worker.MoveTarget = Engine.SearchClosestPeacefulAnimalInRange(pos, worker.SightRange, out isDead);
                    if (isDead)
                    {
                        worker.NextOrder = Order.GoToFood;
                        worker.RememberedGatherTarget = worker.MoveTarget;
                    }
                    break;

                case Order.GoToFood:
                    bool ResourceIsNotValid = true;
                    foreach (MapObject mo in Engine.Map.GetCellAt(worker.RememberedGatherTarget).MapObjects)
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
                        worker.myNextOrder = Order.Hunt;

                        worker.MoveTarget = Engine.SearchClosestPeacefulAnimalInRange(pos, worker.SightRange, out isDead);
                        if (isDead)
                        {
                            worker.NextOrder = Order.GoToFood;
                            worker.RememberedGatherTarget = worker.MoveTarget;
                        }
                    }
                    worker.MoveTarget = worker.RememberedGatherTarget;
                    break;
                case Order.GoToGold:
                    ResourceIsNotValid = true;
                    foreach (MapObject mo in Engine.Map.GetCellAt(worker.RememberedGatherTarget).MapObjects)
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
                        worker.RememberedGatherTarget = Engine.SearchClosestResourceInRange(pos, ResourceType.Gold, worker.SightRange);
                    }
                    worker.MoveTarget = worker.RememberedGatherTarget;
                    break;
                case Order.GoToWood:
                    ResourceIsNotValid = true;
                    foreach (MapObject mo in Engine.Map.GetCellAt(worker.RememberedGatherTarget).MapObjects)
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
                        worker.RememberedGatherTarget = Engine.SearchClosestResourceInRange(pos, ResourceType.Wood, worker.SightRange);
                    }
                    worker.MoveTarget = worker.RememberedGatherTarget;
                    break;
            }


            if (worker.myNextOrder == Order.GoToFood || worker.myNextOrder == Order.GoToGold || worker.myNextOrder == Order.GoToWood)
            {
                worker.Gather(pos);
            }
            if (worker.myNextOrder == Order.ReturnResource)
            {
                worker.ReturnResource(pos);
            }
            if (worker.myNextOrder == Order.Build)
            {
                worker.Build(pos);
            }
            if (worker.myNextOrder == Order.Hunt)
            {
                worker.Attack(pos);
            }

            worker.Move(pos);
        }

        public static bool OrderABuild(this Worker worker, Building building, Point place)
        {
            bool couldBuild = true;
            if (Engine.ThePlayer.Resources.HasEnoughToReduceBy(building.Costs))
            {
                couldBuild = Engine.StartBuilding(building, place);
                if (couldBuild)
                {
                    worker.NextOrder = Order.Build;
                    worker.MoveTarget = place;
                    Engine.ThePlayer.Resources.ReduceBy(building.Costs);
                }
            }

            return couldBuild;
        }

        /// <summary>
        /// Gather the targeted resource if it is next to it.
        /// </summary>
        public static void Gather(this Worker worker, Point pos)
        {
            if (worker.MoveTarget != null && pos.DistanceFrom(worker.MoveTarget) == 1)
            {
                foreach (MapObject mo in Engine.Map.GetCellAt(worker.MoveTarget).MapObjects)
                {
                    if (mo is Resource)
                    {

                        if (mo is Animal)
                        {
                            Animal animal = (Animal)mo;
                            if (animal.Dead)
                            {
                                if ((worker.CarriedResourceType != ResourceType.Food))
                                {
                                    worker.ResetAmount();
                                    worker.CarriedResourceType = ResourceType.Food;
                                }
                                Resource resource = (Resource)mo;
                                int amount = worker.HowMuchCanILoad();
                                if (amount > 3) amount = 3;
                                amount = resource.gatherAmount(amount);
                                worker.AddAmount(amount);

                                break;
                            }
                        }
                        else
                        {
                            if (mo.GetType() == typeof(Mine) && worker.CarriedResourceType != ResourceType.Gold)
                            {
                                worker.ResetAmount();
                                worker.CarriedResourceType = ResourceType.Gold;
                            }
                            if (mo.GetType() == typeof(Tree) && worker.CarriedResourceType != ResourceType.Wood)
                            {
                                worker.ResetAmount();
                                worker.CarriedResourceType = ResourceType.Wood;
                            }
                            if ((mo.GetType() == typeof(Farm))
                                  &&
                                 (worker.CarriedResourceType != ResourceType.Food))
                            {
                                worker.ResetAmount();
                                worker.CarriedResourceType = ResourceType.Food;
                            }



                            Resource resource = (Resource)mo;
                            int amount = worker.HowMuchCanILoad();
                            if (amount > 3) amount = 3;
                            amount = resource.gatherAmount(amount);
                            worker.AddAmount(amount);


                            break;
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Returns the resource if it is next to the targeted Main Hall.
        /// </summary>
        public static void ReturnResource(this Worker worker, Point pos)
        {
            if (worker.MoveTarget != null && pos.DistanceFrom(worker.MoveTarget) == 1)
            {
                foreach (MapObject mo in Engine.Map.GetCellAt(worker.MoveTarget).MapObjects)
                {
                    if (mo.GetType() == typeof(MainHall))
                    {
                        switch (worker.CarriedResourceType)
                        {
                            case ResourceType.Wood: Engine.ThePlayer.Resources.Wood += worker.ReturnAmount(); break;
                            case ResourceType.Food: Engine.ThePlayer.Resources.Food += worker.ReturnAmount(); break;
                            case ResourceType.Gold: Engine.ThePlayer.Resources.Gold += worker.ReturnAmount(); break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Builds a building if there is one under him.
        /// </summary>
        /// <param name="worker"></param>
        public static void Build(this Worker worker, Point pos)
        {
            if (worker.MoveTarget != null && pos.DistanceFrom(worker.MoveTarget) == 1)
            {
                foreach (MapObject mo in Engine.Map.GetCellAt(worker.MoveTarget).MapObjects)
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
                            worker.myNextOrder = Order.None;
                        }

                    }
                }
            }
        }
    }
}