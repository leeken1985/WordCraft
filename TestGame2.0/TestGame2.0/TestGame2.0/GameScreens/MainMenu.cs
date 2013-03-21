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

namespace TestGame2._0.GameScreens
{
    class MainMenu
    {
        private Texture2D texture;
        private Game1 game;
        private KeyboardState lastState;
        private SpriteFont myFont;

        public MainMenu(Game1 game)
        {
            this.game = game;
            lastState = Keyboard.GetState();
            texture = game.Content.Load<Texture2D>("Awesome_Face");
            myFont = game.Content.Load<SpriteFont>("myFont");
        }

        public void Update()
        {
            KeyboardState currentState = Keyboard.GetState();
            if (currentState.IsKeyDown(Keys.Enter) && lastState.IsKeyUp(Keys.Enter))
            {
                game.StartGame();
            }
            lastState = currentState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
            {
                spriteBatch.Draw(texture, new Vector2(35, 0f), Color.White);
                spriteBatch.DrawString(myFont, "Press Enter to Start!", new Vector2(100, 600), Color.White);
            }
        }
    }
}
