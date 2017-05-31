using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace EtherShip
{
    class UI
    {
        private Texture2D backGround;
        private Rectangle backGRectangle;
        private Texture2D uiTexture;
        private Rectangle uiRectangle;
        private string textureName;

        public string TextureName
        {
            get { return textureName; }
            set { textureName = value; }
        }

        public delegate void ElementClicked(string element);

        public event ElementClicked ClickEvent;

        public UI(string textureName)
        {
            this.TextureName = textureName;
        } 
        public void LoadContent(ContentManager content)
        {
            uiTexture = content.Load<Texture2D>(TextureName);
            backGRectangle = new Rectangle(0, 600, GameWorld.Instance.Window.ClientBounds.Width, backGround.Height / 2);
            uiRectangle = new Rectangle(0, 600, uiTexture.Width / 2, uiTexture.Height / 2 );
             
        }
        public void Update()
        {
            if (uiRectangle.Contains(new Point(Mouse.GetState().X,Mouse.GetState().Y))&& Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                ClickEvent(TextureName);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(uiTexture, uiRectangle, Color.White);
        }
        public void MoveElement(int x, int y)
        {

            uiRectangle = new Rectangle(uiRectangle.X += x, uiRectangle.Y += y, uiRectangle.Width, uiRectangle.Height);
        }

    }
}
