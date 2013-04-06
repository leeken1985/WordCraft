using System;
using System.Collections.Generic;
using System.Collections;
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

namespace TestGame2._0
{
    class ScoreBoard : Sprite
    {
        private Game1 game;
        private Dictionary<char, int> hash = new Dictionary<char, int>();
        private Cannon cannonSprite;
        private Block block0, block1, block2, block3;
        private GameArea mainArea;
        private SpriteFont scoreBoardFont;
        private ArrayList wordList = new ArrayList() {"", "", "", "", ""};
        
        /// <summary>
        /// Constructor ScoreBoard class. It takes 2 main class object and use them to update score board
        /// </summary>
        /// <param name="g"></param>
        /// <param name="ma"></param>
        /// <param name="cs"></param>
        public ScoreBoard(Game1 g, GameArea ma, Cannon cs) 
        {
            this.game = g;
            block0 = new Block(g.Content.Load<Texture2D>("spriteSheet"), 6);
            block1 = new Block(g.Content.Load<Texture2D>("spriteSheet"), 6);
            block2 = new Block(g.Content.Load<Texture2D>("spriteSheet"), 6);
            block3 = new Block(g.Content.Load<Texture2D>("spriteSheet"), 6);
            scoreBoardFont = g.Content.Load<SpriteFont>("scoreBoardFont");
            mainArea = ma;
            cannonSprite = cs;            
        }

        /// <summary>
        /// Draw method to display score, a list of 5 fomred words, and current & next 2 letters
        /// </summary>
        /// <param name="thespriteBatch"></param>
        public override void Draw(SpriteBatch thespriteBatch)
        {
            game.totalScore = mainArea.getScore();
            thespriteBatch.DrawString(scoreBoardFont, "SCORE :    " + mainArea.getScore(), new Vector2(20, 100), Color.Red);            
            thespriteBatch.DrawString(scoreBoardFont, (String)wordList[0] != "" ? "1.    " + (String)wordList[0] : "", new Vector2(20, 240), Color.White);
            thespriteBatch.DrawString(scoreBoardFont, (String)wordList[1] != "" ? "2.    " + (String)wordList[1] : "", new Vector2(20, 270), Color.White);
            thespriteBatch.DrawString(scoreBoardFont, (String)wordList[2] != "" ? "3.    " + (String)wordList[2] : "", new Vector2(20, 300), Color.White);
            thespriteBatch.DrawString(scoreBoardFont, (String)wordList[3] != "" ? "4.    " + (String)wordList[3] : "", new Vector2(20, 330), Color.White);
            thespriteBatch.DrawString(scoreBoardFont, (String)wordList[4] != "" ? "5.    " + (String)wordList[4] : "", new Vector2(20, 360), Color.White);

            thespriteBatch.DrawString(scoreBoardFont, "Current:  ", new Vector2(560, 150), Color.White);
            thespriteBatch.DrawString(scoreBoardFont, "Next:  ", new Vector2(560, 310), Color.White);
            thespriteBatch.DrawString(scoreBoardFont, "Storage:  ", new Vector2(560, 560), Color.White);
            
            block0.SetFrame(cannonSprite.getQueue()[0]);
            block1.SetFrame(cannonSprite.getQueue()[1]);
            block2.SetFrame(cannonSprite.getQueue()[2]);
            block3.SetFrame(cannonSprite.getStorage());

 
            block0.setPosition(new Vector2(640, 140));
            block1.setPosition(new Vector2(630, 300));
            block2.setPosition(new Vector2(630, 360));
            block3.setPosition(new Vector2(640, 550));

            block0.Draw(thespriteBatch);
            block1.Draw(thespriteBatch);
            block2.Draw(thespriteBatch);
            block3.Draw(thespriteBatch);
        }

        /// <summary>
        /// Upated the formed word list
        /// </summary>
        public void Update(GameTime gameTime)
        {
            String formedWord;
            formedWord = mainArea.getFormedWord();
            
            if (formedWord != (String)wordList[0]) 
            {
                wordList.RemoveAt(4);
                wordList.Insert(0, formedWord);
            }
        }
    }
}
