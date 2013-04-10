﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;

namespace TestGame2._0.GameScreens
{
    public class PauseScreen
    {
        private Game1 game;
        private KeyboardState lastState;
        private SpriteFont myFont;
        private Texture2D texture;
        private Rectangle rect1;
        public static ArrayList list = new ArrayList{ "", "", "", "", "" };

        public PauseScreen(Game1 game)
        {
            this.game = game;
            lastState = Keyboard.GetState();
            rect1 = new Rectangle(0, 0, 751, 751);
            texture = game.Content.Load<Texture2D>("Space_background");
            myFont = game.Content.Load<SpriteFont>("scoreBoardFont");
        }

        public void pause(GameTime gameTime)
        {
            if (true)//Use somehting to hold update. 
                Update(gameTime);
        }
        public void Update(GameTime gameTime)
        {
            KeyboardState currentState = Keyboard.GetState();

            if (currentState.IsKeyDown(Keys.Escape) && lastState.IsKeyUp(Keys.Escape))
            {
                game.unpauseGame();
            }
            lastState = currentState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect1, Color.White);
            spriteBatch.DrawString(myFont, "PAUSED", new Vector2(325, 300), Color.White);
            for(int i = 0; i < list.Count; i++)
                spriteBatch.DrawString(myFont, (String)list[i], new Vector2(325, 325 + i * 10), Color.White);
        }
    }
}
