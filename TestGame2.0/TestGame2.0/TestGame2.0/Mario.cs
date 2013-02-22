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
        const int MOVE_LEFT = -10;
        const int MOVE_RIGHT = 10;
        const int SPEED = 160;

        Vector2 speed = Vector2.Zero;
        Vector2 direction = Vector2.Zero;

        KeyboardState oldState = Keyboard.GetState();

        public void LoadContent(ContentManager theContentManager)
        {
            // Create a new SpriteBatch, which can be used to draw textures
            Position = new Vector2(50, 50);

            // TODO: use this.Content to load your game content here

            base.LoadContent(theContentManager, "mariosprite");
        }

        private void UpdateMovement(KeyboardState newState)
        {
             speed = Vector2.Zero;
             direction = Vector2.Zero;
             newState = Keyboard.GetState();
             //Move Left one key press at a time
             if (newState.IsKeyDown(Keys.Left))
             {
                 if (!oldState.IsKeyDown(Keys.Left))
                 {
                     speed.X = SPEED;
                     direction.X = MOVE_LEFT;
                 }
             }
             else if (newState.IsKeyDown(Keys.Left))
             {
             }
             //Move right one key press at a time
             else if (newState.IsKeyDown(Keys.Right))
             {
                 if (!oldState.IsKeyDown(Keys.Right))
                  {
                      speed.X = SPEED;
                      direction.X = MOVE_RIGHT;
                  }
             }
             else if (newState.IsKeyDown(Keys.Right))
             {
             }
             oldState = newState;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();
            UpdateMovement(newState);
            base.Update(gameTime, speed, direction);
        }
        
    }
}
