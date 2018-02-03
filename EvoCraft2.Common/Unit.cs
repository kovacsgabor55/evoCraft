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

        public int Team;
        public Coordinate Target;
        public int HP;
        public int MaxHP;
        public int Damage;
        

        public int UnitID
        {
            get { return unitID; }
        }

        private static int GenerateUnitID()
        {
            return LastID++;
        }

        public Unit(int team, Coordinate position, int maxHP, int damage)
        {
            unitID = GenerateUnitID();
            Team = team;
            Position = position;
            MaxHP = maxHP;
            HP = maxHP;
            Damage = damage;
        }

        public override string ToString()
        {
            return "ID: " + UnitID + ", Team: " + Team + ", Pos: " + Position + ", Target: " + Target + "HP: " + HP + ", Damage: " + Damage;
        }
    }
}
