using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TestGame2._0.Backend;


namespace TestGame2._0
{
    class Cannon : Sprite
    {
        const int START_X_POSITION =  0;
        const int START_Y_POSITION = 700 - 100;//window height - canon height
        const int MOVE_LEFT = -50;
        const int MOVE_RIGHT = 50;
        const int SPEED = 160;
        Block block;
        Vector2 speed = Vector2.Zero;
        Vector2 direction = Vector2.Zero;
        Calculate calc;
        KeyboardState oldState ;
        ContentManager mContentManager;
        int columnPosition = 1;
        int playerLetter;
        GameArea gameArea;
        int[] queue;
        //Random random;
        public void LoadContent(ContentManager theContentManager)
        {
            mContentManager = theContentManager;
            // Create a new SpriteBatch, which can be used to draw textures
            Position = new Vector2(START_X_POSITION, START_Y_POSITION);

            base.LoadContent(theContentManager, "tShape");
            calc = new Calculate();
            queue = new int[2];
            for (int i = 0; i < 2; i++)
            {
                queue[i] = calc.generateLetter();
            }
        }

        public void setBlock(Block b)
        {
            block = b;
        }

        public void setGameArea(GameArea ga)
        {
            gameArea = ga;
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
                     direction.X = MOVE_LEFT;
                     block.setPosition(Position + new Vector2(0, 0) + new Vector2(1, -Size.Height + 50));
                     columnPosition--;
                 }
            }
            if (Position.X != 300)//cannon is not hitting the right window boundary
            {
                //Move right one key press at a time
                 if (newState.IsKeyDown(Keys.Right) && !oldState.IsKeyDown(Keys.Right))
                 {
                     direction.X = MOVE_RIGHT;
                     block.setPosition(Position + new Vector2(100, 0) + new Vector2(1, -Size.Height + 50));
                     columnPosition++;
                 }
            }
        }

        private void UpdateBlock(GameTime theGameTime, KeyboardState newState)
        {
            block.Update(theGameTime);

            if (newState.IsKeyDown(Keys.Space))
            {
                if (!oldState.IsKeyDown(Keys.Space))
                {
                    ShootBlock();
                }
            }
        }

        private void ShootBlock()
        {
            block.SetFrame(queue[0]);//chooses random asteroid (a-z)
            gameArea.setPlayerLetter(queue[0]);
            gameArea.setPiece(columnPosition);
            queue[0] = queue[1];
            queue[1] = calc.generateLetter();
            block.SetFrame(queue[0]);
        }

        public override void Draw(SpriteBatch theSpriteBatch)
        {
            block.Draw(theSpriteBatch);
            base.Draw(theSpriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();

            UpdateMovement(newState);
            UpdateBlock(gameTime, newState);

            oldState = newState;
            base.Update(direction);
        }
        
    }
}
