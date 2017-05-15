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
    class Map
    {
        public GridPoint[,] MapgGrid { get; set; }

        private string spriteName;
        private Texture2D sprite;
        private Rectangle sourceRect;
        private int xGridPointAmount = 10;
        private int yGridPointAmount = 10;

        public Map(string spriteName)
        {
            this.spriteName = spriteName;

            MapgGrid = new GridPoint[xGridPointAmount, yGridPointAmount];
            GenerateMapGrid();
        }

        public void DrawBackground(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(sprite, Vector2.Zero, sourceRect, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);

#if DEBUG //draws the points which makes up the grid
            foreach(GridPoint gp in MapgGrid)
            {
                Texture2D tempRect = new Texture2D(GameWorld.Instance.GraphicsDevice, 1, 1);
                spriteBatch.Draw(tempRect, gp.Pos, Color.Black);
            }
#endif

        }

        private void GenerateMapGrid()
        {
            int xSize = GameWorld.Instance.Window.ClientBounds.Width / xGridPointAmount;
            int ySize = GameWorld.Instance.Window.ClientBounds.Height / yGridPointAmount;

            for (int x = 0; x < xGridPointAmount; x++)
            {
                for(int y = 0; y < yGridPointAmount; y++)
                {
                    MapgGrid[x, y] = new GridPoint(new Vector2(x * xSize + (xSize / 2), y * ySize), null);
                }
            }
        }

        public void LoadContent(ContentManager content)
        {
            //sprite = content.Load<Texture2D>("Background");
            //sourceRect = new Rectangle(0, 0, sprite.Width, sprite.Height);
        }
    }
}
