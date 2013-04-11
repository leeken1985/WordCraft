using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TestGame2._0.Backend;

namespace TestGame2._0.GameScreens
{
    class MainGame
    {
        private Game1 game;
        Cannon cannonSprite;
        Texture2D infiniteBackground;
        Rectangle mainFrame;
        Rectangle rect1, rect2;
        Block block;
        GameArea mainArea;
        SpriteFont myFont;
        ScoreBoard scoreBoard;
        private KeyboardState lastState;
        public static SoundEffect seExplode;
        public static SoundEffect seTravel;
        public static SoundEffect seFire;


        /// <summary>
        /// Main game constructor. Takes a game (since some game methods will be used).
        /// </summary>
        /// <param name="game"></param>
        public MainGame(Game1 game)
        {
            this.game = game;
            mainArea = new GameArea(this.game);
            cannonSprite = new Cannon();
            cannonSprite.LoadContent(game.Content);
            block = new Block(game.Content.Load<Texture2D>("spriteSheet"), 6);
            cannonSprite.setBlock(block);
            cannonSprite.setGameArea(mainArea);
            mainFrame = new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            myFont = game.Content.Load<SpriteFont>("myFont");
            scoreBoard = new ScoreBoard(this.game, this.mainArea, this.cannonSprite);
            seExplode = game.Content.Load<SoundEffect>("explode");
            seTravel = game.Content.Load<SoundEffect>("travel");
            seFire = game.Content.Load<SoundEffect>("fire");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(game.Content.Load<Song>("theme1"));
            infiniteBackground = game.Content.Load<Texture2D>("gameBoard");
            rect1 = new Rectangle(0, 0, 1024, 1024);
            rect2 = new Rectangle(0, 1024, 1024, 1024);
        }

        /// <summary>
        /// Update uses the Game1.cs update method. 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            KeyboardState currentState = Keyboard.GetState();
            if (currentState.IsKeyDown(Keys.Escape) && lastState.IsKeyUp(Keys.Escape))
            {
                game.pauseGame();
            }
            lastState = currentState;

            mainArea.Update(gameTime);
            cannonSprite.Update(gameTime);
            scoreBoard.Update(gameTime); // update scoreboard

            if (rect1.Y + infiniteBackground.Height <= 0)
            {
                rect1.Y = rect2.Y + infiniteBackground.Height;
            }
            else if (rect2.Y + infiniteBackground.Height <= 0)
            {
                rect2.Y = rect1.Y + infiniteBackground.Height;
            }
            rect1.Y -= 1;
            rect2.Y -= 1;

        }

        /// <summary>
        /// Draw is passed into Game1.cs, which does the actual drawing.
        /// Things below are what the Game1.cs draw method will draw.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(infiniteBackground, rect1, Color.White);
            spriteBatch.Draw(infiniteBackground, rect2, Color.White);
            cannonSprite.Draw(spriteBatch);
            mainArea.CreateGameArea(spriteBatch, block);
            scoreBoard.Draw(spriteBatch); // draw scoreboard
        }
    }
}
