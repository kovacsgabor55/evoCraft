using EvoCraft.Common.Map;
using EvoCraft.Common.MapObjects;
using EvoCraft.Common.MapObjects.PlayerControlled;
using EvoCraft.Common.MapObjects.Resources.Animals;
using EvoCraft.Core.MapObjects.PlayerControlled;
using System;
using System.Linq;
using System.Reflection;

namespace EvoCraft.Core.MapObjects.Resources.Animals
{
    public static class AggressiveAnimalExtension
    {
        public static void Update(this AggressiveAnimal aggressiveAnimal, Point pos)
        {
                if (!aggressiveAnimal.Dead)
                {
                    Attack(aggressiveAnimal, pos);

                //TODO

                //Type myTypeObj = aggressiveAnimal.GetType();

                //MethodInfo myMethodInfo = typeof(AggressiveAnimalExtension).GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).First(m => m.IsGenericMethod && m.Name == nameof(AggressiveAnimalExtension.MoveWithPathfinding));


                //MethodInfo myMethodInfo = myTypeObj.GetMethod("MoveWithPathfinding");
                //myMethodInfo.Invoke(aggressiveAnimal, new object[] { pos });


                if (aggressiveAnimal is Chupacabra)
                {
                    ChupacabraExtension.MoveWithPathfinding((Chupacabra)aggressiveAnimal, pos);
                }
                if (aggressiveAnimal is Boss)
                {
                    BossExtension.MoveWithPathfinding((Boss)aggressiveAnimal, pos);
                }

                //MoveWithPathfinding(aggressiveAnimal, pos);
            }
                else
                {
                    aggressiveAnimal.Decay();
                    if (aggressiveAnimal.Capacity <= 0)
                    {
                        Engine.DestroyMapObject(aggressiveAnimal);
                    }
                }
        }

        /// <summary>
        /// Attack the target if next to it.
        /// </summary>
        public static void Attack(this AggressiveAnimal aggressiveAnimal, Point pos)
        {
            if (aggressiveAnimal.MoveTarget != null && pos.DistanceFrom(aggressiveAnimal.MoveTarget) == 1)
            {
                PlayerControlledClass playerctrl = null;
                foreach (MapObject mo in Engine.Map.GetCellAt(aggressiveAnimal.MoveTarget).MapObjects)
                {
                    if (mo is PlayerControlledClass)
                    {
                        playerctrl = (PlayerControlledClass)mo;
                        playerctrl.TakeDamage(aggressiveAnimal.Damage);
                        break;
                    }
                }
                if (playerctrl != null && playerctrl.ActualHealthPoints <= 0)
                {
                    Engine.DestroyMapObject(playerctrl, aggressiveAnimal.MoveTarget);
                }
            }
        }

        public static void MoveWithPathfinding(this AggressiveAnimal aggressiveAnimal, Point pos)
        {
            if (aggressiveAnimal.MoveTarget != null)
            {
                Engine.MoveMapObject(aggressiveAnimal, Engine.GetDirectionForPathToTargetPosition(pos, aggressiveAnimal.MoveTarget), pos);
            }
        }
    }
}
