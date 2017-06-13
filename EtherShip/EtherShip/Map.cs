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
        public int XGridAmount { get; set; }
        public int YGridAmount { get; set; }

        private List<GridPoint> gridPoints;
        private List<GridPoint> tempGridPoints;

        private string spriteName;
        private Texture2D sprite;
        private Rectangle sourceRect;

        private Texture2D pointSprite;
        private Rectangle sourceRectPoint;
        private SpriteFont font;

        public Map(string spriteName)
        {
            this.spriteName = spriteName;
            this.GridPointSize = 50;
            gridPoints = new List<GridPoint>();
            tempGridPoints = new List<GridPoint>();
        }

        //Returns the gridpoint that is closest to the position
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
          spriteBatch.Draw(sprite, Vector2.Zero, sourceRect, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);

#if DEBUG //draws the points which makes up the grid
            foreach(GridPoint gp in MapGrid)
                spriteBatch.Draw(pointSprite, gp.Pos, sourceRectPoint, gp.Color, 1f, Vector2.Zero, 1f, SpriteEffects.None, 1);

            foreach (GridPoint gp in MapGrid)
                spriteBatch.DrawString(font, "" + gp.Heat, gp.Pos, Color.Red, 1f, Vector2.Zero, 1f, SpriteEffects.None, 1);
#endif
        }

        /// <summary>
        /// Generates the grid
        /// </summary>
        private void GenerateMapGrid()
        {
            XGridAmount = GameWorld.Instance.Window.ClientBounds.Width / GridPointSize;
            YGridAmount = (GameWorld.Instance.Window.ClientBounds.Height - GameWorld.Instance.Menu.GetUIHeight()) / GridPointSize; //The number 100 is an adjustment so the grid wont go down behind the UI.

            // - GameWorld.Instance.Menu.UIHeight()

            MapGrid = new GridPoint[XGridAmount, YGridAmount];

            for (int x = 0; x < XGridAmount; x++)
            {
                for (int y = 0; y < YGridAmount; y++)
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
            font = content.Load<SpriteFont>("Font");
#endif
            GenerateMapGrid();

            //Draws the background
            sprite = content.Load<Texture2D>("starBackground");
            sourceRect = new Rectangle(0, 0, sprite.Width, sprite.Height);
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
                        if (MapGrid[x, y].Occupant.GetComponent<Tower>() != null)
                            GameWorld.Instance.gameObjectPool.player.GetComponent<Player>().Credit += 40;
                        if (MapGrid[x, y].Occupant.GetComponent<Wall>() != null)
                            GameWorld.Instance.gameObjectPool.player.GetComponent<Player>().Credit += 4;
                        MapGrid[x, y].Occupant = null;
                    }
                }
            }
        }



        public void Vectorfield(Vector2 pos)
        {
            int x = (int)(pos.X / GridPointSize);
            int y = (int)(pos.Y / GridPointSize);
            GridPoint gridPos = MapGrid[x, y];
            gridPos.Heat = 0;

            if (x < MapGrid.GetLength(0) && x > 0 && (y + 1) < MapGrid.GetLength(1) && (y + 1) > 0 && MapGrid[x, y + 1].Occupant == null && MapGrid[x, y + 1].Heat == 0)
            {
                MapGrid[x, y + 1].Heat = 1;
                gridPoints.Add(MapGrid[x, y + 1]);
            }
            if (x < MapGrid.GetLength(0) && x > 0 && (y - 1) < MapGrid.GetLength(1) && (y - 1) > 0 && MapGrid[x, y - 1].Occupant == null && MapGrid[x, y - 1].Heat == 0)
            {
                MapGrid[x, y - 1].Heat = 1;
                gridPoints.Add(MapGrid[x, y - 1]);
            }
            if ((x + 1) < MapGrid.GetLength(0) && (x + 1) > 0 && y < MapGrid.GetLength(1) && y > 0 && MapGrid[x + 1, y].Occupant == null && MapGrid[x + 1, y].Heat == 0)
            {
                MapGrid[x + 1, y].Heat = 1;
                gridPoints.Add(MapGrid[x + 1, y]);
            }
            if ((x - 1) < MapGrid.GetLength(0) && (x - 1) > 0 && y < MapGrid.GetLength(1) && y > 0 && MapGrid[x - 1, y].Occupant == null && MapGrid[x - 1, y].Heat == 0)
            {
                MapGrid[x - 1, y].Heat = 1;
                gridPoints.Add(MapGrid[x - 1, y]);
            }


            VectorfieldSlave(gridPoints, 1);
        }

        public void VectorfieldSlave(List<GridPoint> gridPoints, int heat)
        {
            for(int i = 0; i < gridPoints.Count; i++)
            {
                int x = (int)(gridPoints[i].Pos.X / GridPointSize);
                int y = (int)(gridPoints[i].Pos.X / GridPointSize);

                if (x < MapGrid.GetLength(0) && x > 0 && (y + 1) < MapGrid.GetLength(1) && (y + 1) > 0 && MapGrid[x, y + 1].Occupant == null && MapGrid[x, y + 1].Heat == 0)
                {
                    MapGrid[x, y + 1].Heat = heat + 1;
                    tempGridPoints.Add(MapGrid[x, y + 1]);
                }
                if (x < MapGrid.GetLength(0) && x > 0 && (y - 1) < MapGrid.GetLength(1) && (y - 1) > 0 && MapGrid[x, y - 1].Occupant == null && MapGrid[x, y - 1].Heat == 0)
                {
                    MapGrid[x, y - 1].Heat = heat + 1;
                    tempGridPoints.Add(MapGrid[x, y - 1]);
                }
                if ((x + 1) < MapGrid.GetLength(0) && (x + 1) > 0 && y < MapGrid.GetLength(1) && y > 0 && MapGrid[x + 1, y].Occupant == null && MapGrid[x + 1, y].Heat == 0)
                {
                    MapGrid[x + 1, y].Heat = heat + 1;
                    tempGridPoints.Add(MapGrid[x + 1, y]);
                }
                if ((x - 1) < MapGrid.GetLength(0) && (x - 1) > 0 && y < MapGrid.GetLength(1) && y > 0 && MapGrid[x - 1, y].Occupant == null && MapGrid[x - 1, y].Heat == 0)
                {
                    MapGrid[x - 1, y].Heat = heat + 1;
                    tempGridPoints.Add(MapGrid[x - 1, y]);
                }
            }

            gridPoints.Clear();
            if(tempGridPoints.Count > 0)
            {
                for (int i = 0; i < tempGridPoints.Count; i++)
                    gridPoints.Add(tempGridPoints[i]);
                tempGridPoints.Clear();
                VectorfieldSlave(gridPoints, heat + 1);
            }
        }
    }
}
