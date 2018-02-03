using EvoCraft.Common.Map;
using EvoCraft.Common.MapObjects;
using EvoCraft.Common.MapObjects.PlayerControlled.Units;

namespace EvoCraft.Core.MapObjects.PlayerControlled.Units
{
    public static class DoctorExtension
    {
        public static void Update(this Doctor doctor, Point pos)
        {
            if (doctor.AlertMode)
            {
                Point p = Engine.GetClosestInjuredUnitInRange(pos, doctor.SightRange);
                if (p != null)
                {
                    doctor.MoveTarget = p;
                    doctor.WasGoingAfterAUnit = true;
                }
                else
                {
                    if (doctor.WasGoingAfterAUnit)
                    {
                        doctor.MoveTarget = null;
                        doctor.WasGoingAfterAUnit = false;
                    }
                }
            }
            Heal(doctor, pos);
            doctor.Move(pos);

        }

        /// <summary>
        /// Heal the target if next to it.
        /// </summary>
        public static void Heal(this Doctor doctor, Point pos)
        {
            if (doctor.MoveTarget != null && pos.DistanceFrom(doctor.MoveTarget) == 1)
            {
                foreach (MapObject mo in Engine.Map.GetCellAt(doctor.MoveTarget).MapObjects)
                {
                    if (mo is Unit)
                    {
                        Unit a = (Unit)mo;
                        a.TakeHealing(doctor.Damage);
                    }
                }
            }
        }
    }
}
