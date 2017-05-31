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

        private float acceleration;
        private float speedElement;
        private int currentWayPoint = 0;
        private bool checkPath;
        private float speed;
        private Vector2 direction;
        private Vector2 translation;
        private int value;
        private float timer;
        private float cooldown = 300;

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
        public void Move(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;

            if (NewRoute == null || (!Generating && timer >= cooldown))
            {
               int width = GameWorld.Instance.Window.ClientBounds.Width,
                    height = GameWorld.Instance.Window.ClientBounds.Height;
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
                    Vector2 newPosition = obj.position + translation;

                    if ((CurrentRoute[currentWayPoint].Pos - newPosition).Length() > (CurrentRoute[currentWayPoint].Pos - obj.position).Length())
                        currentWayPoint++;
                    if (currentWayPoint == CurrentRoute.Count)
                    {
                        currentWayPoint = 0;
                        CurrentRoute = null;
                    }
                    obj.position = newPosition;
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
