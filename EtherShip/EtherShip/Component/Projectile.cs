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
        private Vector2 translation;

        public Projectile(GameObject obj, float speed, int damage, GameObject target) : base(obj)
        {
            this.speed = speed;
            this.Damage = damage;
            this.target = target;
        }

        public void Update(GameTime gameTime)
        {
            Move(gameTime);
            OBJCollision();
        }

        public void Move(GameTime gameTime)
        {
            translation = Vector2.Normalize(target.position - obj.position) * speed / (float)gameTime.ElapsedGameTime.Milliseconds;
            obj.position += translation;
        }

        /// <summary>
        /// Checks for collision and acts if there is a collision.
        /// </summary>
        public void OBJCollision()
        {
            //Checks if this collides with another gameobject.
            if ((obj.position - target.position).Length() < 100)
            {
                if (CollisionCheck.Check(obj.GetComponent<CollisionCircle>().edges, obj.position + (translation / 2), target.GetComponent<CollisionCircle>().edges, target.position))
                {
                    if(target.GetComponent<Enemy>() != null)
                        target.GetComponent<Enemy>().Health -= Damage;
                    if(target.GetComponent<Whale>() != null)
                        target.GetComponent<Whale>().Health -= Damage;


                    GameWorld.Instance.gameObjectPool.RemoveActive.Add(obj);
                }
            }
        }
    }
}
