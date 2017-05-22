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
        private int health;
        private float speed;
        private Vector2 direction;
        private int value;

        List<GridPoint> Route = null;
    
        public Enemy(GameObject obj, int health, float speed, int value, Vector2 direction) : base(obj)
        {
            this.health = health;
            this.speed = speed;
            this.direction = direction;
            this.value = value;
        }

        public void Update(GameTime gameTime)
        {
            Move(gameTime);
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
                Vector2 newPosition = obj.position + Vector2.Normalize(routeDirection) * speed;

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
    }
}
