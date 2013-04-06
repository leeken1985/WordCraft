using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TestGame2._0.Backend;
using TestGame2._0.GameScreens;


namespace TestGame2._0
{
    class Cannon : Sprite
    {
        const int START_X_POSITION = 150;
        const int START_Y_POSITION = 750 - 100;//window height - canon height
        const int MOVE_LEFT = -50;
        const int MOVE_RIGHT = 50;
        const int SPEED = 160;
        Block block;
        Vector2 speed = Vector2.Zero;
        Vector2 direction = Vector2.Zero;
        Calculate calc;
        KeyboardState oldState;
        ContentManager mContentManager;
        int columnPosition = 1;
        int playerLetter;
        private int storageLetter;
        private static bool storeOption = false;
        private static bool isShoot = false;
        GameArea gameArea;
        List<int> queue;
        private int totalTimer = 601;
        private int timeBetweenShots = 800;
        //Random random;
        public void LoadContent(ContentManager theContentManager)
        {
            mContentManager = theContentManager;
            // Create a new SpriteBatch, which can be used to draw textures
            Position = new Vector2(START_X_POSITION, START_Y_POSITION);

            base.LoadContent(theContentManager, "spaceship");
            calc = new Calculate();
            queue = new List<int>();
            for (int i = 0; i < 3; i++)
            {
                queue.Add(calc.generateLetter());
            }
        }
        
        public void swap()
        {
            int temp;
            if (isShoot == true)
            {
                if (storeOption == false)
                {
                    storageLetter = queue[0];
                    queue.RemoveAt(0);
                    queue.Add(calc.generateLetter());
                    storeOption = true;
                    block.SetFrame(queue[0]);
                } else {
                    temp = queue[0];
                    queue.RemoveAt(0);
                    queue.Insert(0, storageLetter);
                    storageLetter = temp;
                    block.SetFrame(queue[0]);
                }
                isShoot = false;
            }
        }

        public void setBlock(Block b)
        {
            block = b;
            b.SetFrame(queue[0]);
            block.setPosition(Position + new Vector2(0, 0) + new Vector2(1, -Size.Height + 50));
        }

        public void setGameArea(GameArea ga)
        {
            gameArea = ga;
            gameArea.setLetterList(calc);
        }

        public List<int> getQueue()
        {
            return queue;
        }

        public int getStorage() {
            return storageLetter;;
        }

        private void UpdateMovement(KeyboardState newState)
        {
            speed = Vector2.Zero;
            direction = Vector2.Zero;

            if (Position.X != 150)//cannon is not hitting the left window boundary
            {
                //Move Left one key press at a time
                if (newState.IsKeyDown(Keys.Left) && !oldState.IsKeyDown(Keys.Left))
                {
                    MainGame.seTravel.Play();
                    direction.X = MOVE_LEFT;
                    block.setPosition(Position + new Vector2(-50, 0) + new Vector2(1, -Size.Height + 50));
                    columnPosition--;
                }
            }
            if (Position.X != 500)//cannon is not hitting the right window boundary
            {
                //Move right one key press at a time
                if (newState.IsKeyDown(Keys.Right) && !oldState.IsKeyDown(Keys.Right))
                {
                    MainGame.seTravel.Play();
                    direction.X = MOVE_RIGHT;
                    block.setPosition(Position + new Vector2(50, 0) + new Vector2(1, -Size.Height + 50));
                    columnPosition++;
                }
            }
        }

        private void UpdateBlock(GameTime theGameTime, KeyboardState newState)
        {
            block.Update(theGameTime);
            totalTimer += theGameTime.ElapsedGameTime.Milliseconds;
            if (newState.IsKeyDown(Keys.Space))
            {
                if (!oldState.IsKeyDown(Keys.Space))
                {
                    if (totalTimer > timeBetweenShots)
                    {
                        totalTimer = 0;
                        MainGame.seFire.Play();
                        ShootBlock();
                    }
                }
            }

            if (newState.IsKeyDown(Keys.LeftControl))
            {
                if (!oldState.IsKeyDown(Keys.LeftControl))
                {
                    if (totalTimer > timeBetweenShots)
                    {
                        swap();
                    }
                }
            }
        }

        private void ShootBlock()
        {
            block.SetFrame(queue[0]);//chooses random asteroid (a-z)
            gameArea.setPlayerLetter(queue[0]);
            gameArea.setPiece(columnPosition - 1);
            //gameArea.findRowWords();
            //gameArea.findColumnWords();
            queue.RemoveRange(0, 1);
            queue.Add(calc.generateLetter());
            block.SetFrame(queue[0]);
            
            if (isShoot == false)
            {
                 isShoot = true;
            }
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
