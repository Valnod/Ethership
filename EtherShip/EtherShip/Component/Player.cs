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
    class Player : Component, IUpdateable
    {
        public Vector2 direction;
        public int health;
        public bool antiGravity;

        public Player(GameObject obj, Vector2 direction, int health, bool antiGravity) : base(obj)
        {
            this.direction = direction;
            this.health = health;
            this.antiGravity = antiGravity;
        }

        public void AntiGravity()
        {

        }
        
        public void Update(GameTime gameTime)
        {

        }

        public void Move(GameTime gameTime)
        {

        }
    }
}
