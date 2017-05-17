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
    class Player : Component, IUpdateable, ICollidable
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
            OBJCollision();
        }

        public void Move(GameTime gameTime)
        {

        }

        public void OBJCollision()
        {
            foreach(GameObject go in GameWorld.Instance.gameObjectPool.CollisionListForPlayer())
            {
                //Checks the distance to the objects, and only cheecks for collision if the given object is close enough for a check to be meaningfull.
                if((obj.position - go.position).Length() < 100)
                {
                    if(go.GetComponent<Enemy>() != null)
                    {
                        if (CollisionCheck.Check(obj.GetComponent<CollisionCircle>().edges, go.GetComponent<CollisionCircle>().edges))
                            obj.GetComponent<SpriteRenderer>().Color = Color.Red;
                    }
                    else if (go.GetComponent<Whale>() != null)
                    {
                        if (CollisionCheck.Check(obj.GetComponent<CollisionCircle>().edges, go.GetComponent<CollisionCircle>().edges))
                            obj.GetComponent<SpriteRenderer>().Color = Color.Red;
                    }
                    else if (go.GetComponent<Tower>() != null)
                    {
                        if (CollisionCheck.Check(obj.GetComponent<CollisionCircle>().edges, go.GetComponent<CollisionCircle>().edges))
                            obj.GetComponent<SpriteRenderer>().Color = Color.Red;
                    }
                    else if (go.GetComponent<Wall>() != null)
                    {
                        if (CollisionCheck.Check(obj.GetComponent<CollisionCircle>().edges, go.GetComponent<CollisionCircle>().edges))
                            obj.GetComponent<SpriteRenderer>().Color = Color.Red;
                    }
                }
            }
        }
    }
}
