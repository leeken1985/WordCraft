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
using TestGame2._0.Backend;
using System.Collections;

namespace TestGame2._0
{

    enum Screen
    {
        MainMenu,
        MainGame,
        GameOver,
        PauseScreen,
        Instructions
    }


    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        MainGame mainGame;
        MainMenu mainMenu;
        Instructions instruct;
        GameOver gameOver;
        MainGame pausedGame;
        PauseScreen pauseScreen;
        SpriteBatch spriteBatch;
        Texture2D background;
        Screen currentScreen;
        GraphicsDeviceManager graphics;
        public int totalScore;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Window.Title = "WordCraft";
            graphics.IsFullScreen = false;
            //set window size
            graphics.PreferredBackBufferHeight = 751;
            graphics.PreferredBackBufferWidth = 701;
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

            //Load background
            background = Content.Load<Texture2D>("background");

            // TODO: use this.Content to load your game content here
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
                case Screen.PauseScreen:
                    if (pauseScreen != null)
                    {
                        pauseScreen.Update(gameTime);
                    }
                    break;
                case Screen.GameOver:
                    if (gameOver != null)
                    {
                        gameOver.Update(gameTime);
                    }
                    break;
                case Screen.Instructions:
                    if (instruct != null)
                    {
                        instruct.Update();
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
                case Screen.PauseScreen:
                    if (pauseScreen != null)
                    {
                        pauseScreen.Draw(spriteBatch);
                    }
                    break;
                case Screen.GameOver:
                    if (gameOver != null)
                    {
                        gameOver.Draw(spriteBatch);
                    }
                    break;
                case Screen.Instructions:
                    if (instruct != null)
                    {
                        instruct.Draw(spriteBatch);
                    }
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Starts a new game and sets main menu screen to be displayed.
        /// </summary>
        public void StartGame()
        {
            mainGame = new MainGame(this);
            currentScreen = Screen.MainGame;
            mainMenu = null;
        }

        /// <summary>
        /// Ends the game and displays game over screen.
        /// </summary>
        public void endGame()
        {
            gameOver= new GameOver(this);
            currentScreen = Screen.GameOver;
            mainGame = null;
        }

        ///
        public void restartGame()
        {
            mainMenu = new MainMenu(this);
            currentScreen = Screen.MainMenu;
            gameOver = null;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    GameArea.SetGameBoard(j, i, 0);
                }
            }
            PauseScreen.list = new ArrayList { "", "", "", "", "" };
        }
        public void pauseGame()
        {
            pausedGame = mainGame;
            mainGame = null;
            pauseScreen = new PauseScreen(this);
            currentScreen = Screen.PauseScreen;
        }

        public void unpauseGame()
        {
            mainGame = pausedGame;
            currentScreen = Screen.MainGame;
            pauseScreen = null;
        }

        /// <summary>
        /// Returns to main menu
        /// </summary>
        public void backToMain()
        {
            mainMenu = new MainMenu(this);
            currentScreen = Screen.MainMenu;
        }

        /// <summary>
        /// Displays instructions screen.
        /// </summary>
        public void instructions()
        {
            instruct = new Instructions(this);
            currentScreen = Screen.Instructions;
        }
    }
}
