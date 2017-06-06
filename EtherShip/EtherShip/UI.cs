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
        SpriteFont font; //font size 100
        private Texture2D uiTexture;
        public Rectangle uiRectangle;
        private string textureName;

        public delegate void ElementClicked(string element);

        public event ElementClicked clickEvent;

        public string TextureName
        {
            get { return textureName; }
            set { textureName = value; }
        }



        public UI(string textureName)
        {
            this.TextureName = textureName;
        }

        public void LoadContent(ContentManager content)
        {
            uiTexture = content.Load<Texture2D>(textureName);

            uiRectangle = new Rectangle(0, 600, uiTexture.Width, uiTexture.Height / 2);

            font = content.Load<SpriteFont>("font");


        }
        public void Update()
        {
            if (uiRectangle.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                clickEvent(textureName);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(uiTexture, uiRectangle, Color.White);
            spriteBatch.DrawString(font, "$" + GameWorld.Instance.gameObjectPool.player.GetComponent<Player>().Credit, new Vector2(100, 650), Color.Black);
            spriteBatch.DrawString(font, "Wave " + GameWorld.Instance.Wave.WaveNumber, new Vector2(1100, 650), Color.Black);
            spriteBatch.DrawString(font,"" + GameWorld.Instance.gameObjectPool.player.GetComponent<Player>().Score, new Vector2(600, 650), Color.Black);
        }
        public void MoveElement(int x, int y)
        {
            uiRectangle = new Rectangle(uiRectangle.X += x, uiRectangle.Y += y, uiRectangle.Width, uiRectangle.Height);

        }
    }
}
