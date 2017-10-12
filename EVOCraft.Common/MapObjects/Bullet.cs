﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoCraft.Common
{
    public class Bullet : MapObject
    {
        private int Damage;
        private int LifeTime;
        private Point Target;

        public Bullet(Point from, Point to, int Damage, int lifeTime) : base (BlockType.NoBlock, 0, "Bullet")
        {
            this.Damage = Damage;
            this.LifeTime = lifeTime;
        }

        public override void Update()
        {
            LifeTime--;
            bool found;
            Point pos = Engine.GetMapObjectPosition(this, out found);
            if (found)
            {
                Point p = Engine.GetClosestAggressiveAnimalInRange(pos, 10);
                if (p != null)
                {
                    Target = p;
                }
                else
                {
                    Target = new Point(pos.x+1,pos.y+1);
                }
                Move(pos);
                Move(pos);
                Hit(pos);
                if (LifeTime <= 0)
                {
                    Engine.DestroyMapObject(this, pos);
                }
            }
            
        }

        private void Hit(Point pos)
        {
            bool hit = false;
            foreach (MapObject mo in Engine.Map.GetCellAt(pos).MapObjects)
            {
                if (mo.GetType().IsSubclassOf(typeof(AggressiveAnimal)))
                {
                    AggressiveAnimal a = (AggressiveAnimal)mo;
                    a.TakeDamage(Damage);
                    hit = true;
                }
            }
            if (hit){
                Engine.DestroyMapObject(this, pos);
            }
            
        }

        public void Move(Point pos)
        {
            if (Target != null)
            {
                Engine.MoveMapObject(this, Engine.GetDirectionForPathToTargetPosition(pos, Target, BlockType), pos);
            }
        }
    }
}
