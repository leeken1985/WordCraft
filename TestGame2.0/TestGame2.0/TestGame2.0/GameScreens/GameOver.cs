using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TestGame2._0.GameScreens
{
    class GameOver
    {
        private Game1 game;
        private SpriteFont myFont;
        private Texture2D texture;
        private Rectangle rect1, rect2, rect3;
        private Texture2D infiniteBackground;
        private KeyboardState lastState;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game</param>
        public GameOver(Game1 game)
        {
            this.game = game;
            myFont = game.Content.Load<SpriteFont>("scoreBoardFont");
            texture = game.Content.Load<Texture2D>("Awesome_Face");
            infiniteBackground = game.Content.Load<Texture2D>("Space_background");
            rect1 = new Rectangle(0, 0, 1312, 887);
            rect2 = new Rectangle(0, 887, 1312, 887);
            rect3 = new Rectangle(600, 250, texture.Width, texture.Height);
            
        }

        /// <summary>
        /// Calls updating of Game over sprite.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            // Press Enter to restart the game.
            KeyboardState currentState = Keyboard.GetState();
            if (currentState.IsKeyDown(Keys.Enter) && lastState.IsKeyUp(Keys.Enter))
            {
                game.restartGame();
            }

            // Infinite scrolling backgrouns
            if (rect1.Y + infiniteBackground.Height <= 0)
            {
                rect1.Y = rect2.Y + infiniteBackground.Height;
            }
            else if (rect2.Y + infiniteBackground.Height <= 0)
            {
                rect2.Y = rect1.Y + infiniteBackground.Height;
            }
            if (rect3.X != 180)
            {
                rect3.X--;
            }
            rect1.Y -= 2;
            rect2.Y -= 2;
        }
        /// <summary>
        /// Display Gameover streen contencts.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //stop the song on draw
            MediaPlayer.Stop();
            spriteBatch.Draw(infiniteBackground, rect1, Color.White);
            spriteBatch.Draw(infiniteBackground, rect2, Color.White);
            spriteBatch.Draw(texture, rect3, Color.White);
            spriteBatch.DrawString(myFont, "GAME OVER", new Vector2(280, 100), Color.White);
            spriteBatch.DrawString(myFont, "Total Score", new Vector2(240, 200), Color.White);
            spriteBatch.DrawString(myFont, game.totalScore.ToString(),new Vector2(440, 200), Color.White);
            spriteBatch.DrawString(myFont, "Press Enter to restart", new Vector2(180, 640), Color.White);
        }
    }
}
