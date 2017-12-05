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
        public static int WorkerFullHealth = 100;
        public static int WorkerDamage = 10;
        public static int WorkerSpeed = 10;
        public static int WorkerCapacity = 30;
        public static readonly int FoodCost = 100;
        public static readonly int WoodCost = 50;
        public static readonly int GoldCost = 0;
        public static int WorkerTrainingTime = 20;
        public static List<Actions> WorkerPossibleActions = new List<Actions> { Actions.Stop, Actions.BuildBarracs, Actions.BuildWall, Actions.BuildMainHall, Actions.BuildTower, Actions.BuildMedicalTent, Actions.BuildFarm};

        /// <summary>
        /// A munkás maximális tárolási kapacitása
        /// </summary>
        public int Capacity
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
            set
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
        
        public void ResetAmount()
        {
            myAmount = 0;
        }

        /// <summary>
        /// Adds the amount if it can. If it can't it simply ignores the amount.
        /// </summary>
        public void AddAmount(int amount)
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

        public int HowMuchCanILoad()
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

        public int myCapacity;
        public int myAmount;
        public ResourceType myCarriedResourceType;
        public Order myNextOrder;
    }
}
