using EvoCraft.Common.Map;
using EvoCraft.Common.MapObjects;
using EvoCraft.Common.MapObjects.PlayerControlled.Units;

namespace EvoCraft.Core.MapObjects.PlayerControlled.Units
{
    public static class GunManExtension
    {
        public static void Update(this GunMan gunMan, Point pos)
        {

            if (gunMan.ShootCooldown <= gunMan.ActualShootCooldown)
            {
                gunMan.ActualShootCooldown = 0;
                gunMan.Target = Engine.GetClosestAggressiveAnimalInRange(pos, gunMan.SightRange - 1);

                if (gunMan.Target != null)
                {
                    Engine.Map.GetCellAt(pos.x, pos.y).MapObjects.Add(new Bullet(pos, gunMan.Target, gunMan.Damage, 10));
                }
            }
            else
            {
                gunMan.ActualShootCooldown++;
            }
            if (gunMan.slowNum > 0)
            {
                gunMan.Move(pos);
                gunMan.slowNum = 0;
            }
            else
            {
                gunMan.slowNum++;
            }

        }
    }
}
