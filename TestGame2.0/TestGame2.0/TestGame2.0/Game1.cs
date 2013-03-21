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
using TestGame2._0.GameScreens;

namespace TestGame2._0
{

    enum Screen
    {
        MainMenu,
        MainGame,
        GameOver
    }


    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        MainGame mainGame;
        MainMenu mainMenu;
        SpriteBatch spriteBatch;
        Screen currentScreen;
        GraphicsDeviceManager graphics;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Window.Title = "WordCraft";
            graphics.IsFullScreen = false;
            //set window size
            graphics.PreferredBackBufferHeight = 701;
            graphics.PreferredBackBufferWidth = 401;
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
            mainMenu = new MainMenu(this);
            currentScreen = Screen.MainMenu;
            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            switch (currentScreen)
            {
                case Screen.MainMenu:
                    if (mainMenu != null)
                    {
                        mainMenu.Update();
                    }
                    break;
                case Screen.MainGame:
                    if (mainGame != null)
                    {
                        mainGame.Update(gameTime);
                    }
                    break;
            }

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
            //spriteBatch.Draw(background, mainFrame, Color.White);
            switch (currentScreen)
            {
                case Screen.MainMenu:
                    if (mainMenu != null)
                    {
                        mainMenu.Draw(spriteBatch);
                    }
                    break;
                case Screen.MainGame:
                    if (mainGame != null)
                    {
                        mainGame.Draw(spriteBatch);
                    }
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void StartGame()
        {
            mainGame = new MainGame(this);
            currentScreen = Screen.MainGame;
            mainMenu = null;
        }
    }
}
