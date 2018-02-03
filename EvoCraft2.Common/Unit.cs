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
        private static int unitID; 
        public Coordinate Position;
        public Coordinate Target;
        public int HP;
        public int MaxHP;
        public int Team;

        public int UnitID
        {
            get { return unitID; }
        }

        private static void GenerateUnitID()
        {
            unitID = LastID++;
        }

        public Unit(Coordinate position, int maxHP, int team)
        {
            GenerateUnitID();
            Position = position;
            MaxHP = maxHP;
            Team = team;
        }

        
    }
}
