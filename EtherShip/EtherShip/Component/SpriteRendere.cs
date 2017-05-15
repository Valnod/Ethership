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
        public Rectangle SpriteRectangle { get; set; }
        public Texture2D Sprite { get; private set; }
        public Color Color { get; set; } = Color.White;

        public string spriteName;
        public float scaleFactor;

        public SpriteRendere(GameObject obj, string spriteName, float scaleFactor) : base(obj)
        {
            this.spriteName = spriteName;
            this.scaleFactor = scaleFactor;
        }
         
        public void LoadContent(ContentManager content)
        {
            Sprite = content.Load<Texture2D>(spriteName);

            this.SpriteRectangle = new Rectangle(0, 0, Sprite.Width, Sprite.Height);
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, SpriteRectangle, Color);
        }
    }
}
