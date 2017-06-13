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
    class Whale : Component, IUpdateable
    {
        public Vector2 target;
        public Vector2 direction;
        public int value;
        public int health;
        public float speed;
        private int xSpawn;
        private int ySpawn;
        private Random rnd;

        public int Health { get; set; }

        //AI

        private bool generating = false;

        private float acceleration;
        private float speedElement;
        private int currentWayPoint = 0;
        private bool checkPath;
        private Vector2 translation;
        private float timer;
        private float cooldown = 1000;

        List<GridPoint> NewRoute = null;
        List<GridPoint> CurrentRoute = null;

        public Whale(GameObject obj, Vector2 target, Vector2 direction, int value, int health, float speed) : base(obj)
        {
            this.target = target;
            this.direction = direction;
            this.value = value;
            this.Health = health;
            this.speed = speed;
        }

        public void Update(GameTime gameTime)
        {
            Move(gameTime);
            CheckAmIDeadWhale();
        }

        public void Move(GameTime gameTime)
        {
            {
                timer += gameTime.ElapsedGameTime.Milliseconds;

                if (NewRoute == null || !generating || timer >= cooldown)
                {
                    int width = GameWorld.Instance.Window.ClientBounds.Width,
                         height = GameWorld.Instance.Window.ClientBounds.Height;
                    new System.Threading.Thread(() => NewRoute = AI.Pathfind(GameWorld.Instance.Map[obj.position], GameWorld.Instance.Map[new Vector2(GameWorld.Instance.Map.MapGrid.GetLength(0) / (GameWorld.Instance.Map.GridPointSize * 12), (GameWorld.Instance.Map.MapGrid.GetLength(1)  / 2) * (GameWorld.Instance.Map.GridPointSize))],
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
        }



        ///<summary>
        /// Checks if health is below 0, and if so move the object to inactive..
        /// </summary>
        public void CheckAmIDeadWhale()
        {
            if (Health < 0)
                GameWorld.Instance.gameObjectPool.RemoveActive.Add(obj);
        }
    }
}
