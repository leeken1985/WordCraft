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
        const int MOVE_LEFT = -5;
        const int MOVE_RIGHT = 5;
        const int SPEED = 160;
        KeyboardState lastKeyboardState;

        Vector2 mStartingPosition = Vector2.Zero;

        List<Bullet> mFireballs = new List<Bullet>();

        ContentManager mContentManager;
        Vector2 speed = Vector2.Zero;
        Vector2 direction = Vector2.Zero;

        public void LoadContent(ContentManager theContentManager)
        {
            mContentManager = theContentManager;
            // Create a new SpriteBatch, which can be used to draw textures
            Position = new Vector2(400, 380);

            // TODO: use this.Content to load your game content here

            base.LoadContent(theContentManager, "mariosprite");
        }

        private void UpdateMovement(KeyboardState aCurrentKeyboardState)
        {
            speed = Vector2.Zero;
            direction = Vector2.Zero;

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
            UpdateFireball(gameTime, aCurrentKeyboardState);
            base.Update(gameTime, speed, direction);
            
            lastKeyboardState = aCurrentKeyboardState;
        }

        private void UpdateFireball(GameTime theGameTime, KeyboardState aCurrentKeyboardState)
        {
            foreach (Bullet aFireball in mFireballs)
            {
                aFireball.Update(theGameTime);
            }

            if (aCurrentKeyboardState.IsKeyDown(Keys.Space) == true){
                ShootFireball();
            }
        }

        private void ShootFireball()
        {

                bool aCreateNew = true;
                foreach (Bullet aFireball in mFireballs)
                {
                    if (aFireball.Visible == false)
                    {
                        aCreateNew = false;
                        aFireball.Fire(Position + new Vector2(Size.Width / 2, Size.Height / 2),
                            new Vector2(200, 0), new Vector2(0, -1));
                        break;
                    }
                }

                if (aCreateNew == true)
                {
                    Bullet aFireball = new Bullet();
                    aFireball.LoadContent(mContentManager);
                    aFireball.Fire(Position + new Vector2(Size.Width / 2, Size.Height / 2),
                        new Vector2(200, 200), new Vector2(0, -1));
                    mFireballs.Add(aFireball);
                }
            
        }

        public override void Draw(SpriteBatch theSpriteBatch)
        {
            foreach (Bullet  aFireball in mFireballs)
            {
                aFireball.Draw(theSpriteBatch);
            }
            base.Draw(theSpriteBatch);
        }
        
    }
}
