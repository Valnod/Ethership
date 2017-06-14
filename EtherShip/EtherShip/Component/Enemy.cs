using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace EtherShip
{
    public class Enemy : Component, IUpdateable
    {
        private Animator animator;
        public bool generating = false;
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

        protected int bountyGiven;
        protected int scoreGiven;

        public Enemy(GameObject obj, int maxHealth, float speed, int value, Vector2 direction, int bountyGiven, int scoreGiven) : base(obj)
        {
            this.maxHealth = maxHealth;
            this.speed = speed;
            this.direction = direction;
            this.value = value;
            this.push = Vector2.Zero;
            this.bountyGiven = bountyGiven;
            this.scoreGiven = scoreGiven;

            ResetHealth();
        }

        public int BountyGiven
        {
            get { return bountyGiven; }
        }
        public void Update(GameTime gameTime)
        {
            //Move(gameTime);
            Move2(gameTime);
            MapCollision();
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

        public void LoadContent(ContentManager content)
        {
            this.animator = obj.GetComponent<Animator>();

            CreateAnimations();
        }

        public void Move2(GameTime gameTime)
        {
            translation = Vector2.Zero;
            //the number indicates the strength 
            direction = Vector2.Normalize(direction + GameWorld.Instance.Map[obj.position].directionVec * 0.2f);  
            //Does so the sprite points in the movement direction. The number is an adjustment so the sprite is turned correctly
            obj.GetComponent<SpriteRenderer>().Rotation = (float)Math.Atan2(direction.X, -direction.Y) - 1.5f;

            //Calculates gravity pull
            g = GravityPull();
            //Ensures that the gravity pull can't be greater than the tranlation vector, ensuring you can't be trapped by gravity
            if (g.Length() > translation.Length())
                g = Vector2.Normalize(g) * speed * gravityEfftectiveness;

            translation = (direction * speed + g) / gameTime.ElapsedGameTime.Milliseconds;

            //Looks at collision
            OBJCollision();

            obj.position += translation + push;
            push = Vector2.Zero;
        }

        public void Move(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;

            if (NewRoute == null || (!generating && timer >= cooldown))
            {
               int width = GameWorld.Instance.WindowWidth,
                    height = GameWorld.Instance.WindowHeight;
               new System.Threading.Thread(() => NewRoute = AI.Pathfind(GameWorld.Instance.Map[obj.position], GameWorld.Instance.Map[GameWorld.Instance.gameObjectPool.player.position],
                   width, height)).Start();
                generating = true;
                timer = 0;
            }
            else if (NewRoute != null)
            {
                CurrentRoute = NewRoute;

                if (CurrentRoute.Count > currentWayPoint)
                {
                    generating = false;
                    Vector2 routeDirection = CurrentRoute[currentWayPoint].Pos - obj.position;
                    translation = Vector2.Normalize(routeDirection) * speed;

                    //Calculates gravity pull
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
            {
                GameWorld.Instance.gameObjectPool.RemoveActive.Add(obj);

                GameWorld.Instance.gameObjectPool.player.GetComponent<Player>().Credit += BountyGiven;
                GameWorld.Instance.gameObjectPool.player.GetComponent<Player>().Score += scoreGiven;
            }            
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
                            obj.GetComponent<SpriteRenderer>().Color = Color.Beige;
                    }
#endif

                    //Ensures that the push vector can't be greater than the translation vector.
                    if (push.Length() > translation.Length())
                        push = Vector2.Normalize(push) * translation.Length();
                }
            }
        }

        public void MapCollision()
        {
            int minX = (obj.GetComponent<SpriteRenderer>().SpriteRectangleForCollision.Width) / 4;
            int maxX = GameWorld.Instance.GraphicsDevice.Viewport.Width - (obj.GetComponent<SpriteRenderer>().SpriteRectangleForCollision.Width) / 4;
            int minY = (obj.GetComponent<SpriteRenderer>().SpriteRectangleForCollision.Height) / 4;
            int maxY = GameWorld.Instance.GraphicsDevice.Viewport.Height - (obj.GetComponent<SpriteRenderer>().SpriteRectangleForCollision.Height / 4) - GameWorld.Instance.Menu.GetUIHeight() - 20;

            if (GameWorld.Instance.Window != null) //Prevents the program from crashing, when the window is closed
            {
                if (!float.IsNaN(GameWorld.Instance.GraphicsDevice.DisplayMode.Width))
                {
                    if (obj.position.X > maxX) //Right GameWindow collision
                    {
                        obj.position.X = maxX;
#if DEBUG
                        obj.GetComponent<SpriteRenderer>().Color = Color.Yellow;
#endif
                    }
                    else if (obj.position.X < minX) //Left GameWindow collision
                    {
                        obj.position.X = minX;
#if DEBUG
                        obj.GetComponent<SpriteRenderer>().Color = Color.Yellow;
#endif
                    }
                }
                if (!float.IsNaN(GameWorld.Instance.GraphicsDevice.DisplayMode.Height))
                {
                    if (obj.position.Y > maxY) //Bottom GameWindow collsion
                    {
                        obj.position.Y = maxY;
#if DEBUG
                        obj.GetComponent<SpriteRenderer>().Color = Color.Yellow;
#endif
                    }
                    else if (obj.position.Y < minY) //Top GameWindow collision
                    {
                        obj.position.Y = minY;
#if DEBUG
                        obj.GetComponent<SpriteRenderer>().Color = Color.Yellow;
#endif
                    }

                }

            }
        }
        public void CreateAnimations()
        {


            animator.CreateAnimation(new Animation(6, 300, 0, 107, 100, 6, Vector2.Zero), "WalkRight");
            animator.CreateAnimation(new Animation(6, 700, 0, 107, 100, 6, Vector2.Zero), "WalkLeft");
        




            animator.CheckAnimation("IdleLeft");


        }
    }
}
