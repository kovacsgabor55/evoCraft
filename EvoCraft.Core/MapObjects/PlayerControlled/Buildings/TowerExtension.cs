using EvoCraft.Common.Map;
using EvoCraft.Common.MapObjects;
using EvoCraft.Common.MapObjects.PlayerControlled.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoCraft.Core.MapObjects.PlayerControlled.Buildings
{
    public static class TowerExtension
    {
        public static void Update(this Tower tower, Point pos)
        {
            if (tower.IsUnderConstruction && tower.BuildTime == 0)
            {
                tower.IsUnderConstruction = false;
                tower.FinishBuilding();
            }
            else
            {
                if (tower.ShootCooldown <= tower.ActualShootCooldown)
                {
                    tower.ActualShootCooldown = 0;
                    tower.Target = Engine.GetClosestAggressiveAnimalInRange(pos, tower.SightRange - 1);

                    if (tower.Target != null)
                    {
                        Engine.Map.GetCellAt(pos.x, pos.y).MapObjects.Add(new Bullet(pos, tower.Target, 50, 10));
                    }
                }
                else
                {
                    tower.ActualShootCooldown++;
                }
            }
        }

        public static void FinishBuilding(this Tower tower)
        {
            tower.ActualHealthPoints = tower.MaximalHealthPoints;
            tower.SightRange = 11;
        }
    }
}
