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
        private bool checkPath;
        public int health;
        public float speed;
        public Vector2 direction;
        public int value;

        List<GridPoint> Route = new List<GridPoint>();
        List<GridPoint> UsablePoints = new List<GridPoint>();
        List<GridPoint> NotUsable = new List<GridPoint>();

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
           
            //the enenmy checks the position of the array and the enemy and finds the closest arrayGrid
            // the float length purpose is to have something that is always larger than the length we seek, so that we have 
            //something to check our Delta Length with. 
            float length = 100000000;
            //xLoc is closest x position
            int xStart = 10;
            //yLoc is closest y position
            int yStart = 10;
            //xEnd is closet position to player
            int xEnd = 0;
            //yEnd is closet position to player
            int yEnd = 0;

            //for checking the x array 
            for (int x = 0; x < GameWorld.Instance.Map.MapGrid.GetLength(0); x++)
            {
                //and checking the y array
                for (int y = 0; y < GameWorld.Instance.Map.MapGrid.GetLength(1); y++)
                {
                    //lengthTemp is a variabel where we save our current closest position
                    float lengthTempStart = Math.Abs((GameWorld.Instance.Map.MapGrid[x, y].Pos - obj.position).Length());
                    //checks if the lengthTemp is smaller than the length, the last one found is the absolute shortest from the current position
                    if (lengthTempStart < length)
                    {
                        length = lengthTempStart;
                        xStart = x;
                        yStart = y;
                    }
                }
            }

            for(int x = 0; x < GameWorld.Instance.Map.MapGrid.GetLength(0); x++)
            {
                for (int y = 0; y < GameWorld.Instance.Map.MapGrid.GetLength(1); y++)
                {
                    //same as the function above, exept this find the shortest route for the player
                    float lengthTempEnd = Math.Abs((GameWorld.Instance.Map.MapGrid[x, y].Pos - GameWorld.Instance.gameObjectPool.player.position).Length());
                    if (lengthTempEnd < length)
                    {
                        length = lengthTempEnd;
                        xEnd = x;
                        yEnd = y;
                    }
                }
            }

            checkPath = true;
            while (checkPath)
            {
                CheckForValidPath(xStart, yStart);
                Route.Add(UsablePoints[UsablePoints.Count-1]);
                
            }
           
        }

        //checks all the points around the xStart and yStart for a valid route, then puts the valid point in the UsablePoints 
        //List, and the invalids in the NotUsables List. 

            //first diamond, Down
        public void CheckForValidPath(int xStart, int yStart)
        {
            
            if (GameWorld.Instance.Map.MapGrid[xStart, yStart + 1].Occupant == null)
            {
                if (GameWorld.Instance.Map.MapGrid[xStart, yStart + 2].Occupant == null)
                {
                    UsablePoints.Add(GameWorld.Instance.Map.MapGrid[xStart, yStart + 2]);
                }
                else
                    NotUsable.Add(GameWorld.Instance.Map.MapGrid[xStart, yStart + 2]);

                if (GameWorld.Instance.Map.MapGrid[xStart + 1, yStart + 1].Occupant == null)
                {
                    UsablePoints.Add(GameWorld.Instance.Map.MapGrid[xStart + 1, yStart + 1]);
                }
                else
                    NotUsable.Add(GameWorld.Instance.Map.MapGrid[xStart + 1, yStart + 1]);

                if (GameWorld.Instance.Map.MapGrid[xStart - 1, yStart + 1].Occupant == null)
                {
                    UsablePoints.Add(GameWorld.Instance.Map.MapGrid[xStart - 1, yStart + 1]);
                }
                else
                    NotUsable.Add(GameWorld.Instance.Map.MapGrid[xStart - 1, yStart + 1]);

                //second diamond, right

            }
            if (GameWorld.Instance.Map.MapGrid[xStart + 1, yStart].Occupant == null)
            {
                if (GameWorld.Instance.Map.MapGrid[xStart + 2, yStart].Occupant == null)
                {
                    UsablePoints.Add(GameWorld.Instance.Map.MapGrid[xStart + 2, yStart]);
                }
                else
                    NotUsable.Add(GameWorld.Instance.Map.MapGrid[xStart + 2, yStart]);

                if (GameWorld.Instance.Map.MapGrid[xStart + 1, yStart + 1].Occupant == null)
                {
                    UsablePoints.Add(GameWorld.Instance.Map.MapGrid[xStart + 1, yStart + 1]);
                }
                else
                    NotUsable.Add(GameWorld.Instance.Map.MapGrid[xStart + 1, yStart + 1]);

                if (GameWorld.Instance.Map.MapGrid[xStart + 1, yStart - 1].Occupant == null)
                {
                    UsablePoints.Add(GameWorld.Instance.Map.MapGrid[xStart + 1, yStart - 1]);
                }
                else
                    NotUsable.Add(GameWorld.Instance.Map.MapGrid[xStart + 1, yStart - 1]);
            }

            //third diamond, Up

            if (GameWorld.Instance.Map.MapGrid[xStart, yStart - 1].Occupant == null)
            {
                if (GameWorld.Instance.Map.MapGrid[xStart, yStart - 2].Occupant == null)
                {
                    UsablePoints.Add(GameWorld.Instance.Map.MapGrid[xStart, yStart - 2]);
                }
                else
                    NotUsable.Add(GameWorld.Instance.Map.MapGrid[xStart, yStart - 2]);
                if (GameWorld.Instance.Map.MapGrid[xStart - 1, yStart - 1].Occupant == null)
                {
                    UsablePoints.Add(GameWorld.Instance.Map.MapGrid[xStart - 1, yStart - 1]);
                }
                else
                    NotUsable.Add(GameWorld.Instance.Map.MapGrid[xStart - 1, yStart - 1]);

                if (GameWorld.Instance.Map.MapGrid[xStart + 1, yStart - 1].Occupant == null)
                {
                    UsablePoints.Add(GameWorld.Instance.Map.MapGrid[xStart + 1, yStart - 1]);
                }
                else
                    NotUsable.Add(GameWorld.Instance.Map.MapGrid[xStart + 1, yStart - 1]);
            }

            //fourth Diamond, left
            if (GameWorld.Instance.Map.MapGrid[xStart - 1, yStart].Occupant == null)
            {
                if (GameWorld.Instance.Map.MapGrid[xStart - 2, yStart].Occupant == null)
                {
                    UsablePoints.Add(GameWorld.Instance.Map.MapGrid[xStart - 2, yStart]);
                }
                else
                    NotUsable.Add(GameWorld.Instance.Map.MapGrid[xStart - 2, yStart]);
                if (GameWorld.Instance.Map.MapGrid[xStart - 1, yStart + 1].Occupant == null)
                {
                    UsablePoints.Add(GameWorld.Instance.Map.MapGrid[xStart - 1, yStart + 1]);
                }
                else
                    NotUsable.Add(GameWorld.Instance.Map.MapGrid[xStart - 1, yStart + 1]);

                if (GameWorld.Instance.Map.MapGrid[xStart - 1, yStart - 1].Occupant == null)
                {
                    UsablePoints.Add(GameWorld.Instance.Map.MapGrid[xStart - 1, yStart - 1]);
                }
                else
                    NotUsable.Add(GameWorld.Instance.Map.MapGrid[xStart - 1, yStart - 1]);
            }
        }
    }
}
