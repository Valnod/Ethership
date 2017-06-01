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
        //kommentar på vej
        
        GameState gameState;
        
        SpriteFont font; //font size 100

        public bool controls;
        public bool viewHighscore;

        List<UI> mainWindow = new List<UI>();
        List<UI> menu = new List<UI>();


        public Menu()
        {
            mainWindow.Add(new UI("gui"));
            mainWindow.Add(new UI("menu"));

            menu.Add(new UI("Exit"));
            menu.Add(new UI("mainMenu"));
            menu.Add(new UI("play"));
            menu.Add(new UI("gui"));
            menu.Add(new UI("highscore"));
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

            foreach (UI element in mainWindow)
            {
                element.LoadContent(content);
                element.clickEvent += OnClick;
            }
            foreach (UI element in menu)
            {
                element.LoadContent(content);
                element.clickEvent += OnClick;
            }

            mainWindow.Find(x => x.TextureName == "gui").MoveElement(0, 0);
            mainWindow.Find(x => x.TextureName == "menu").MoveElement(100, 0);

            //function to move the different ui pictures on the screen 
            menu.Find(x => x.TextureName == "gui").MoveElement(0, 0);
            menu.Find(x => x.TextureName == "mainMenu").MoveElement(200, -600);
            menu.Find(x => x.TextureName == "Exit").MoveElement(200, -300);
            menu.Find(x => x.TextureName == "play").MoveElement(200, -500);
            menu.Find(x => x.TextureName == "highscore").MoveElement(200, -400);



        }
        public void Update()
        {
            switch (gameState)
            {
                case GameState.playWindow:
                    foreach (UI element in mainWindow)
                    {
                        element.Update();
                    }
                    break;
                case GameState.menu:
                    foreach (UI element in menu)
                    {
                        element.Update();
                    }
                    break;
                case GameState.quit:
                    foreach (UI element in menu)
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
                case GameState.playWindow:
                    foreach (UI element in mainWindow)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                case GameState.menu:
                    foreach (UI element in menu)
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
                case GameState.highscore:
                    foreach (UI element in menu)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                default:
                    break;
            }

           
        }
        public void OnClick(string button)
        {
            if (button == "play")
            {
                gameState = GameState.playWindow;

            }
            //virker ikke!!!!!
            if (button == "exit" )
            {
                GameWorld.Instance.Exit();

            }
            if (button == "menu" || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                gameState = GameState.menu;
            }
            if (button == "highscore" || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                gameState = GameState.highscore;
            }

        }
    }
}
