using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Threading;

namespace EtherShip
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    class GameWorld : Game
    {

        //##################################################################
        Song song;

    ////####kan ikke lige kommme på hvordan pokker det fúngere fra en anden klasse :s

        public int WindowWidth { get; set; }
        public int WindowHeight { get; set; }
        public SFX SFX { get; set; }
        public int SpriteWidth { get; set; }
        public int SpriteHeight { get; set; }

        public Menu Menu { get; set; }
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private static GameWorld instance;
        public bool betweenRounds;
        private bool buildMode;
        public bool GameOver { get; set; }
        private EndGame endGame;
        private BuildMode build;
        private Texture2D background;

        public Map Map { get; set; }
        public GameObjectPool gameObjectPool;
        public Random rnd;

        public Wave Wave { get; set; }

        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameWorld();
                }
                return instance;
            }
        }

        public bool BetweenRounds
        {
            get
            {
                return betweenRounds;
            }

            set
            {
                betweenRounds = value;
            }
        }

        public bool BuildMode
        {
            get
            {
                return buildMode;
            }

            set
            {
                buildMode = value;
            }
        }

        private GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            WindowWidth = 1280;
            WindowHeight = 720;
            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
            graphics.ApplyChanges();
            this.Window.AllowUserResizing = false;
            SpriteWidth = 400;
            SpriteHeight = 400;

            SFX = new SFX();
            betweenRounds = true;
            GameOver = false;
            buildMode = false;
            build = new BuildMode("circle", "rectangle");
            endGame = new EndGame("rectangle", Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2, 5f);

            //Initializes the map
            Map = new Map("Background");

            //Menu
            Menu = new Menu();

            //Initializes the gameObjectPool
            gameObjectPool = new GameObjectPool();

            //Adds some gameObjects for testing
            gameObjectPool.CreatePlayer();

            //testing waves
            Wave = new Wave(0, 0, Map);
            Wave.Start();

            gameObjectPool.AddToActive();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Menu.LoadContent(Content);

            //MUSIC bad location FIX FIX FIX #########################################
            
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                song = Content.Load<Song>("ebAndFlow");
                MediaPlayer.Play(song);
                MediaPlayer.IsRepeating = true;
                SFX.LoadContent(Content);
            }).Start();
           
            Map.LoadContent(Content);
            Map.Vectorfield(gameObjectPool.player.position);
            endGame.LoadContent(Content);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {            
            // TODO: Add your update logic here

            //Updates mouse state
            InputManager.Update();

            if (!GameOver)
            {
                //Get the keyboard state
                KeyboardState keystate = Keyboard.GetState();

                if (betweenRounds == true)
                {
                    if (keystate.IsKeyDown(Keys.B) && !BuildMode)
                        buildMode = true;
                    else if (keystate.IsKeyDown(Keys.N) && BuildMode)
                        buildMode = false;

                    this.IsMouseVisible = true;
                    if (!buildMode)
                        gameObjectPool.Update(gameTime); //Updates all gameObjects
                }

                if (keystate.IsKeyDown(Keys.P))
                {
                    betweenRounds = false;
                    buildMode = false;
                    this.IsMouseVisible = false;
                }

                if (!betweenRounds)
                {
                    gameObjectPool.Update(gameTime); //Updates all gameObjects
                    Wave.Update(gameTime);
                }
                else if (buildMode)
                    build.Update(gameTime); //Build mode                          

                //Adds and removes GameObjects from the game
                gameObjectPool.RemoveFromActive();
                gameObjectPool.AddToActive();
            }
            else
                endGame.Update(gameTime);
            Menu.Update();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            //Draws the map
            Map.DrawBackground(spriteBatch);
            //draws the menu
            Menu.Draw(spriteBatch);

            //Draws all gameObjects
            gameObjectPool.Draw(spriteBatch);

            //Draws the end game screen
            if (GameOver)
                endGame.Draw(spriteBatch);
         
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
