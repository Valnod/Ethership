using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Timers;

namespace EtherShip
{
    class Player : Component, IUpdateable
    {
        public Vector2 direction;
        public int health;
        public float speed;

        public bool antiGravity = false; //Anti-gravity effect
        public bool cdTimer = false; //Cooldown of the anti-gravity ability
        float timer = 0; //Timer for both anti-gravity effect and the anti gravity ability

        public Player(GameObject obj, Vector2 direction, int health, bool antiGravity) : base(obj)
        {
            this.direction = direction;
            this.health = health;
            this.antiGravity = antiGravity;
            speed = 10;
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
            //Move(gameTime);
            MoveTest(gameTime);
        }

        public void MoveTest(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            Vector2 translation = Vector2.Zero; //Reset the translation
            KeyboardState keystate = Keyboard.GetState(); //Get the keyboard state

            if (keystate.IsKeyDown(Keys.W))
            {
                translation += new Vector2(0, -1); //Up
            }
            if (keystate.IsKeyDown(Keys.A))
            {
                translation += new Vector2(-1, 0); //Left
            }
            if (keystate.IsKeyDown(Keys.S))
            {
                translation += new Vector2(0, 1); //Down
            }
            if (keystate.IsKeyDown(Keys.D))
            {
                translation += new Vector2(1, 0); //Right
            }
            if (translation.X != float.NaN && translation.Y != float.NaN)
            {
                Vector2.Normalize(translation); //Normalize the movement to 1 (doesn't add up in case of multible buttons press)
                translation *= speed;
                this.obj.position += translation * speed / (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }

        public void Move(GameTime gameTime)
        {
            Vector2 translation = Vector2.Zero; //Reset the translation
            KeyboardState keystate = Keyboard.GetState(); //Get the keyboard state

            if (keystate.IsKeyDown(Keys.W))
            {
                translation += new Vector2(0, -1); //Up
            }
            if (keystate.IsKeyDown(Keys.A))
            {
                translation += new Vector2(-1, 0); //Left
            }
            if (keystate.IsKeyDown(Keys.S))
            {
                translation += new Vector2(0, 1); //Down
            }
            if (keystate.IsKeyDown(Keys.D))
            {
                translation += new Vector2(1, 0); //Right
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
    } 
}
