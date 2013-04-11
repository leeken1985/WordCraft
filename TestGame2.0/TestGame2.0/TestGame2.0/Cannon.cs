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

            // Generates queue of letters for player to fire.
            for (int i = 0; i < 3; i++)
            {
                queue.Add(calc.generateLetter());
            }
        }
        
        /// <summary>
        /// Swaps the next letter in queue with the letter in hold.
        /// If hold is empty, the first letter in queue is simply moved to hold.
        /// Queue is then refilled.
        /// </summary>
        public void swap()
        {
            int temp;
            if (isShoot == true)
            {   
                // If Hold is empty, move first letter to hold.  And refill Queue.
                if (storeOption == false)
                {
                    storageLetter = queue[0];
                    queue.RemoveAt(0);
                    queue.Add(calc.generateLetter());
                    storeOption = true;
                    block.SetFrame(queue[0]);
                } else {
                    // Swap Hold and Current letter
                    temp = queue[0];
                    queue.RemoveAt(0);
                    queue.Insert(0, storageLetter);
                    storageLetter = temp;
                    block.SetFrame(queue[0]);
                }
                isShoot = false;
            }
        }

        /// <summary>
        /// Sets a block
        /// </summary>
        /// <param name="b">Block object</param>
        public void setBlock(Block b)
        {
            block = b;
            b.SetFrame(queue[0]);
            block.setPosition(Position + new Vector2(0, 0) + new Vector2(1, -Size.Height + 50));
        }

        /// <summary>
        /// Sets game area
        /// </summary>
        /// <param name="ga">Game area object</param>
        public void setGameArea(GameArea ga)
        {
            gameArea = ga;
            gameArea.setLetterList(calc);
        }

        /// <summary>
        /// Returns letter queue
        /// </summary>
        /// <returns>Letter queue list</returns>
        public List<int> getQueue()
        {
            return queue;
        }

        /// <summary>
        /// Returns letter that is stored in Hold
        /// </summary>
        /// <returns>Letter in hold</returns>
        public int getStorage() {
            return storageLetter;;
        }

        /// <summary>
        /// Controls movement of sprite
        /// </summary>
        /// <param name="newState">Keyboard key that is pressed</param>
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

        /// <summary>
        /// Controls events for firing letters and switching letters into Hold.
        /// </summary>
        /// <param name="theGameTime"></param>
        /// <param name="newState">Key that is pressed</param>
        private void UpdateBlock(GameTime theGameTime, KeyboardState newState)
        {
            block.Update(theGameTime);
            totalTimer += theGameTime.ElapsedGameTime.Milliseconds;
            // If spacebar is pressed, fire a letter.
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

            // If left control is pressed, swap current letter with letter in Hold
            if (newState.IsKeyDown(Keys.LeftControl))
            {
                if (!oldState.IsKeyDown(Keys.LeftControl))
                {
                        swap();
                }
            }
        }

        /// <summary>
        /// Calls methods when letter is fired.
        /// </summary>
        private void ShootBlock()
        {
            block.SetFrame(queue[0]);//chooses random asteroid (a-z)
            gameArea.setPlayerLetter(queue[0]);
            gameArea.setPiece(columnPosition - 1);
            //gameArea.findRowWords();
            //gameArea.findColumnWords();

            // Regenerates queue
            queue.RemoveRange(0, 1);
            queue.Add(calc.generateLetter());
            block.SetFrame(queue[0]);
            
            if (isShoot == false)
            {
                 isShoot = true;
            }
        }

        /// <summary>
        /// Draws sprites
        /// </summary>
        /// <param name="theSpriteBatch"></param>
        public override void Draw(SpriteBatch theSpriteBatch)
        {
            block.Draw(theSpriteBatch);
            base.Draw(theSpriteBatch);
        }

        /// <summary>
        /// Calls updating of player sprite and blocks.
        /// </summary>
        /// <param name="gameTime"></param>
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
