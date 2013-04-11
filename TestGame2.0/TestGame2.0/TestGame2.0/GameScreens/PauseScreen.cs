using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using TestGame2._0.Backend;

namespace TestGame2._0.GameScreens
{
    public class PauseScreen
    {
        private Game1 game;
        private KeyboardState lastState;
        private SpriteFont myFont, myFont1;
        private Texture2D texture;
        private Rectangle rect1;
        public static ArrayList list = new ArrayList{ "", "", "", "", "" };
        private int x = 50;
        private int y = 325;

        public PauseScreen(Game1 game)
        {
            this.game = game;
            lastState = Keyboard.GetState();
            rect1 = new Rectangle(0, 0, 751, 751);
            texture = game.Content.Load<Texture2D>("Space_background");
            myFont = game.Content.Load<SpriteFont>("scoreBoardFont");
            myFont1 = game.Content.Load<SpriteFont>("pausefont");
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
            spriteBatch.DrawString(myFont, "PAUSED", new Vector2(300, 250), Color.White);
            for (int i = 0; i < list.Count; i++)
            {
                spriteBatch.DrawString(myFont, (String)list[i], new Vector2(15, 325+i*50), Color.White);
                if (GameArea.getDictionary().ContainsKey(list[i].ToString()))
                {
                    if (GameArea.getDictionary()[list[i].ToString()].Length < 65)
                        spriteBatch.DrawString(myFont1, GameArea.getDictionary()[list[i].ToString()], new Vector2(35 + x, 330 + i * 50), Color.White);
                    else
                    {
                        spriteBatch.DrawString(myFont1, GameArea.getDictionary()[list[i].ToString()].Substring(0,65), new Vector2(35 + x, 330 + i * 50), Color.White);
                        spriteBatch.DrawString(myFont1, GameArea.getDictionary()[list[i].ToString()].Substring(66), new Vector2(35 + x, 350 + i * 50), Color.White);
                    }
                }
            }
        }
    }
}
