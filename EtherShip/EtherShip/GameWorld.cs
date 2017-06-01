using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace EtherShip
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    class GameWorld : Game
    {
        Menu menu = new Menu();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private static GameWorld instance;
        private bool betweenRounds;
        private bool buildMode;
        private BuildMode build;

        public Map Map { get; set; }
        public GameObjectPool gameObjectPool;
        public Random rnd;

        private Vector2 posti = new Vector2(0, 610);
        public int[] uiWall = new int[42];

        Wave wave;

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
        
            ////set the GraphicsDeviceManager's fullscreen property
            //graphics.ToggleFullScreen();
            ////Makes the window borderless
            //Window.IsBorderless = true;
            //Changes the windw resolution
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
            this.Window.AllowUserResizing = true;

            betweenRounds = true;
            buildMode = false;
            build = new BuildMode("circle", "rectangle");

            //Initializes the map
            Map = new Map("Background");

            //Initializes the gameObjectPool
            gameObjectPool = new GameObjectPool();

            //Adds some gameObjects for testing
            gameObjectPool.CreatePlayer();
            //enemy is out for testing
            //gameObjectPool.CreateEnemy();
            /*foreach (int wall in uiWall)
            {
                AddWall();
            }*/           

            //testing waves
            wave = new Wave(0, 100, Map);
            wave.Start();

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
            menu.LoadContent(Content);
            Map.LoadContent(Content);
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
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();
            
            // TODO: Add your update logic here

            //Get the keyboard state
            KeyboardState keystate = Keyboard.GetState();

          
            if(betweenRounds == true)
                {
                if (keystate.IsKeyDown(Keys.B))
                    buildMode = true;
                else if(keystate.IsKeyDown(Keys.N))
                    buildMode = false;

                this.IsMouseVisible = true;
                if(!buildMode)
                    gameObjectPool.Update(gameTime); //Updates all gameObjects

            }
                
            
            if (keystate.IsKeyDown(Keys.P) )
                {
                     betweenRounds = false;
                     buildMode = false;
                     this.IsMouseVisible = false;
                }

            //Updates mouse state
            InputManager.Update();

            if (!betweenRounds)
            {
                gameObjectPool.Update(gameTime); //Updates all gameObjects
                wave.Update(gameTime);
            }
            else if (buildMode)
            {
                build.Update(gameTime); //Build mode             
            }
            
            //Adds and removes GameObjects from the game
            gameObjectPool.RemoveFromActive();
            gameObjectPool.AddToActive();        

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
            menu.Draw(spriteBatch);

            //Draws all gameObjects
            gameObjectPool.Draw(spriteBatch);

         
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void AddWall()
        {
            gameObjectPool.CreateWall(new Vector2(posti.X, posti.Y));
            posti.X += 31;
            uiWall[0] += 1;
        }
    }
}
