using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherShip
{
    class Menu
    {
        public bool controls;
        public bool viewHighscore;
        List<UI> main = new List<UI>();
        public Menu()
        {
            main.Add(new UI("gui"));
            main.Add(new UI("play"));
            main.Add(new UI("Exit"));
        }

        public void SaveGame()
        {

        }
        public void LoadGame()
        {

        }
        public void ToggleSound()
        {

        }
        public void Controls()
        {
            
        }
        public void ViewHighscore()
        {

        }
        public void LoadContent(ContentManager content)
        {
            foreach (UI element in main)
            {
                element.LoadContent(content);
            }

            main.Find(x => x.TextureName == "gui").MoveElement(0, 0, GameWorld.Instance.Window.ClientBounds.Width, 0);
            main.Find(x => x.TextureName == "Exit").MoveElement(50,0,0, 0);
            main.Find(x => x.TextureName == "play").MoveElement(0, 0,0,0);
        }
        public void Upddate()
        {

        }
        public void Quit()
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(UI element in main)
            {
                element.Draw(spriteBatch);
            }
        }
    }
}
