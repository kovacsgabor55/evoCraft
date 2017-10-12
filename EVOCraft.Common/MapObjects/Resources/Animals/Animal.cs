using System;

namespace EvoCraft.Common
{
    /// <summary>
    /// Meant to represent animals
    /// </summary>
    abstract public class Animal: Resource
    {
        /// <summary>
        /// The maximal Health Points
        /// </summary>
        public int MaximalHealthPoints { get; set; }
        /// <summary>
        /// The actual Health Points
        /// </summary>
        public int ActualHealthPoints { get; set; }
        protected static Random randomNum = new Random();
        public bool Dead;

        internal Animal(string Label, int maxCapacity, int maximalHealthPoints) : base(Label, maxCapacity, BlockType.BlockOtherBlock)
        {
            Dead = false;
            Type = ResourceType.Food;
            MaximalHealthPoints = maximalHealthPoints;
            ActualHealthPoints = maximalHealthPoints;
        }

        public override void Update()
        {
            if (!Dead)
            {
                Move();
            }
            else
            {
                Decay();
                if (Capacity <= 0)
                {
                    Engine.DestroyMapObject(this);
                }
            }
        }


        private int deccayDelay = 0;
        protected void Decay()
        {
            Capacity--;
        }

        internal virtual void Move()
        {
            int direction = randomNum.Next(4);
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
        
        internal void TakeDamage(int damage)
        {
            if (!Dead)
            {
                ActualHealthPoints -= damage;
                if (ActualHealthPoints <= 0)
                {
                    ActualHealthPoints = 0;
                    Dead = true;
                }
            }
        }
    }
}
