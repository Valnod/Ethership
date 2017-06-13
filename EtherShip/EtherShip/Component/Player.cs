using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Timers;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace EtherShip
{
    public class Player : Component, IUpdateable//, ICollidable
    {
        private SFX sfx;
        private Vector2 direction;
        private SpriteRenderer spriteRenderer;
        private Animator animator;
        public int Health { get; set; }
        private float speed;
        private float maxSpeed;
        private float minSpeed;
        private Vector2 g; //Gravity vector
        private Vector2 push; //Push vector for when colliding with solid objects
        private bool invincible; //When true your invincible and can't collide with enemies 
        private float invincibleTime = 1000; //Indicates the amount of time your invincible
        private float invincibleTimer; //Indicates the amount of time you have been invincible

        public bool antiGravity; //Anti-gravity effect
        private bool cdTimer; //Cooldown of the anti-gravity ability
        private float timer = 0; //Timer for both anti-gravity effect and the anti gravity ability
        private Vector2 translation;
        

        public int Score { get; set; }
        public int Credit { get; set; }

        public Player(GameObject obj, Vector2 direction, int health, bool antiGravity) : base(obj)
        {
            this.direction = direction;
            this.Health = health;
            this.antiGravity = false;
            this.cdTimer = false;
            speed = 0;
            minSpeed = 0;
            maxSpeed = 10;
            spriteRenderer = obj.GetComponent<SpriteRenderer>();
            animator = obj.GetComponent<Animator>();
            this.invincible = false;
            this.Credit = 90;


            this.Health = 100;
        }

        /// <summary>
        /// Anti-gravity ability that makes the player immune to the tower object gravitational pull 
        /// </summary>
        /// <param name="gameTime"></param>
        public void AntiGravity(GameTime gameTime)
        {
            g = Vector2.Zero; //Reset gravity
            antiGravity = true;
            cdTimer = true;
        }
        
        public void Update(GameTime gameTime)
        {
            //Invincible timer
            if (invincible)
            {
                invincibleTimer += gameTime.ElapsedGameTime.Milliseconds;
                if(invincibleTimer >= invincibleTime)
                {
                    invincible = false;
                    invincibleTimer = 0;
                }
            }
            else
            {
                if(Health <= 0)
                {
                    GameWorld.Instance.GameOver = true;
                }
            }

            if (antiGravity == true || cdTimer == true)
            {
#if DEBUG
                obj.GetComponent<SpriteRenderer>().Color = Color.DarkGreen;
#endif 
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer >= 5) //5 or more sec
                {
                    antiGravity = false; //Anti-gravity ability stops
                    obj.GetComponent<SpriteRenderer>().Color = Color.ForestGreen;
                }
                if (timer >= 8) //8 or more sec
                {
                    cdTimer = false; //Cooldown finishes, anti gravity ability becomes available to the player again 
                    timer = 0; //Timer resets
                    obj.GetComponent<SpriteRenderer>().Color = Color.White;
                }
            }
            Move(gameTime);
            MapCollision();
        }

        public void Move(GameTime gameTime)
        {
            Vector2 direction = new Vector2((float)Math.Cos(obj.GetComponent<SpriteRenderer>().Rotation), 
                (float)Math.Sin(obj.GetComponent<SpriteRenderer>().Rotation));
            direction.Normalize();

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float circle = MathHelper.Pi * 2;

            translation = Vector2.Zero; //Reset the translation
            KeyboardState keystate = Keyboard.GetState(); //Get the keyboard state

            if (keystate.IsKeyDown(Keys.W))
            {
                if (speed <= maxSpeed) //Accelerate the player object forward as long as the W button is held
                {
                    speed += 0.6f - (float) gameTime.ElapsedGameTime.TotalSeconds;
                    translation += direction * speed;
                }
                if (speed > maxSpeed) //Caps the player speed to 5
                {
                    speed = maxSpeed;
                }
            }
            if (keystate.IsKeyUp(Keys.W))
            {
                if (speed >= minSpeed) //When the W button is released, the player object slowly lose momentum
                {
                    speed -= 0.1f;
                    translation += direction * speed;
                }
            }
            if (keystate.IsKeyDown(Keys.A))
            {
                obj.GetComponent<SpriteRenderer>().Rotation -= elapsed;
                obj.GetComponent<SpriteRenderer>().Rotation = obj.GetComponent<SpriteRenderer>().Rotation % circle;
                obj.GetComponent<SpriteRenderer>().Rotation -= 0.05f; // Rotate the sprite (clockwise left)
            }
            //if (keystate.IsKeyDown(Keys.S))
            //{
            //    Down (unnecessary?)
            //}
            if (keystate.IsKeyDown(Keys.D))
            {
                obj.GetComponent<SpriteRenderer>().Rotation += elapsed;
                obj.GetComponent<SpriteRenderer>().Rotation = obj.GetComponent<SpriteRenderer>().Rotation % circle;
                obj.GetComponent<SpriteRenderer>().Rotation += 0.05f; //Rotate the sprite (clockwise right)
            }
            if (translation.X != float.NaN && translation.Y != float.NaN)
            {
                if (translation.X != 0 || translation.Y != 0)
                    translation = Vector2.Normalize(translation); //Normalize the movement to 1 (doesn't add up in case of multible buttons press)
                translation *= speed;
                if (antiGravity == false)
                {
                    g = GravityPull();
                }
                //Ensures that the gravity pull can't be greater than the tranlation vector, ensuring you can't be trapped by gravity
                if (g.Length() > translation.Length())
                    g = Vector2.Normalize(g) * maxSpeed * 6;
                translation = (g + translation * speed) / (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                //Checks collision and changes the push vector accordingly
                OBJCollision();

                //Changes the position
                this.obj.position += push + translation;

            }
            if (keystate.IsKeyDown(Keys.Q))
            {
                //soundTest.soundEffects[0].CreateInstance().Play();
                AntiGravity(gameTime); //Activate anti-gravity ability
            }
        }

        /// <summary>
        /// Calculates the gravitational pull from all the towers.
        /// </summary>
        /// <returns></returns>
        private Vector2 GravityPull()
        {
            Vector2 totalGravPull = Vector2.Zero;
            foreach(GameObject tower in GameWorld.Instance.gameObjectPool.ActiveTowerList)
            {
                totalGravPull += tower.GetComponent<Tower>().Gravity(this.obj.position, maxSpeed);
            }
            return totalGravPull;
        }        

        /// <summary>
        /// Checks for collision and acts if there is a collision.
        /// </summary>
        public void OBJCollision()
        {
            push = Vector2.Zero;
            //Checks if this collides with another gameobject.
            foreach (GameObject go in GameWorld.Instance.gameObjectPool.CollisionListForPlayer())
            {
                //Checks the distance to the objects, and only cheecks for collision if the given object is close enough for a check to be meaningfull.
                if ((obj.position - go.position).Length() < 200)
                {
                    //The collision checks are done with the upcoming location in mind. The division is just a adjustment, so the objects can come closer before colliding. 
                    if (go.GetComponent<Enemy>() != null)
                        if(!invincible)
                            push += CollisionCheck.CheckV2(obj.GetComponent<CollisionCircle>().edges, obj.position + translation, go.GetComponent<CollisionCircle>().edges, go.position);
                    if (go.GetComponent<Whale>() != null)
                        push += CollisionCheck.CheckV2(obj.GetComponent<CollisionCircle>().edges, obj.position + translation, go.GetComponent<CollisionCircle>().edges, go.position);
                    if (go.GetComponent<Tower>() != null)
                        push += CollisionCheck.CheckV2(obj.GetComponent<CollisionCircle>().edges, obj.position + translation, go.GetComponent<CollisionCircle>().edges, go.position);
                    if (go.GetComponent<Wall>() != null)
                        push += CollisionCheck.CheckV2(obj.GetComponent<CollisionCircle>().edges, obj.position + translation, go.GetComponent<CollisionRectangle>().edges, go.position);                    
                    
                    //If push's length is greater than 0 a collisions happens, and acts differently depending on what is hit
                    if(push.Length() > 0)
                    {
                        if (go.GetComponent<Enemy>() != null)
                        {
#if DEBUG
                            obj.GetComponent<SpriteRenderer>().Color = Color.Red;
#endif
                            if (!invincible)
                            {
                                Health -= 1;
                                invincible = true;
                            }
                        }
#if DEBUG
                        else if (go.GetComponent<Whale>() != null)
                            obj.GetComponent<SpriteRenderer>().Color = Color.Blue;
                        else if (go.GetComponent<Tower>() != null)
                            obj.GetComponent<SpriteRenderer>().Color = Color.RoyalBlue;
                        else if (go.GetComponent<Wall>() != null)
                            obj.GetComponent<SpriteRenderer>().Color = Color.Black;
#endif
                    }

                    //Ensures that the push vector can't be greater than the translation vector.
                    if (push.Length() > translation.Length())
                        push = Vector2.Normalize(push) * translation.Length();
                }
            }
        }

        public void MapCollision()
        {
            int minX = (obj.GetComponent<SpriteRenderer>().SpriteRectangleForCollision.Width) / 2;
            int maxX = GameWorld.Instance.GraphicsDevice.Viewport.Width - (obj.GetComponent<SpriteRenderer>().SpriteRectangleForCollision.Width) / 2;
            int minY = (obj.GetComponent<SpriteRenderer>().SpriteRectangleForCollision.Height) / 2;
            int maxY = GameWorld.Instance.GraphicsDevice.Viewport.Height - (obj.GetComponent<SpriteRenderer>().SpriteRectangleForCollision.Height / 2) - GameWorld.Instance.Menu.GetUIHeight() - 20;

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
    }
}
