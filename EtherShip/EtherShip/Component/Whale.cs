using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherShip
{
    class Whale : Component, IUpdateable
    {
        public Vector2 target;
        public Vector2 direction;
        public int value;
        public int health;
        public float speed;
        private int xSpawn;
        private int ySpawn;
        private Random rnd;

        public Whale(GameObject obj, Vector2 target, Vector2 direction, int value, int health, float speed) : base(obj)
        {
            this.target = target;
            this.direction = direction;
            this.value = value;
            this.health = health;
            this.speed = speed;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Move()
        {

        }
    }
}
