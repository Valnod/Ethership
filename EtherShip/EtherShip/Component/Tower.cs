using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherShip
{
    public class Tower : Component, IUpdateable
    {
        private float gStrength;
        private float gRange;
        protected float radius;

        public float Radius
        {
            get { return radius; }
        }

        public Tower(GameObject obj, float gStrength, float gRange) : base(obj)
        {
            this.gStrength = gStrength;
            this.gRange = gRange;
            radius = 1000;
        }

        public bool IsInRange(Vector2 position)
        {
            if (Vector2.Distance(this.obj.position, position) <= radius)
                return true;

            return false;
        }
        private void Shoot()
        {
            float length = 100000;
            //xLoc is closest x position
            int xStart = 0;
            //yLoc is closest y position
            int yStart = 0;
            //xEnd is closet position to TOwer
            int xEnd = 0;
            //yEnd is closet position to TOwer
            int yEnd = 0;

          
            for (int x = 0; x < GameWorld.Instance.gameObjectPool.ActiveEnemyList.Count; x++)
            {
                GameObject target = GameWorld.Instance.gameObjectPool.ActiveEnemyList[x];

                if (IsInRange(target.position) &&(this.obj.position - target.position).Length() < length)

                {
                    length = (this.obj.position - GameWorld.Instance.gameObjectPool.ActiveEnemyList[x].position).Length();
                    new Projectile(obj, 10f, 10, target);

                }
            }

    
            

        }

        public Vector2 Gravity(Vector2 position, float maxSpeed)
        {
            float distance = (position - this.obj.position).Length();
            if (distance > gRange)
                return Vector2.Zero;
            Vector2 elkg = position - this.obj.position;
            Vector2 gravDirection = Vector2.Normalize(this.obj.position - position);

            //Calculates the strength of the gravitation at the given point
            //float gravStrength = 10 + gStrength * (float)Math.Exp(distance);
            float gravStrength = gStrength / (distance * distance);
            //Ensures the gravity wont keep the given object indefently
            if (gravStrength > maxSpeed*3)
                gravStrength = maxSpeed*3;
            if (gravStrength < 0)
                gravStrength = 0;
            Vector2 positionalGravStrength = gravDirection * gravStrength;

            return positionalGravStrength;
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
