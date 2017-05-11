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
        public Vector2[,] mapGrid;
        public Vector2[,] MapgGrid { get; set; }

        private string spriteName;
        private Texture2D sprite;

        public Map(string spriteName)
        {
            this.spriteName = spriteName;
        }

        public void DrawBackground(SpriteBatch spriteBatch)
        {

        }

        private void GenerateMapGrid()
        {

        }

        public void LoadContent(ContentManager content)
        {

        }
    }
}
