using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Focus.globals;
using Focus.entities;

namespace Focus {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        internal GameScene currentScene;

        bool menu = true;
        Background menuBackground;
        Button menuButton;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            Window.Title = "Focus";
            this.IsMouseVisible = true;
            GV.Current = this;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here

            base.Initialize();
            GV.contentManager = Content;
            menuBackground = new Background("Title");
            menuButton = new Button("Play Game", new Vector2());

            LoadLevel(7);
        }

        internal void LoadLevel(int level)
        {

            currentScene = new GameScene(level);
            currentScene.CreateRenderTargets(GraphicsDevice);
        }

        internal void LoadLevelUp(int level)
        {
            switch (level)
            {
                case 7:
                    level = 9;
                    break;
                case 8:
                    level = 7;
                    break;
                case 10:
                    level = 11;
                    break;
                case 11:
                    level = 12;
                    break;
            }
            LoadLevel(level);
        }

        internal void LoadLevelDown(int level)
        {
            switch (level)
            {
                case 7:
                    level = 8;
                    break;
                case 9:
                    level = 7;
                    break;
                case 12:
                    level = 11;
                    break;
                case 11:
                    level = 10;
                    break;
            }
            LoadLevel(level);
        }

        internal void LoadLevelRight(int level)
        {
            switch (level)
            {
                case 9:
                    level = 10;
                    break;
            }
            LoadLevel(level);
        }

        internal void LoadLevelLeft(int level)
        {
            switch (level)
            {
                case 10:
                    level = 9;
                    break;
            }
            LoadLevel(level);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            // Allows the game to exit
            Input.Update();
            if (Input.isKeyDown(Keys.Escape))
                this.Exit();
            if (menu) {
                menuBackground.Update();
                menuButton.Update();

                if (menuButton.clickReleased) {
                    menu = false;
                }
            }
            else {
                currentScene.Update(gameTime);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {


            GraphicsDevice.Clear(Color.Black);
            
            if (menu) {
                spriteBatch.Begin();
                menuBackground.Draw(spriteBatch);
                menuButton.Draw(spriteBatch);
                spriteBatch.End();
            }
            else {
                currentScene.Draw(gameTime, GraphicsDevice, spriteBatch);
            }

            base.Draw(gameTime);
        }
    }
}
