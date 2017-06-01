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
    class EndGame
    {
        private int buttonX, buttonY;
        private Rectangle rectangle;
        private Rectangle buttonRect;
        private Vector2 pos;
        private Texture2D sprite;
        private string spriteName;
        private Color color;
        private SpriteFont font;
        private string text;
        private string text2 = "Enter your name:";
        private int charSize;
        private float scaleFactor;
        private float textSizeScaleFactor = 0.1f;

        public MouseState mouseInput;
        public int ButtonX { get; }
        public int ButtonY { get; }
        public string PlayerName { get { return text; } }
        public SpriteFont Font { get; }

        Keys[] keysToCheck = new Keys[] {
    Keys.A, Keys.B, Keys.C, Keys.D, Keys.E,
    Keys.F, Keys.G, Keys.H, Keys.I, Keys.J,
    Keys.K, Keys.L, Keys.M, Keys.N, Keys.O,
    Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T,
    Keys.U, Keys.V, Keys.W, Keys.X, Keys.Y,
    Keys.Z, Keys.Back, Keys.Space };
        KeyboardState currentKeyboardState;
        KeyboardState lastKeyboardState;

        public EndGame(string spriteName, int buttonX, int buttonY, float scaleFactor)
        {
            this.spriteName = spriteName;
            this.buttonX = buttonX;
            this.buttonY = buttonY;
            this.scaleFactor = scaleFactor;
            color = Color.White;
            text = "";
            charSize = (int)(30 * 0.1f);
        }

        public void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Font");
            sprite = content.Load<Texture2D>(spriteName);
            rectangle = new Rectangle(0, 0, (int)(sprite.Width * scaleFactor), (int)(sprite.Height * scaleFactor));
            buttonRect = new Rectangle(buttonX - (int)(sprite.Width * scaleFactor) / 2, buttonY - (int)(sprite.Height * scaleFactor) / 2,
                (int)(sprite.Width * scaleFactor), (int)(sprite.Height * scaleFactor));
        }

        public void Update(GameTime gameTime)
        {
            if (GameWorld.Instance.GameOver)
                GameWorld.Instance.IsMouseVisible = true; //Makes the mouse visible within the gamewindow

            Point mousePosition = InputManager.GetMousePositionPoint();

            //Checks if the mouse position is inside the rectangle
            if (buttonRect.Contains(mousePosition))
                color = Color.White;
            else
                color = Color.Black;

            //What happens when button is pressed
            if (InputManager.GetHasMouseButtonBeenReleased(MouseButton.Left) && buttonRect.Contains(mousePosition))
                enterButton();

            //Used to write the player name
            currentKeyboardState = Keyboard.GetState();
            foreach (Keys key in keysToCheck)
            {
                if (CheckKey(key))
                {
                    AddKeyToText(key);
                    break;
                }
            }
            lastKeyboardState = currentKeyboardState;
        }

        /// <summary>
        /// Starts the game and adds the players name to the score list.
        /// </summary>
        /// <returns></returns>
        public void enterButton()
        {
            //makes all gameObjects inactive, so the game is ready to start anew.
            GameWorld.Instance.gameObjectPool.ClearLists();

            //Adds the player
            GameWorld.Instance.gameObjectPool.CreatePlayer();

            //Resests the wave to the first wave
            GameWorld.Instance.Wave.WaveNumber = 1;

            //Names the player acordin to the input
            HighScore.Instance.AddScore(new HighScoreUnit(text, GameWorld.Instance.gameObjectPool.player.GetComponent<Player>().Score));

            //Changes the state of the game
            GameWorld.Instance.GameOver = false;
            GameWorld.Instance.BetweenRounds = true;
            GameWorld.Instance.GameOver = false;
            GameWorld.Instance.BuildMode = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            pos = new Vector2(buttonX - (sprite.Width / 2) * scaleFactor, buttonY - (sprite.Height / 2) * scaleFactor);
            spriteBatch.Draw(sprite, pos, rectangle, color, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

            pos = new Vector2(buttonX - text2.Count() * charSize, buttonY - 140);
            spriteBatch.DrawString(font, text2, pos, Color.Black, 0, Vector2.Zero, textSizeScaleFactor, SpriteEffects.None, 1);
            pos = new Vector2(buttonX - text.Count() * charSize, buttonY - 100);
            spriteBatch.DrawString(font, text, pos, Color.Black, 0, Vector2.Zero, textSizeScaleFactor, SpriteEffects.None, 1);
        }

        /// <summary>
        /// Adds the pressed keys corrosponding letter to the string text.
        /// </summary>
        /// <param name="key"></param>
        private void AddKeyToText(Keys key)
        {
            string newChar = "";

            if (text.Length >= 200 && key != Keys.Back) //The number indicates the max number of characters the string text can be.
                return;

            switch (key)
            {
                case Keys.A:
                    newChar += "a";
                    break;
                case Keys.B:
                    newChar += "b";
                    break;
                case Keys.C:
                    newChar += "c";
                    break;
                case Keys.D:
                    newChar += "d";
                    break;
                case Keys.E:
                    newChar += "e";
                    break;
                case Keys.F:
                    newChar += "f";
                    break;
                case Keys.G:
                    newChar += "g";
                    break;
                case Keys.H:
                    newChar += "h";
                    break;
                case Keys.I:
                    newChar += "i";
                    break;
                case Keys.J:
                    newChar += "j";
                    break;
                case Keys.K:
                    newChar += "k";
                    break;
                case Keys.L:
                    newChar += "l";
                    break;
                case Keys.M:
                    newChar += "m";
                    break;
                case Keys.N:
                    newChar += "n";
                    break;
                case Keys.O:
                    newChar += "o";
                    break;
                case Keys.P:
                    newChar += "p";
                    break;
                case Keys.Q:
                    newChar += "q";
                    break;
                case Keys.R:
                    newChar += "r";
                    break;
                case Keys.S:
                    newChar += "s";
                    break;
                case Keys.T:
                    newChar += "t";
                    break;
                case Keys.U:
                    newChar += "u";
                    break;
                case Keys.V:
                    newChar += "v";
                    break;
                case Keys.W:
                    newChar += "w";
                    break;
                case Keys.X:
                    newChar += "x";
                    break;
                case Keys.Y:
                    newChar += "y";
                    break;
                case Keys.Z:
                    newChar += "z";
                    break;
                case Keys.Space:
                    newChar += " ";
                    break;
                case Keys.Back:
                    if (text.Length != 0)
                        text = text.Remove(text.Length - 1);
                    return;
            }
            if (currentKeyboardState.IsKeyDown(Keys.RightShift) ||
                currentKeyboardState.IsKeyDown(Keys.LeftShift))
            {
                newChar = newChar.ToUpper();
            }
            text += newChar;
        }

        private bool CheckKey(Keys theKey)
        {
            return lastKeyboardState.IsKeyDown(theKey) && currentKeyboardState.IsKeyUp(theKey);
        }


    }

}
