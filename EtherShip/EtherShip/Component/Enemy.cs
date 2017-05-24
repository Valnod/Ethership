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
        private bool Generating = false;

        private float acceleration;
        private float speedElement;
        private int currentWayPoint = 0;
        private bool checkPath;
        private float speed;
        private Vector2 direction;
        private Vector2 translation;
        private int value;

        public int Health { get; set; }

        List<GridPoint> Route = null;
    
        public Enemy(GameObject obj, int health, float speed, int value, Vector2 direction) : base(obj)
        {
            this.Health = health;
            this.speed = speed;
            this.direction = direction;
            this.value = value;
        }

        public void Update(GameTime gameTime)
        {
            Move(gameTime);
            CheckAmIDead();
        }

        public void Move(GameTime gameTime)
        {
            if(Route == null && !Generating)
            {
               int width = GameWorld.Instance.Window.ClientBounds.Width,
                    height = GameWorld.Instance.Window.ClientBounds.Height;
               new System.Threading.Thread(() => Route = AI.Pathfind(GameWorld.Instance.Map[obj.position], GameWorld.Instance.Map[GameWorld.Instance.gameObjectPool.player.position],
                   width, height)).Start();
                Generating = true;
            
            }
            else if (Route != null)
            {
                Generating = false;
                Vector2 routeDirection = Route[currentWayPoint].Pos - obj.position;
                translation = Vector2.Normalize(routeDirection) * speed;
                Vector2 newPosition = obj.position + translation;

                if ((Route[currentWayPoint].Pos - newPosition).Length() > (Route[currentWayPoint].Pos - obj.position).Length())
                    currentWayPoint++;
                if (currentWayPoint == Route.Count)
                {
                    currentWayPoint = 0;
                    Route = null;
                }
                obj.position = newPosition;
            }
        }

        /// <summary>
        /// Checks for collision and acts if there is a collision.
        /// </summary>
        public void OBJCollision()
        {
            //Checks if this collides with another gameobject.
            foreach (GameObject go in GameWorld.Instance.gameObjectPool.CollisionListForPlayer())
            {
                //Checks the distance to the objects, and only cheecks for collision if the given object is close enough for a check to be meaningfull.
                if ((obj.position - go.position).Length() < 200)
                {
                    //The collision checks are done with the upcoming location in mind. The division is just a adjustment, so the objects can come closer before colliding. 
                    if (go.GetComponent<Enemy>() != null)
                    {
                        if (CollisionCheck.Check(obj.GetComponent<CollisionCircle>().edges, obj.position + (translation / 2), go.GetComponent<CollisionCircle>().edges, go.position))
                        {
                            obj.GetComponent<SpriteRenderer>().Color = Color.Red;
                        }
                    }
                    else if (go.GetComponent<Tower>() != null)
                    {
                        if (CollisionCheck.Check(obj.GetComponent<CollisionCircle>().edges, obj.position + (translation / 2), go.GetComponent<CollisionCircle>().edges, go.position))
                        {
                            obj.GetComponent<SpriteRenderer>().Color = Color.RoyalBlue;
                            translation = CollisionReaction.EllipseCircle(this.obj.position, this.translation, go.position);
                        }
                    }
                    else if (go.GetComponent<Wall>() != null)
                    {
                        if (CollisionCheck.Check(obj.GetComponent<CollisionCircle>().edges, obj.position + (translation / 2), go.GetComponent<CollisionRectangle>().edges, go.position))
                        {
                            obj.GetComponent<SpriteRenderer>().Color = Color.Black;
                            translation = CollisionReaction.EllipseRectangle(this.obj.position, translation, go.position, GameWorld.Instance.Map.GridPointSize);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks if health is below 0, and if so move the object to inactive.
        /// </summary>
        public void CheckAmIDead()
        {
            if (Health < 0)
                GameWorld.Instance.gameObjectPool.RemoveActive.Add(obj);
        }
    }
}
