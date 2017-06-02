using Microsoft.Xna.Framework;
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
        List<UI> highscore = new List<UI>();


        public Menu()
        {
            mainWindow.Add(new UI("gui"));
            mainWindow.Add(new UI("menu"));

            
            menu.Add(new UI("mainMenu"));
            menu.Add(new UI("resume"));
            menu.Add(new UI("gui"));
            menu.Add(new UI("highscore"));
            menu.Add(new UI("Exit"));


            highscore.Add(new UI("highscoremenu"));
            highscore.Add(new UI("resume"));



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
            foreach (UI element in highscore)
            {
                element.LoadContent(content);
                element.clickEvent += OnClick;
            }

            mainWindow.Find(x => x.TextureName == "gui").MoveElement(0, 0);
            mainWindow.Find(x => x.TextureName == "menu").MoveElement(70, 30);

            //function to move the different ui pictures on the screen 
            menu.Find(x => x.TextureName == "gui").MoveElement(0, 0);
            menu.Find(x => x.TextureName == "mainMenu").MoveElement(500, -600);
            menu.Find(x => x.TextureName == "resume").MoveElement(550, -590);
            menu.Find(x => x.TextureName == "highscore").MoveElement(550, -540);
            menu.Find(x => x.TextureName == "Exit").MoveElement(550, -490);


            highscore.Find(x => x.TextureName == "highscoremenu").MoveElement(500, -600);
            highscore.Find(x => x.TextureName == "resume").MoveElement(500, -400);


            font = content.Load<SpriteFont>("font");

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
                case GameState.highscore:
                    foreach (UI element in highscore)
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
                    List<HighScoreUnit> scores = HighScore.Instance.Scores;
                    int length = scores.Count;
                    if (length > 10 )
                    {
                        length = 10;
                    }

                    foreach (UI element in highscore)
                    {
                        element.Draw(spriteBatch);

                        if (element == highscore[0])
                        {
                            for (int i = 0; i < length; i++)
                            {
                                Vector2 pos = new Vector2(element.uiRectangle.X + 100, (element.uiRectangle.Y + 20) + i * 20);
                                spriteBatch.DrawString(font, scores[i].Name + " - " + scores[i].Score, pos, Color.Black);
                            }
                        }
                    }
                    break;

                default:
                    break;
            }

           
        }
        public void OnClick(string button)
        {
            if (button == "resume")
            {
                gameState = GameState.playWindow;

            }
            //virker ikke!!!!!!
            if (button == "exit" )
            {
                GameWorld.Instance.Exit();

            }
            if (button == "menu" || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                gameState = GameState.menu;
            }
            if (button == "highscore" )
            {
                gameState = GameState.highscore;
            }

        }
    }
}
