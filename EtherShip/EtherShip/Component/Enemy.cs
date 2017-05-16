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
        public int health;
        public float speed;
        public Vector2 direction;
        public int value;

        List<GridPoint> Route;
        List<GridPoint> AlternativeRoute;
        List<GridPoint> NotUsable;

        public Enemy(GameObject obj, int health, float speed, int value, Vector2 direction) : base(obj)
        {
            this.health = health;
            this.speed = speed;
            this.direction = direction;
            this.value = value;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Move(GameTime gameTime)
        {
            // skal ændres, men får fjende til at gå mod spiller  
            //Vector2 V = GameWorld.Instance.gos.Find(g => g.GetComponent("Player") != null).transform.Position - GameObject.transform.Position;
            for (int x = 0; x < GameWorld.Instance.Map.MapGrid.GetLength(0); x++)
            {
                for (int y = 0; y < GameWorld.Instance.Map.MapGrid.GetLength(1); y++)
                {
                   // Math.Abs((GameWorld.Instance.Map.MapGrid[x, y].Pos - obj.position).Length)
   
                }
            }

        }

    }
}
