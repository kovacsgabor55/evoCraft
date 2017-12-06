using EvoCraft.Common.Map;
using EvoCraft.Common.MapObjects.Resources.Animals;
using System;

namespace EvoCraft.Core.MapObjects.Resources.Animals
{
    public static class AnimalExtension
    {
        public static void Update(this Animal animal)
        {
            if (!animal.Dead)
            {
                Move(animal);
            }
            else
            {
                animal.Decay();
                if (animal.Capacity <= 0)
                {
                    Engine.DestroyMapObject(animal);
                }
            }
        }

        public static void Move(this Animal animal)
        {
            Random rnd = new Random();
            int direction = rnd.Next(4);
            switch (direction)
            {
                case 0:
                    Engine.MoveMapObject(animal, Direction.Up);
                    break;
                case 1:
                    Engine.MoveMapObject(animal, Direction.Down);
                    break;
                case 2:
                    Engine.MoveMapObject(animal, Direction.Left);
                    break;
                case 3:
                    Engine.MoveMapObject(animal, Direction.Right);
                    break;
            }
        }

        public static void TakeDamage(this Animal animal, int damage)
        {
            if (!animal.Dead)
            {
                animal.ActualHealthPoints -= damage;
                if (animal.ActualHealthPoints <= 0)
                {
                    animal.ActualHealthPoints = 0;
                    animal.Dead = true;
                }
            }
        }

        public static void Decay(this Animal animal)
        {
            animal.Capacity--;
        }
    }
}
