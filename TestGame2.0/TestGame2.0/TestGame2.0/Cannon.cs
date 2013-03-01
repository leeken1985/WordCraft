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
    class Cannon : Sprite
    {
        const int START_X_POSITION =  0;
        const int START_Y_POSITION = 700 - 100;//window height - canon height
        const int MOVE_LEFT = -50;
        const int MOVE_RIGHT = 50;
        const int SPEED = 160;

        Vector2 speed = Vector2.Zero;
        Vector2 direction = Vector2.Zero;

        KeyboardState oldState ;
        ContentManager mContentManager;

        List<Fireball> mFireballs = new List<Fireball>();

        public void LoadContent(ContentManager theContentManager)
        {
            mContentManager = theContentManager;
            // Create a new SpriteBatch, which can be used to draw textures
            Position = new Vector2(START_X_POSITION, START_Y_POSITION);

            // TODO: use this.Content to load your game content here
            foreach (Fireball aFireball in mFireballs)
            {
                aFireball.LoadContent(theContentManager);
            }
            base.LoadContent(theContentManager, "tShape");
            
        }

        private void UpdateMovement(KeyboardState newState)
        {
            speed = Vector2.Zero;
            direction = Vector2.Zero;

            if (Position.X != -50 )//cannon is not hitting the left window boundary
            {
                 //Move Left one key press at a time
                 if (newState.IsKeyDown(Keys.Left) && !oldState.IsKeyDown(Keys.Left))
                 {
                     //speed.X = SPEED;
                     direction.X = MOVE_LEFT;
                 }
            }
            if (Position.X != 300)//cannon is not hitting the right window boundary
            {
                //Move right one key press at a time
                 if (newState.IsKeyDown(Keys.Right) && !oldState.IsKeyDown(Keys.Right))
                 {
                     //speed.X = SPEED;
                     direction.X = MOVE_RIGHT;
                 }
            }
        }

        private void UpdateFireball(GameTime theGameTime, KeyboardState newState)
        {
            foreach (Fireball aFireball in mFireballs)
            {
                aFireball.Update(theGameTime);
            }

            if (newState.IsKeyDown(Keys.Space))
            {
                if (!oldState.IsKeyDown(Keys.Space))
                {
                    ShootFireball();
                }
            }
        }

        private void ShootFireball()
        {
            bool aCreateNew = true;

            //recycles the fireballs. Reuses/fires again the fireballs that
            //were fired earlier and were hidden(hit the max distance)
            foreach (Fireball aFireball in mFireballs)
            {
                if (aFireball.Visible == false)
                {
                    aCreateNew = false;
                    aFireball.Fire(Position + new Vector2(50, 0) + new Vector2(-5, -Size.Height),
                        new Vector2(0, 200), new Vector2(0, -1));
                    break;
                }
            }

            if (aCreateNew == true)
            {
                Fireball aFireball = new Fireball();
                aFireball.LoadContent(mContentManager);
                aFireball.Fire(Position + new Vector2(50, 0) + new Vector2(-5, -Size.Height),
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

            oldState = newState;

            //base.Update(gameTime, speed, direction);
            base.Update(direction);
        }
        
    }
}
