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
    public class Enemy : Component, IUpdateable
    {
        public bool Generating = false;
        private Vector2 push;
        private float acceleration;
        private float speedElement;
        private int currentWayPoint = 0;
        private bool checkPath;
        private float speed;
        private Vector2 direction;
        private Vector2 translation;
        private Vector2 g;
        private float gravityEfftectiveness = 0.55f;
        private int value;
        private float timer;
        private float cooldown = 500;

        public int Health { get; set; }
        private int maxHealth;

        List<GridPoint> NewRoute = null;
        List<GridPoint> CurrentRoute = null;
    
        public Enemy(GameObject obj, int maxHealth, float speed, int value, Vector2 direction) : base(obj)
        {
            this.maxHealth = maxHealth;
            this.speed = speed;
            this.direction = direction;
            this.value = value;
            this.push = Vector2.Zero;
            ResetHealth();
        }

        public void Update(GameTime gameTime)
        {
            Move(gameTime);
            CheckAmIDead();
        }

        public void ResetHealth()
        {
            Health = maxHealth;
        }

        /// <summary>
        /// Calculates the gravitational pull from all the towers.
        /// </summary>
        /// <returns></returns>
        private Vector2 GravityPull()
        {
            Vector2 totalGravPull = Vector2.Zero;
            foreach (GameObject tower in GameWorld.Instance.gameObjectPool.ActiveTowerList)
            {
                totalGravPull += tower.GetComponent<Tower>().Gravity(this.obj.position, speed);
            }
            return totalGravPull;
        }

        public void Move(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;

            if (NewRoute == null || (!Generating && timer >= cooldown))
            {
               int width = GameWorld.Instance.WindowWidth,
                    height = GameWorld.Instance.WindowHeigth;
               new System.Threading.Thread(() => NewRoute = AI.Pathfind(GameWorld.Instance.Map[obj.position], GameWorld.Instance.Map[GameWorld.Instance.gameObjectPool.player.position],
                   width, height)).Start();
                Generating = true;
                timer = 0;
            }
            else if (NewRoute != null)
            {
                CurrentRoute = NewRoute;

                if (CurrentRoute.Count > currentWayPoint)
                {
                    Generating = false;
                    Vector2 routeDirection = CurrentRoute[currentWayPoint].Pos - obj.position;
                    translation = Vector2.Normalize(routeDirection) * speed;

                    //calculates gravity pull
                    g = GravityPull();
                    //Ensures that the gravity pull can't be greater than the tranlation vector, ensuring you can't be trapped by gravity
                    if (g.Length() > translation.Length())
                        g = Vector2.Normalize(g) * speed * gravityEfftectiveness;
                    //Adds gravity pull
                    translation += g;

                    //Looks at collision
                    OBJCollision();

                    //New position
                    Vector2 newPosition = (obj.position + translation) + push;
                    push = Vector2.Zero;

                    if ((CurrentRoute[currentWayPoint].Pos - newPosition).Length() > (CurrentRoute[currentWayPoint].Pos - obj.position).Length())
                        currentWayPoint++;
                    if (currentWayPoint == CurrentRoute.Count)
                    {
                        currentWayPoint = 0;
                        CurrentRoute = null;
                    }
                    obj.position = newPosition;
                }
                else if (currentWayPoint >= CurrentRoute.Count)
                {
                    currentWayPoint = 0;
                    CurrentRoute = null;
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


        /// <summary>
        /// Checks for collision and acts if there is a collision.
        /// </summary>
        public void OBJCollision()
        {
            push = Vector2.Zero;
            //Checks if this collides with another gameobject.
            foreach (GameObject go in GameWorld.Instance.gameObjectPool.CollisionListForEnemy())
            {
                //Checks the distance to the objects, and only cheecks for collision if the given object is close enough for a check to be meaningfull.
                if (go != this.obj && ((obj.position - go.position).Length() < 200))
                {
                    //The collision checks are done with the upcoming location in mind. The division is just a adjustment, so the objects can come closer before colliding. 
                    if (go.GetComponent<Enemy>() != null)
                        push += CollisionCheck.CheckV2(obj.GetComponent<CollisionCircle>().edges, obj.position + translation, go.GetComponent<CollisionCircle>().edges, go.position);
                    if (go.GetComponent<Tower>() != null)
                        push += CollisionCheck.CheckV2(obj.GetComponent<CollisionCircle>().edges, obj.position + translation, go.GetComponent<CollisionCircle>().edges, go.position);
                    if (go.GetComponent<Wall>() != null)
                        push += CollisionCheck.CheckV2(obj.GetComponent<CollisionCircle>().edges, obj.position + translation, go.GetComponent<CollisionRectangle>().edges, go.position);

#if DEBUG
                    //If push's length is greater than 0 a collisions happens, and acts differently depending on what is hit
                    if (push.Length() > 0)
                    {
                        if (go.GetComponent<Enemy>() != null)
                            obj.GetComponent<SpriteRenderer>().Color = Color.Red;
                        else if (go.GetComponent<Tower>() != null)
                            obj.GetComponent<SpriteRenderer>().Color = Color.RoyalBlue;
                        else if (go.GetComponent<Wall>() != null)
                            obj.GetComponent<SpriteRenderer>().Color = Color.Black;
                    }
#endif

                    //Ensures that the push vector can't be greater than the translation vector.
                    if (push.Length() > translation.Length())
                        push = Vector2.Normalize(push) * translation.Length();
                }
            }
        }

    }
}
