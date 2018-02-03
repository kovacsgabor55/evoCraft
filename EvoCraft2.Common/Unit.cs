using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoCraft2.Common
{
    public class Unit : MapObject
    {
        private static int LastID = 0;
        private int unitID; 

        public Coordinate Target;
        public int HP;
        public int MaxHP;
        public int Team;

        public int UnitID
        {
            get { return unitID; }
        }

        private static int GenerateUnitID()
        {
            return LastID++;
        }

        public Unit(Coordinate position, int maxHP, int team)
        {
            unitID = GenerateUnitID();
            Position = position;
            MaxHP = maxHP;
            Team = team;
        }

        public override string ToString()
        {
            return "ID: " + UnitID + ", Team: " + Team + ", Pos: " + Position + ", Target: " + Target + "HP: " + HP;
        }
    }
}
