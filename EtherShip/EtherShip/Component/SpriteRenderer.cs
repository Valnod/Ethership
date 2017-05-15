﻿using System;
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
    class SpriteRenderer : Component, IDrawable, IUpdateable
    {
        public Texture2D sprite { get; set; }
        public string spriteName { get; set; }
        public float scaleFactor;
        public Rectangle spriteRectangle { get; set; }
        public float layerDepth;



        public SpriteRenderer(GameObject obj, Texture2D sprite, string spriteName, float scaleFactor, Rectangle spriteRectangle, float layerDepth) : base(obj)
        {
            this.sprite = sprite;
            this.spriteName = spriteName;
            this.scaleFactor = scaleFactor;
            this.layerDepth = layerDepth;
        }
         
        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>(spriteName);
            spriteRectangle = new Rectangle(0, 0, sprite.Width, sprite.Height);
        }

        public void Update(GameTime gameTime)
        {
            //andet projekt har ikke en update i spriteRenderer? 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, obj.position, spriteRectangle, Color.White, 1f, Vector2.Zero, scaleFactor, SpriteEffects.None, layerDepth);
        }
    }
}
