using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherShip
{
    class Map : Iloadable
    {
        public GridPoint[,] MapGrid { get; set; }
        public int GridPointSize { get; set; }

        private string spriteName;
        private Texture2D sprite;
        private Rectangle sourceRect;

        private Texture2D pointSprite;
        private Rectangle sourceRectPoint;

        public Map(string spriteName)
        {
            this.spriteName = spriteName;
            this.GridPointSize = 25;
            GenerateMapGrid();
        }

        //returns the gridpoint that is closest to the position
        public GridPoint this[Vector2 position]
        {
            get
            {
                return MapGrid[(int)(position.X / GridPointSize), (int)(position.Y / GridPointSize)];
            }
        }
        /// <summary>
        /// Draws the background of the game.
        /// </summary>
        /// <param name="spriteBatch"></param> 
        public void DrawBackground(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(sprite, Vector2.Zero, sourceRect, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);

#if DEBUG //draws the points which makes up the grid
            foreach(GridPoint gp in MapGrid)
            {
                spriteBatch.Draw(pointSprite, gp.Pos, sourceRectPoint, gp.Color, 1f, Vector2.Zero, 1f, SpriteEffects.None, 1);
            }
#endif
        }

        /// <summary>
        /// Generates the grid
        /// </summary>
        private void GenerateMapGrid()
        {
            int xGridAmount = GameWorld.Instance.Window.ClientBounds.Width / GridPointSize;
            int yGridAmount = GameWorld.Instance.Window.ClientBounds.Height / GridPointSize;

            MapGrid = new GridPoint[xGridAmount, yGridAmount];

            for (int x = 0; x < xGridAmount; x++)
            {
                for (int y = 0; y < yGridAmount; y++)
                {
                    MapGrid[x, y] = new GridPoint(new Vector2(x * GridPointSize + (GridPointSize / 2), y * GridPointSize + (GridPointSize / 2)), null);
                }
            }
        }

        /// <summary>
        /// Loads the content needed for the class.
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
#if DEBUG  //Texture and rect to draw the point, which makes up the grid
            pointSprite = content.Load<Texture2D>("rectangle");
            sourceRectPoint = new Rectangle(0, 0, 2, 2);
#endif
            //Draws the background
            //sprite = content.Load<Texture2D>("Background");
            //sourceRect = new Rectangle(0, 0, sprite.Width, sprite.Height);
        }

        public void RemoveOccupant(GridPoint gridPoint)
        {
            for(int x = 0; x < MapGrid.GetLength(0); x++)
            {
                for(int y = 0; y < MapGrid.GetLength(1); y++)
                {
                    if(MapGrid[x, y] == gridPoint)
                    {
                        GameWorld.Instance.gameObjectPool.RemoveActive.Add(MapGrid[x, y].Occupant);
                        MapGrid[x, y].Occupant = null;
                    }
                }
            }
        }
    }
}
