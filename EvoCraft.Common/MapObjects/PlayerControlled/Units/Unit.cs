using EvoCraft.Common.Map;
using System;
using System.Collections.Generic;

namespace EvoCraft.Common.MapObjects.PlayerControlled.Units
{
    [Serializable]
    public abstract class Unit : PlayerControlledClass
    {       
        /// <summary>
        /// Ide akar menni az egység. Ez lett neki parancsba adva, vagy felismerte magának a nyersanyag hordása közben.
        /// </summary>
        public Point MoveTarget { get; set; }

        /// <summary>
        /// The Z index of all untis. Helps the drawing engine determine which what order to draw the objects.
        /// </summary>
        public static readonly int UnitZIndex = 4;

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
         int Speed
        {
            get
            {
                return mySpeed;
            }
        }

        /// <summary>
        /// képzéshez szükséges idő.
        /// </summary>
        public int TrainingTime { get; set; }


        public bool AlertMode = true;

        /// <summary>
        /// Egy egység létrehozását segítő konstruktor.
        /// </summary>
        /// <param name="fullhealth">Az egység maximális életerje.</param>
        /// <param name="fullarmor">Az egység maximális páncélja.</param>
        /// <param name="damage">Az egység által bevihető sebzés.</param>
        /// <param name="speed">Az egység sebessége.</param>
        /// <param name="cost">Az egység ára.</param>
         public Unit(
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
   
        public int myDamage;
        public int mySpeed;
        public int myTrainingTime;
        
    }
}
