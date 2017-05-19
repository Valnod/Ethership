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

namespace EtherShip
{
    class Player : Component, IUpdateable//, ICollidable
    {
        private Vector2 direction;
        private SpriteRenderer spriteRenderer;
        private Animator animator;
        private int health;
        private float speed;
        private float maxSpeed;
        private float minSpeed;

        private bool antiGravity = false; //Anti-gravity effect
        private bool cdTimer = false; //Cooldown of the anti-gravity ability
        float timer = 0; //Timer for both anti-gravity effect and the anti gravity ability

        public Player(GameObject obj, Vector2 direction, int health, bool antiGravity) : base(obj)
        {
            this.direction = direction;
            this.health = health;
            this.antiGravity = antiGravity;
            speed = 0;
            minSpeed = 0;
            maxSpeed = 5;
            spriteRenderer = obj.GetComponent<SpriteRenderer>();
            animator = obj.GetComponent<Animator>();
        }

        /// <summary>
        /// Anti-gravity ability that makes the player immune to the tower object gravitational pull 
        /// </summary>
        /// <param name="gameTime"></param>
        public void AntiGravity(GameTime gameTime)
        {
            antiGravity = true;
            cdTimer = true;
            timer += gameTime.ElapsedGameTime.Milliseconds;
            if (timer >= 5000) //5 or more sec
            {
                antiGravity = false; //Anti-gravity ability stops
            }
            if (timer >= 8000) //8 or more sec
        {
                cdTimer = false; //Cooldown finishes, anti gravity ability becomes available to the player again 
                timer = 0; //Timer resets
            }
        }
        
        public void Update(GameTime gameTime)
        {
            Move(gameTime);
            //OBJCollision();
        }

        public void Move(GameTime gameTime)
        {
            Vector2 direction = new Vector2((float)Math.Cos(spriteRenderer.Rotation), (float)Math.Sin(spriteRenderer.Rotation));
            direction.Normalize();

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float circle = MathHelper.Pi * 2;

            Vector2 translation = Vector2.Zero; //Reset the translation
            KeyboardState keystate = Keyboard.GetState(); //Get the keyboard state

            while (keystate.IsKeyDown(Keys.W))
            {
                if (speed <= maxSpeed) //Accelerate the player object forward as long as the W button is held
                {
                    speed += 0.3f - (float) gameTime.ElapsedGameTime.TotalSeconds;
                    obj.position += direction * speed;
                }
                if (speed > maxSpeed) //Caps the player speed to 5
                {
                    speed = 5f;
                }
                break;
            }
            if (keystate.IsKeyUp(Keys.W))
            {
                if (speed >= minSpeed) //When the W button is released, the player object slowly lose momentum
                {
                    speed -= 0.1f;
                    obj.position += direction * speed;
                }
            }
            if (keystate.IsKeyDown(Keys.A))
            {
                spriteRenderer.Rotation -= elapsed;
                spriteRenderer.Rotation = spriteRenderer.Rotation % circle;
                spriteRenderer.Rotation -= 0.05f; // Rotate the sprite (clockwise left)
            }
            if (keystate.IsKeyDown(Keys.S))
            {
                //Down (unnecessary?)
            }
            if (keystate.IsKeyDown(Keys.D))
            {
                spriteRenderer.Rotation += elapsed;
                spriteRenderer.Rotation = spriteRenderer.Rotation % circle;
                spriteRenderer.Rotation += 0.05f; //Rotate the sprite (clockwise right)
            }
            if (translation.X != float.NaN && translation.Y != float.NaN)
            {
                Vector2.Normalize(translation); //Normalize the movement to 1 (doesn't add up in case of multible buttons press)
                translation *= speed;
                this.obj.position += translation * speed / (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if (keystate.IsKeyDown(Keys.Q))
            {
                AntiGravity(gameTime); //Activate anti-gravity ability
            }
        }

        /// <summary>
        /// Checks for collision and acts if there is a collision.
        /// </summary>
        /*public void OBJCollision()
        {
            //Checks if this collides with another gameobject.
            foreach(GameObject go in GameWorld.Instance.gameObjectPool.CollisionListForPlayer())
            {
                //Checks the distance to the objects, and only cheecks for collision if the given object is close enough for a check to be meaningfull.
                if((obj.position - go.position).Length() < 200)
                {
                    if(go.GetComponent<Enemy>() != null)
                    {
                        if (CollisionCheck.Check(obj.GetComponent<CollisionCircle>().edges, obj.position, go.GetComponent<CollisionCircle>().edges, go.position))
                            obj.GetComponent<SpriteRenderer>().Color = Color.Red;
                    }
                    else if (go.GetComponent<Whale>() != null)
                    {
                        if (CollisionCheck.Check(obj.GetComponent<CollisionCircle>().edges, obj.position, go.GetComponent<CollisionCircle>().edges, go.position))
                            obj.GetComponent<SpriteRenderer>().Color = Color.Blue;
                    }
                    else if (go.GetComponent<Tower>() != null)
                    {
                        if (CollisionCheck.Check(obj.GetComponent<CollisionCircle>().edges, obj.position, go.GetComponent<CollisionCircle>().edges, go.position))
                            obj.GetComponent<SpriteRenderer>().Color = Color.RoyalBlue;
                    }
                    else if (go.GetComponent<Wall>() != null)
                    {
                        if (CollisionCheck.Check(obj.GetComponent<CollisionCircle>().edges, obj.position, go.GetComponent<CollisionRectangle>().edges, go.position))
                            obj.GetComponent<SpriteRenderer>().Color = Color.Black;
                    }
                }
            }
        }*/
    }
}
