using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace TestGame2._0
{
    class Mario : Sprite
    {
        const int START_X_POSITION =  0;
        const int START_Y_POSITION = 0;
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = -1;
        const int MOVE_RIGHT = 1;
        const int SPEED = 160;

        Vector2 speed = Vector2.Zero;
        Vector2 direction = Vector2.Zero;

        public void LoadContent(ContentManager theContentManager)
        {
            // Create a new SpriteBatch, which can be used to draw textures
            Position = new Vector2(50, 50);

            // TODO: use this.Content to load your game content here

            base.LoadContent(theContentManager, "mariosprite");
        }

        private void UpdateMovement(KeyboardState aCurrentKeyboardState)
        {
             if (aCurrentKeyboardState.IsKeyDown(Keys.Left) == true)
             {
                 speed.X = SPEED;
                 direction.X = MOVE_LEFT;
             }
             else if (aCurrentKeyboardState.IsKeyDown(Keys.Right) == true)
             {
                 speed.X = SPEED;
                 direction.X = MOVE_RIGHT;
             }
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState aCurrentKeyboardState = Keyboard.GetState();
            UpdateMovement(aCurrentKeyboardState);
            base.Update(gameTime, speed, direction);
        }
        
    }
}
