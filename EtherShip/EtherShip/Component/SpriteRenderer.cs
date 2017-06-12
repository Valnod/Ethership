using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace EtherShip
{
    class SpriteRenderer : Component, IDrawable, IUpdateable, Iloadable
    {
        public Texture2D Sprite { get; set; }
        public string SpriteName { get; set; }
        public float scaleFactor;
        public float Rotation { get; set; }
        public float layerDepth;
        public Rectangle SpriteRectangle { get; set; }
        public Vector2 Offset { get; set; }
        private Vector2 origin;
        public Color Color { get; set; }
        public Rectangle SpriteRectangleForCollision
        {
            get { return new Rectangle(0, 0, (int)(Sprite.Width * scaleFactor), (int)(Sprite.Height * scaleFactor)); }
        }

        public SpriteRenderer(GameObject obj, string spriteName, float scaleFactor, float rotation, float layerDepth) : base(obj)
        {
            this.SpriteName = spriteName;
            this.scaleFactor = scaleFactor;
            this.Rotation = rotation;
            this.layerDepth = layerDepth;
            this.Color = Color.White;
        }
         
        public void LoadContent(ContentManager content)
        {
            Sprite = content.Load<Texture2D>(SpriteName);
            origin.X = Sprite.Width / 2;
            origin.Y = Sprite.Height / 2;
            SpriteRectangle = new Rectangle(0, 0, Sprite.Width, Sprite.Height);
        }

        public void Update(GameTime gameTime)
        {
            //andet projekt har ikke en update i spriteRenderer ...? 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, obj.position, SpriteRectangle, Color, Rotation, origin, scaleFactor, SpriteEffects.None, layerDepth);
        }
    }
}
