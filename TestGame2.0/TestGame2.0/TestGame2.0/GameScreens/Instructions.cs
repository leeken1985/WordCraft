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
    class Instructions
    {
        private Game1 game;
        private Texture2D infiniteBackground;
        private Texture2D spaceblue;
        private Texture2D example;
        private KeyboardState lastState;
        private Rectangle rect1, rect2;
        private SpriteFont myFont;
        private Boolean next = false;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game</param>
        public Instructions(Game1 game)
        {
            this.game = game;
            infiniteBackground = game.Content.Load<Texture2D>("Space_background");
            example = game.Content.Load<Texture2D>("instructions_page2_2");
            spaceblue = game.Content.Load<Texture2D>("gameBoard");
            rect1 = new Rectangle(0, 0, 1312, 887);
            rect2 = new Rectangle(0, 887, 1312, 887);
            lastState = Keyboard.GetState();

            myFont = game.Content.Load<SpriteFont>("myFont");
        }

        /// <summary>
        /// Passed into the Game1.cs Update method.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            // Move to first instruction page when Enter is pressed
            KeyboardState currentState = Keyboard.GetState();
            if (currentState.IsKeyDown(Keys.Enter) && lastState.IsKeyUp(Keys.Enter) && !next)
            {
                next = true;
            }
            // Return to main menu when Enter is pressed
            else if (currentState.IsKeyDown(Keys.Enter) && lastState.IsKeyUp(Keys.Enter) && next)
            {
                game.backToMain();
                next = false;
            }
            lastState = currentState;

            //Scrolls background
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
        /// Passed into the Game1.cs Draw method.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Display first instruction page
            if (!next)
            {
                spriteBatch.Draw(infiniteBackground, rect1, Color.White);
                spriteBatch.Draw(infiniteBackground, rect2, Color.White);
                spriteBatch.DrawString(myFont, "ASTEROIDS ARE COMING!", new Vector2(230, 80), Color.Yellow);
                spriteBatch.DrawString(myFont, "DO NOT LET THEM DAMAGE YOUR SHIP!", new Vector2(180, 130), Color.Yellow);
                spriteBatch.DrawString(myFont, "FIRE ASTEROIDS TO FORM WORDS TO DESTROY THEM.", new Vector2(80, 200), Color.Yellow);
                spriteBatch.DrawString(myFont, "WORDS CAN BE 3 TO 6 LETTERS", new Vector2(180, 240), Color.Yellow);
                spriteBatch.DrawString(myFont, "FROM LEFT TO RIGHT AND TOP DOWN.", new Vector2(160, 280), Color.Yellow);
                spriteBatch.DrawString(myFont, "GOOD LUCK.", new Vector2(300, 330), Color.Yellow);

                spriteBatch.DrawString(myFont, "CONTROLS: ", new Vector2(50, 430), Color.LightSkyBlue);
                spriteBatch.DrawString(myFont, "MOVE LEFT   -   LEFT ARROW", new Vector2(50, 480), Color.LightSkyBlue);
                spriteBatch.DrawString(myFont, "MOVE RIGHT  -   RIGHT ARROW", new Vector2(50, 510), Color.LightSkyBlue);
                spriteBatch.DrawString(myFont, "FIRE BLOCK  -   SPACE BAR", new Vector2(50, 540), Color.LightSkyBlue);
                spriteBatch.DrawString(myFont, "HOLD BLOCK  -   LEFT CONTROL", new Vector2(50, 570), Color.LightSkyBlue);
                spriteBatch.DrawString(myFont, "PAUSE/DEFINITIONS   -   ESCAPE", new Vector2(50, 600), Color.LightSkyBlue);

                spriteBatch.DrawString(myFont, "Press Enter for Next Screen!", new Vector2(200, 680), Color.White);
            }
            else
            {
                // Display second instruction page
                spriteBatch.Draw(spaceblue, new Vector2(0, 0), Color.White);
                spriteBatch.Draw(example, new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(myFont, "Press Enter to return to Main Menu!", new Vector2(140, 250), Color.Yellow);
            }
        }


    }
}
