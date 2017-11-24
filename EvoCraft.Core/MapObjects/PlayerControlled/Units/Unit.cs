using EvoCraft.Common;
using System;
using System.Collections.Generic;

namespace EvoCraft.Core
{
    [Serializable]
    public abstract class Unit : PlayerControlled
    {
        
        
        /// <summary>
        /// Ide akar menni az egység. Ez lett neki parancsba adva, vagy felismerte magának a nyersanyag hordása közben.
        /// </summary>
        public Point MoveTarget { get; set; }

        /// <summary>
        /// The Z index of all untis. Helps the drawing engine determine which what order to draw the objects.
        /// </summary>
        private static readonly int UnitZIndex = 4;

        /// <summary>
        /// Visszaadja, hogy az egység mennyit képpes sebezni.
        /// </summary>
        public int Damage
        {
            get
            {
                return myDamage;
            }
        }

        /// <summary>
        /// Visszaadja az egység sebességét.
        /// </summary>
        protected internal int Speed
        {
            get
            {
                return mySpeed;
            }
        }

        /// <summary>
        /// képzéshez szükséges idő.
        /// </summary>
        protected internal int TrainingTime { get; set; }


        public bool AlertMode = true;

        /// <summary>
        /// Egy egység létrehozását segítő konstruktor.
        /// </summary>
        /// <param name="fullhealth">Az egység maximális életerje.</param>
        /// <param name="fullarmor">Az egység maximális páncélja.</param>
        /// <param name="damage">Az egység által bevihető sebzés.</param>
        /// <param name="speed">Az egység sebessége.</param>
        /// <param name="cost">Az egység ára.</param>
        protected internal Unit(
            string Label, 
            int fullhealth, 
            int damage, 
            int speed,
            int trainingTime,
            int PlayerId, 
            Point initialTarget,
            List<Actions> possibleActions,
            int SightRange,
            ResourceSet Costs) 
        : base(PlayerId, fullhealth, fullhealth, SightRange, BlockType.BlockOtherBlock, UnitZIndex, Label, Costs, possibleActions, Actions.None)
        {
            myDamage = damage;
            mySpeed = speed;
            TrainingTime = trainingTime;
            MoveTarget = initialTarget;
        }

        public override void Update()
        {
            bool found;
            Point pos = Engine.GetMapObjectPosition(this, out found);
            if (found)
            {
                Move(pos);
                Attack(pos);
            }
        }
        
        public void Move(Point pos)
        {
            if (MoveTarget != null)
            {
                Engine.MoveMapObject(this, Engine.GetDirectionForPathToTargetPosition(pos, MoveTarget), pos);
            }
        }

        /// <summary>
        /// Attack the target if next to it.
        /// </summary>
        internal void Attack(Point pos)
        {
            if (MoveTarget != null && pos.DistanceFrom(MoveTarget) == 1)
            {
                foreach (MapObject mo in Engine.Map.GetCellAt(MoveTarget).MapObjects)
                {
                    if (mo.GetType().IsSubclassOf(typeof(Animal)))
                    {
                        Animal a = (Animal)mo;
                        a.TakeDamage(Damage);
                    }
                }
            }
        }
        
        private int myDamage;
        private int mySpeed;
        private int myTrainingTime;
        
    }
}
