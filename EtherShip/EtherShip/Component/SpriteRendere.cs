using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherShip
{
    class SpriteRendere : Component, IDrawable, IUpdateable
    {
        public Texture2D sprite;
        public string spriteName;
        public float scaleFactor;
        public Rectangle spriteRectangle;

        public SpriteRendere(GameObject obj, Texture2D sprite, string spriteName, float scaleFactor, Rectangle spriteRectangle) : base(obj)
        {
            this.sprite = sprite;
            this.spriteName = spriteName;
            this.scaleFactor = scaleFactor;
        }
         
        public void LoadContent(ContentManager content)
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
