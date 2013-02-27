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
        ContentManager mContentManager;
        KeyboardState mPreviousKeyboardState;

        Vector2 mStartingPosition = Vector2.Zero;

        List<Fireball> mFireballs = new List<Fireball>();

        public void LoadContent(ContentManager theContentManager)
        {
            mContentManager = theContentManager;
            // Create a new SpriteBatch, which can be used to draw textures
            Position = new Vector2(380, 330);

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

        private void UpdateFireball(GameTime theGameTime, KeyboardState aCurrentKeyboardState)
        {
            foreach (Fireball aFireball in mFireballs)
            {
                aFireball.Update(theGameTime);
            }

            if (aCurrentKeyboardState.IsKeyDown(Keys.Space) == true && mPreviousKeyboardState.IsKeyDown(Keys.Space) == false)
            {
                ShootFireball();
            }
        }

        private void ShootFireball()
        {

                bool aCreateNew = true;
                foreach (Fireball aFireball in mFireballs)
                {
                    if (aFireball.Visible == false)
                    {
                        aCreateNew = false;
                        aFireball.Fire(Position + new Vector2(0, -Size.Height),
                            new Vector2(200, 0), new Vector2(0, -1));
                        break;
                    }
                }

                if (aCreateNew == true)
                {
                    Fireball aFireball = new Fireball();
                    aFireball.LoadContent(mContentManager);
                    aFireball.Fire(Position + new Vector2(0, -Size.Height),
                        new Vector2(200, 200), new Vector2(0, -1));
                    mFireballs.Add(aFireball);
                }
        }

        public override void Draw(SpriteBatch theSpriteBatch)
        {
            foreach (Fireball aFireball in mFireballs)
            {
                aFireball.Draw(theSpriteBatch);
            }
            base.Draw(theSpriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();
            UpdateMovement(newState);
            UpdateFireball(gameTime, newState);
            base.Update(gameTime, speed, direction);
        }
        
    }
}
