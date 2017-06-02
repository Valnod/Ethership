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
    class BuildMode
    {
        private bool placingTower;
        private bool placingWall;

        public BuildMode(string spriteNameTower, string spriteNameWall)
        {
            placingTower = true;
            placingWall = false;
        }

        public void Update(GameTime gametime)
        {
            //Get the keyboard state
            KeyboardState keystate = Keyboard.GetState();
            
            if (keystate.IsKeyDown(Keys.D1)) //now places towers
            {
                placingTower = true;
                placingWall = false;
            }
            else if (keystate.IsKeyDown(Keys.D2)) //now places walls
            {
                placingTower = false;
                placingWall = true;
            }

            //Checks if building shall be placed or removed and acts.h
            if (InputManager.GetIsMouseButtonPressed(MouseButton.Left))
                PlaceBuilding(InputManager.GetMousePositionVec());
            else if (InputManager.GetIsMouseButtonPressed(MouseButton.Right))
                RemoveBuilding(InputManager.GetMousePositionVec());
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        /// <summary>
        /// Places a building at the given location.
        /// </summary>
        /// <param name="pos"></param>
        public void PlaceBuilding(Vector2 posVec)
        {
            MouseState ms = Mouse.GetState();

            if (GameWorld.Instance.IsActive == false)
            {
                //If the game is not active, don't place a building
            }
            else
            {
                if (ms.X > GameWorld.Instance.GraphicsDevice.Viewport.Bounds.Width 
                    && ms.Y > GameWorld.Instance.GraphicsDevice.Viewport.Bounds.Height)
                {
                    //Don't do anything if you try to place a building outside the grid
                }
                else if (ms.X >= 0 && ms.X < GameWorld.Instance.GraphicsDevice.Viewport.Bounds.Width
                && ms.Y >= 0 && ms.Y < GameWorld.Instance.GraphicsDevice.Viewport.Bounds.Height)
                {
                    //Gets the position on the mapgrid closest to the mouseposition
                    posVec = GameWorld.Instance.Map[posVec].Pos;

                    //Builds the building
                    if (placingTower)
                    {
                        if (GameWorld.Instance.Map[posVec].Occupant == null) //If there's no object on the spot
                        {
                            GameWorld.Instance.gameObjectPool.CreateTower(posVec);
                        }
                        else //If there's an object on the spot
                        {
                            //Do nothing
                        }
                    }
                    else if (placingWall)
                    {
                        if (GameWorld.Instance.Map[posVec].Occupant == null) //Same as above
                        {
                            GameWorld.Instance.gameObjectPool.CreateWall(posVec);
                        }
                        else //Same as above
                        {
                            //Do nothing
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Removes the building at a given location.
        /// </summary>
        /// <param name="pos"></param>
        public void RemoveBuilding(Vector2 posVec)
        {
            if (GameWorld.Instance.Map[posVec].Occupant == null) //If there's no object on the spot
            {
                //Do nothing
            }
            else //If there is an object on the spot; remove it
            {
                GameWorld.Instance.Map.RemoveOccupant(GameWorld.Instance.Map[posVec]);
            }
        }

        public void LoadContent()
        {

        }
    }
}
