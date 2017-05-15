using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace EtherShip
{
    class Enemy : Component, IUpdateable
    {
        public int health;
        public float speed;
        public Vector2 direction;
        public int value;

        public Enemy(GameObject obj, int health, float speed, int value, Vector2 direction) : base(obj)
        {
            this.health = health;
            this.speed = speed;
            this.direction = direction;
            this.value = value;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Move(GameTime gameTime)
        {

        }
    }
}
