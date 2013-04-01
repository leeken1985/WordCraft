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
    class MainGame
    {
        private Game1 game;
        Cannon cannonSprite;
        //Texture2D background;
        Texture2D gridLine;
        Rectangle mainFrame;
        Block block;

        public MainGame(Game1 game)
        {
            this.game = game;

            cannonSprite = new Cannon();
            cannonSprite.LoadContent(game.Content);
            block = new Block(game.Content.Load<Texture2D>("spirteSheet"), 6);
            cannonSprite.setBlock(block);
            //background = game.Content.Load<Texture2D>("background");
            gridLine = new Texture2D(game.GraphicsDevice, 1, 1);
            gridLine.SetData(new Color[] { Color.White });
            mainFrame = new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
        }

        public void Update(GameTime gameTime)
        {
            cannonSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            cannonSprite.Draw(spriteBatch);
            //draw a red grid 50 x 50
            for (float x = -4; x < 5; x++)
            {
                Rectangle rectangle = new Rectangle((int)(200 + x * 50), 0, 1, 700);
                spriteBatch.Draw(gridLine, rectangle, Color.Red);
            }
            for (float y = -7; y < 8; y++)
            {
                Rectangle rectangle = new Rectangle(0, (int)(350 + y * 50), 400, 1);
                spriteBatch.Draw(gridLine, rectangle, Color.Red);
            }
            cannonSprite.Draw(spriteBatch);
        }
    }
}
