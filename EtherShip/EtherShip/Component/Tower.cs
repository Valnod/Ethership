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
        private GameObject target;
        private float timer;
        private float shootCooldown = 1000;
        private bool canShoot;
        protected float radius;

        public float Radius
        {
            get { return radius; }
        }

        public Tower(GameObject obj, float gStrength, float gRange) : base(obj)
        {
            this.gStrength = gStrength;
            this.gRange = gRange;
            radius = 300;
            canShoot = true;
        }

        public bool IsInRange(Vector2 position)
        {
            if (Vector2.Distance(this.obj.position, position) <= radius)
                return true;

            return false;
        }
        private void Shoot()
        {
            float length = 400;
            //xLoc is closest x position
            int xStart = 0;
            //yLoc is closest y position
            int yStart = 0;
            //xEnd is closet position to TOwer
            int xEnd = 0;
            //yEnd is closet position to TOwer
            int yEnd = 0;
            //Target
            target = null;

            for (int x = 0; x < GameWorld.Instance.gameObjectPool.ActiveEnemyList.Count; x++)
            {
                //her kom en fejl med enemy out of range, et tårn havde skudt men fjenden forsvandt så dens afstand var uden for boundary
                if (IsInRange(GameWorld.Instance.gameObjectPool.ActiveEnemyList[x].position) &&
                    ((this.obj.position - GameWorld.Instance.gameObjectPool.ActiveEnemyList[x].position).Length() < length))
                {
                    length = (this.obj.position - GameWorld.Instance.gameObjectPool.ActiveEnemyList[x].position).Length();
                    target = GameWorld.Instance.gameObjectPool.ActiveEnemyList[x];
                }
            }
            for (int x = 0; x < GameWorld.Instance.gameObjectPool.ActiveWhaleList.Count; x++)
            {
                if (IsInRange(GameWorld.Instance.gameObjectPool.ActiveWhaleList[x].position) &&
                    ((this.obj.position - GameWorld.Instance.gameObjectPool.ActiveWhaleList[x].position).Length() < length))
                {
                    length = (this.obj.position - GameWorld.Instance.gameObjectPool.ActiveWhaleList[x].position).Length();
                    target = GameWorld.Instance.gameObjectPool.ActiveWhaleList[x];
                }
            }
            if (target != null)
            {
                GameWorld.Instance.gameObjectPool.CreateProjectile(obj.position, target);
                canShoot = false;
                timer = 0;
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
            if (gravStrength > maxSpeed*2)
                gravStrength = maxSpeed*2;
            if (gravStrength < 0)
                gravStrength = 0;
            Vector2 positionalGravStrength = gravDirection * gravStrength;

            return positionalGravStrength;
        }

        public void Update(GameTime gameTime)
        {
            if(!canShoot)
                timer += (float)gameTime.ElapsedGameTime.Milliseconds;
            if (canShoot)
                Shoot();
            else if (timer > shootCooldown)
                canShoot = true;
            else
                timer += 1;
        }
    }
}
