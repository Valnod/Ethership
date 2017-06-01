using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherShip
{
    class Menu
    {
        enum GameState { mainMenuen, play, quit }
        GameState gameState;

        public bool controls;
        public bool viewHighscore;

        List<UI> main = new List<UI>();
        List<UI> menu = new List<UI>();


        public Menu()
        {
            main.Add(new UI("gui"));
            main.Add(new UI("menu"));


            menu.Add(new UI("Exit"));
            menu.Add(new UI("mainMenu"));
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
                element.clickEvent += OnClick;
            }
            foreach (UI element in menu)
            {
                element.LoadContent(content);
                element.clickEvent += OnClick;
            }

            main.Find(x => x.TextureName == "gui").MoveElement(0, 0);
            main.Find(x => x.TextureName == "menu").MoveElement(100, 0);

            menu.Find(x => x.TextureName == "mainMenu").MoveElement(200, 0);
            menu.Find(x => x.TextureName == "Exit").MoveElement(300, 0);

           
         
        }
        public void Update()
        {
            switch (gameState)
            {
                case GameState.mainMenuen:
                    foreach (UI element in main)
                    {
                         element.Update();
                    }
                    break;
                case GameState.quit:
                    foreach(UI element in menu)
                    {
                        element.Update();
                    }
                    break;
                default:
                    break;
            }
          
            
        }
       
        public void Draw(SpriteBatch spriteBatch)
        {
            switch (gameState)
            {
                case GameState.mainMenuen:
                    foreach (UI element in main)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                case GameState.quit:
                    foreach (UI element in menu)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                default:
                    break;
            }

            foreach (UI element in main)
            {
                element.Draw(spriteBatch);
            }
        }
        public void OnClick(string button)
        {
          
            if (button == "exit" || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                GameWorld.Instance.Exit();
                
            }
            if (button == "menu")
            {
                gameState = GameState.mainMenuen;
            }
            
        }
    }
}
