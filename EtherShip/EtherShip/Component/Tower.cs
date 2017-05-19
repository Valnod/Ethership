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

        public Vector2 Gravity(Vector2 position)
        {
            return position;
        }
        
        public void Update(GameTime gameTime)
        {

        }
    }
}
