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
        public Texture2D sprite { get; set; }
        public string spriteName { get; set; }
        public float scaleFactor;
        public float Rotation { get; set; }
        public float layerDepth;
        public Rectangle spriteRectangle { get; set; }
        public Vector2 Offset { get; set; }
        private Vector2 origin;

        public SpriteRenderer(GameObject obj, string spriteName, float scaleFactor, float rotation, float layerDepth) : base(obj)
        {
            this.spriteName = spriteName;
            this.scaleFactor = scaleFactor;
            this.Rotation = rotation;
            this.layerDepth = layerDepth;
        }
         
        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>(spriteName);
            origin.X = sprite.Width / 2;
            origin.Y = sprite.Height / 2;
            spriteRectangle = new Rectangle(0, 0, sprite.Width, sprite.Height);
        }

        public void Update(GameTime gameTime)
        {
            //andet projekt har ikke en update i spriteRenderer ...? 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, obj.position, spriteRectangle, Color.White, Rotation, origin, scaleFactor, SpriteEffects.None, layerDepth);
        }
    }
}
