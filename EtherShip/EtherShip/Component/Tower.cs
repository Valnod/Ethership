using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherShip
{
    class Tower : Component, IUpdateable
    {
        private float gStrength;
        private float gRange;

        public Tower(GameObject obj, float gStrength, float gRange) : base(obj)
        {
            this.gStrength = gStrength;
            this.gRange = gRange;
        }

        private void Shoot()
        {

            ////Vector2 SP = GameWorld.Instance.gameObjectPool.ActiveEnemyList

            foreach (object CEL in (GameWorld.Instance.gameObjectPool.ActiveEnemyList))
            {



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
