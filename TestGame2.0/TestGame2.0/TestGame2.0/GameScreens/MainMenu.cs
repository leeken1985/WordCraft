﻿using System;
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

namespace TestGame2._0.GameScreens
{
    class MainMenu
    {
        private Texture2D title;
        private Texture2D infiniteBackground;
        private Game1 game;
        private KeyboardState lastState;
        private SpriteFont myFont;
        private Rectangle rect1, rect2;
        private Texture2D newGame;
        private Texture2D instructions;

        public MainMenu(Game1 game)
        {
            this.game = game;
            lastState = Keyboard.GetState();
            myFont = game.Content.Load<SpriteFont>("myFont");
            title = game.Content.Load<Texture2D>("title");
            infiniteBackground = game.Content.Load<Texture2D>("Space_background");
            rect1 = new Rectangle(0, 0, 1312, 887);
            rect2 = new Rectangle(0, 887, 1312, 887);
            newGame = game.Content.Load<Texture2D>("newGameOn");
            instructions = game.Content.Load<Texture2D>("instructionsOFF");
        }

        public void Update()
        {
            KeyboardState currentState = Keyboard.GetState();
            if (currentState.IsKeyDown(Keys.Enter) && lastState.IsKeyUp(Keys.Enter))
            {
                game.StartGame();
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
            rect1.Y -= 2;
            rect2.Y -= 2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(infiniteBackground, rect1, Color.White);
            spriteBatch.Draw(infiniteBackground, rect2, Color.White);
            spriteBatch.Draw(title, new Vector2(100, 50), Color.White);
            spriteBatch.Draw(newGame, new Vector2(150, 300), Color.White);
            spriteBatch.Draw(instructions, new Vector2(150, 400), Color.White);
            //spriteBatch.DrawString(myFont, "Press Enter to Start!", new Vector2(220, 600), Color.White);
        }
    }
}
