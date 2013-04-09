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
using TestGame2._0.Backend;

namespace TestGame2._0.GameScreens
{
    class MainGame
    {
        private Game1 game;
        Cannon cannonSprite;
        Texture2D background;
        Texture2D infiniteBackground;
        Texture2D gridLine;
        Rectangle mainFrame;
        Rectangle rect1, rect2;
        Block block;
        GameArea mainArea;
        SpriteFont myFont;
        ScoreBoard scoreBoard;
        public static SoundEffect seExplode;
        public static SoundEffect seTravel;
        public static SoundEffect seFire;

        public MainGame(Game1 game)
        {
            this.game = game;
            mainArea = new GameArea(this.game);
            cannonSprite = new Cannon();
            cannonSprite.LoadContent(game.Content);
            block = new Block(game.Content.Load<Texture2D>("spriteSheet"), 6);
            cannonSprite.setBlock(block);
            cannonSprite.setGameArea(mainArea);
            background = game.Content.Load<Texture2D>("space");
            gridLine = new Texture2D(game.GraphicsDevice, 1, 1);
            gridLine.SetData(new Color[] { Color.White });
            mainFrame = new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            myFont = game.Content.Load<SpriteFont>("myFont");
            scoreBoard = new ScoreBoard(this.game, this.mainArea, this.cannonSprite);
            seExplode = game.Content.Load<SoundEffect>("explode");
            seTravel = game.Content.Load<SoundEffect>("travel");
            seFire = game.Content.Load<SoundEffect>("fire");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(game.Content.Load<Song>("theme1"));
            infiniteBackground = game.Content.Load<Texture2D>("Space_background");
            rect1 = new Rectangle(0, 0, 1312, 887);
            rect2 = new Rectangle(0, 887, 1312, 887);
        }

        public void Update(GameTime gameTime)
        {
            mainArea.Update(gameTime);
            cannonSprite.Update(gameTime);
            scoreBoard.Update(gameTime); // update scoreboard

            if (rect1.Y + infiniteBackground.Height <= 0)
            {
                rect1.Y = rect2.Y + infiniteBackground.Height;
            }
            else if (rect2.Y + infiniteBackground.Height <= 0)
            {
                rect2.Y = rect1.Y + infiniteBackground.Height;
            }
            rect1.Y -= 2;
            rect2.Y -= 2;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(infiniteBackground, rect1, Color.White);
            spriteBatch.Draw(infiniteBackground, rect2, Color.White);
            cannonSprite.Draw(spriteBatch);
            mainArea.CreateGameArea(spriteBatch, block);
            scoreBoard.Draw(spriteBatch); // draw scoreboard

            //draw a red grid 50 x 50
            for (float x = -4; x < 5; x++)
            {
                Rectangle rectangle = new Rectangle((int)(350 + x * 50), 0, 1, 750);
                spriteBatch.Draw(gridLine, rectangle, Color.Red);
            }
            for (float y = -7; y < 9; y++)
            {
                Rectangle rectangle = new Rectangle(150, (int)(350 + y * 50), 400, 1);
                spriteBatch.Draw(gridLine, rectangle, Color.Red);
            }

            //scroll the background
           

            spriteBatch.Draw(gridLine, new Rectangle(150, 600, 400, 1), Color.White);
        }
    }
}
