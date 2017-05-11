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
        public string spriteNameTower;
        public string spriteNameWall;
        public Texture2D spriteTower;
        public Texture2D spriteWall;

        public BuildMode(string spriteNameTower, string spriteNameWall)
        {
            spriteNameTower = this.spriteNameTower;
            spriteNameWall = this.spriteNameWall;
        }
        public void Update(GameTime gametime)
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {

        }
        public void PlaceBuilding()
        {

        }
        public void RemoveBuilding()
        {

        }
        public void LoadContent()
        {

        }
    }
}
