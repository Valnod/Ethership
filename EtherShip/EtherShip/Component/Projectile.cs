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
    class Projectile : Component, IUpdateable
    {
        public float speed;
        public int Damage { get; }
        public GameObject target;

        public Projectile(GameObject obj, float speed, int damage, GameObject target) : base(obj)
        {
            this.speed = speed;
            this.Damage = damage;
            this.target = target;
        }

        public void Update(GameTime gameTime)
        {
            Move(gameTime);
        }

        public void Move(GameTime gameTime)
        {

            Vector2 PM = Vector2.Normalize(obj.position - target.position) * speed;
            obj.position += PM;

        }
    }
}
