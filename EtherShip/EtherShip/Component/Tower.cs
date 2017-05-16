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

        public Vector2 Gravity(Vector2 position)
        {
            return position;
        }
        
        public void Update(GameTime gameTime)
        {

        }
    }
}
