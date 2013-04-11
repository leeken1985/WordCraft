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
using System.IO;
using System.Timers;
using TestGame2._0.GameScreens;

namespace TestGame2._0.Backend
{
    /// <summary>
    /// Object that is deleted.  
    /// Takes in x and y coords, length of word, and whether it is in row or column.
    /// </summary>
    struct objToDel
    {
        public int x, y, value, rowCol;
        public objToDel(int i, int j, int k, int l)
        {
            x = i;
            y = j;
            value = k;
            rowCol = l;
        }
    }
    class GameArea
    {
        private Game1 game;
        private Calculate c = new Calculate();
        private const int column = 8; //8 columns
        private const int row = 12; //12 row
        private static int playerLetter;
        private Dictionary<char, int> pointList;
        private static Dictionary<string, string> dict;
        private List<char> letterList;
        private int tempRow;
        private int letterColumn;
        private int currScore = 0;
        private string formedWord = "";
        private List<string> possibleColumnWords = new List<string>();
        private List<string> possibleRowWords = new List<string>();
        private List<string> winList = new List<string>();
        private List<objToDel> coordToDestroy = new List<objToDel>();
        private int explosion = 27; //which explosion sprite to use
        private int teleport = 28;
        private float timeElapsed;
        private float timeElapesedToFall;
        protected int FrameIndex = 0;
        private float timeToUpdate = 0.10f;
        private bool toDestroy = false;
        private bool toFind = false;
        private bool toFall = true;
        private bool isGameOver = false;
        // Sets default game area with all blank cells.
        private static int[,] GameBoard = {{0, 0, 0, 0, 0, 0, 0, 0},
                                           {0, 0, 0, 0, 0, 0, 0, 0},
                                           {0, 0, 0, 0, 0, 0, 0, 0},
                                           {0, 0, 0, 0, 0, 0, 0, 0},
                                           {0, 0, 0, 0, 0, 0, 0, 0},
                                           {0, 0, 0, 0, 0, 0, 0, 0},
                                           {0, 0, 0, 0, 0, 0, 0, 0},
                                           {0, 0, 0, 0, 0, 0, 0, 0},
                                           {0, 0, 0, 0, 0, 0, 0, 0},
                                           {0, 0, 0, 0, 0, 0, 0, 0},
                                           {0, 0, 0, 0, 0, 0, 0, 0},
                                           {0, 0, 0, 0, 0, 0, 0, 0}};

        /// <summary>
        /// Constructor.  Create intial game area with 2 lines of letters.
        /// </summary>
        /// <param name="game"></param>
        public GameArea(Game1 game)
        {
            this.game = game;
            pointList = new Dictionary<char, int>();
            dict = new Dictionary<string, string>();

            // Create Point list and Dictionary
            CreatePointList();
            CreateDictionary();

            // Generate 2 random lines of letters and adds them to the game area
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < GameBoard.GetLength(1); y++)
                {
                    SetGameBoard(x, y, c.generateLetter());
                }
            }
        }

        /// <summary>
        /// Creates a gameboard with empty blocks. 
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="b">Block to be used</param>
        public void CreateGameArea(SpriteBatch spriteBatch, Block b)
        {
            int sWidth = 50, sHeight = 50;
            for (int i = 0; i < GameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < GameBoard.GetLength(1); j++)
                {
                    spriteBatch.Draw(b.Texture, new Rectangle(j * sWidth + 150, i * sHeight, sWidth, sHeight),
                        b.Rectangles[GameBoard[i, j]], Color.White);
                }
            }
        }

        /// <summary>
        /// Reads in words from a text file to create a Dictionary.
        /// </summary>
        public void CreateDictionary()
        {
            // Reads in words from a file.
            using (StreamReader sr = new StreamReader("wordanddef.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    // Adds word into Dictionary as Key.  Value is the definition of the words.
                    dict.Add(line.Split('\t')[0].Trim(), line.Split('\t')[1].Trim());
                }
            }
        }

        public static Dictionary<string, string> getDictionary()
        {
            return dict;
        }

        /// <summary>
        /// Generates a random line of blocks that is added to the top of the game area.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FallDown()
        {
            MainGame.seFallDown.Play();
            for (int i = 0; i < 8; i++)
            {
                if (GameBoard[11, i] != 0)
                {
                    isGameOver = true;
                    explosion = 27;
                }
            }
            System.Buffer.BlockCopy(GameBoard, 0, GameBoard, 32, 352);
            for (int y = 0; y < GameBoard.GetLength(1); y++)
            {
                SetGameBoard(0, y, c.generateLetter());
            }
        }

        /// <summary>
        /// Calculates points of a word based on the letters it is made up of.
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public int calcPoints(string word)
        {
            word.Trim();
            char[] chars = new char[word.Length];

            int sum = 0;

            // Add values of each letter
            for (int i = 0; i < word.Length; i++)
            {
                sum += pointList[word[i]];
            }
            return sum;
        }

        /// <summary>
        /// Assigns point values to each letter.
        /// </summary>
        public void CreatePointList()
        {
            pointList.Add(' ', 0);
            pointList.Add('A', 1);
            pointList.Add('B', 3);
            pointList.Add('C', 3);
            pointList.Add('D', 2);
            pointList.Add('E', 1);
            pointList.Add('F', 4);
            pointList.Add('G', 2);
            pointList.Add('H', 4);
            pointList.Add('I', 1);
            pointList.Add('J', 8);
            pointList.Add('K', 5);
            pointList.Add('L', 1);
            pointList.Add('M', 3);
            pointList.Add('N', 1);
            pointList.Add('O', 1);
            pointList.Add('P', 3);
            pointList.Add('Q', 10);
            pointList.Add('R', 1);
            pointList.Add('S', 1);
            pointList.Add('T', 1);
            pointList.Add('U', 1);
            pointList.Add('V', 4);
            pointList.Add('W', 4);
            pointList.Add('X', 8);
            pointList.Add('Y', 4);
            pointList.Add('Z', 10);
            pointList.Add(',', 0);
            pointList.Add('.', 0);
            pointList.Add('/', 0);
        }

        /// <summary>
        /// Sets a coordinate on the game area to be a certain block.
        /// Block keeps moving up until it hits the end of the game area or another block.
        /// </summary>
        /// <param name="userColumn"></param>
        public void setPiece(int userColumn)
        {
            toFall = false;
            int min = 100;
            for (int i = row - 1; i >= 0; i--)
            {
                if (GameBoard[i, userColumn] == 0)
                {
                    if (i < min)
                    {
                        min = i;
                    }
                }
                else
                {
                    break;
                }
            }
            tempRow = min;
            try
            {
                toFind = true;
                GameBoard[tempRow, userColumn] = 29;
            }
            catch (Exception ex)
            {
                // Game ends if user fires in row that is at its max.
                //game.endGame();
                toFind = false;
                toDestroy = false;
                toFall = false;
                isGameOver = true;
                explosion = 27;
            }
            letterColumn = userColumn;
            teleport = 28;
        }

        /// <summary>
        /// Returns the current score.
        /// </summary>
        /// <returns>Current score of player</returns>
        public int getScore()
        {
            return currScore;
        }

        /// <summary>
        /// Returns the word that was formed.
        /// </summary>
        /// <returns>Word that was formed</returns>
        public string getFormedWord()
        {
            return formedWord;
        }

        /// <summary>
        /// Sets the current letter to fire to a certain block
        /// </summary>
        /// <param name="player"></param>
        public void setPlayerLetter(int player)
        {
            playerLetter = player;
        }

        /// <summary>
        /// Sets a block of the board to a specific value.
        /// </summary>
        /// <param name="x">Row value</param>
        /// <param name="y">Column value</param>
        /// <param name="value">Letter value</param>
        public static void SetGameBoard(int x, int y, int value)
        {
            GameBoard[x, y] = value;
        }

        /// <summary>
        /// Sets a calculator to use a specific letter list.
        /// </summary>
        /// <param name="calc">Calculate object</param>
        public void setLetterList(Calculate calc)
        {
            letterList = calc.getLetterList();
        }

        /// <summary>
        /// Returns the letter list
        /// </summary>
        /// <returns>Letter list</returns>
        public List<char> getLetterList()
        {
            return letterList;
        }

        /// <summary>
        /// Returns all letters in a column and combines them into a string.
        /// </summary>
        public string getColumnLetters()
        {
            string lineString = "";
            for (int i = 0; i < row; i++)
            {
                int temp = GameBoard[i, letterColumn];
                lineString += letterList[temp];
            }
            return lineString.Replace("-", "");
        }

        /// <summary>
        /// Returns letters in a row and combines them into a string.
        /// </summary>
        /// <param name="y"></param>
        public string getRowLetters()
        {
            string lineString = "";
            try
            {
                for (int j = 0; j < column; j++)
                {
                    int temp = GameBoard[tempRow, j];
                    lineString += letterList[temp];
                }
            }
            catch (Exception ex)
            {
                //game.endGame();
                isGameOver = true;
                explosion = 27;
            }
            return lineString;
        }

        /// <summary>
        /// Creates a substring for letters in a row depending on the index of the letter that was fired.
        /// Substrings can be of length 3 to 6.  They are then added to a list and compared against the dictionary.
        /// If they exist, calls method to destroy word that is worth the most points.
        /// </summary>
        public void findRowWords()
        {
            string lineString = "";
            string sub = "";

            // Store row letters into a string.
            lineString = getRowLetters();

            try
            {
                // Create substrings of row letters based on which column it was fired into.
                switch (letterColumn)
                {
                    // Column 0
                    case 0:
                        for (int j = 3; j <= 6; j++)
                        {
                            sub = lineString.Substring(0, j);
                            possibleRowWords.Add(sub);
                        }
                        break;
                    // Column 1
                    case 1:
                        for (int j = 3; j <= 6; j++)
                        {
                            sub = lineString.Substring(0, j);
                            possibleRowWords.Add(sub);
                        }
                        for (int k = 3; k <= 6; k++)
                        {
                            sub = lineString.Substring(1, k);
                            possibleRowWords.Add(sub);
                        }
                        break;
                    // Column 2
                    case 2:
                        for (int j = 3; j <= 6; j++)
                        {
                            sub = lineString.Substring(0, j);
                            possibleRowWords.Add(sub);
                        }
                        for (int k = 3; k <= 6; k++)
                        {
                            sub = lineString.Substring(1, k);
                            possibleRowWords.Add(sub);
                        }
                        for (int l = 3; l <= 6; l++)
                        {
                            sub = lineString.Substring(2, l);
                            possibleRowWords.Add(sub);
                        }
                        break;
                    // Column 3
                    case 3:
                        for (int j = 3; j <= 6; j++)
                        {
                            sub = lineString.Substring(1, j);
                            possibleRowWords.Add(sub);
                        }
                        for (int l = 3; l <= 6; l++)
                        {
                            sub = lineString.Substring(2, l);
                            possibleRowWords.Add(sub);
                        }
                        break;
                    // Column 4
                    case 4:
                        sub = lineString.Substring(0, 5);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(0, 6);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(1, 4);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(1, 5);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(1, 6);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(2, 3);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(2, 4);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(2, 5);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(2, 6);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(3, 3);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(3, 4);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(3, 5);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(4, 3);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(4, 4);
                        possibleRowWords.Add(sub);
                        break;
                    // Column 5
                    case 5:
                        sub = lineString.Substring(0, 6);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(1, 5);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(1, 6);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(2, 4);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(2, 5);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(2, 6);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(3, 3);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(3, 4);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(3, 5);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(4, 3);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(4, 4);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(5, 3);
                        break;
                    // Column 6
                    case 6:
                        sub = lineString.Substring(4, 3);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(3, 4);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(2, 5);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(1, 6);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(5, 3);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(4, 4);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(3, 5);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(2, 6);
                        possibleRowWords.Add(sub);
                        break;
                    // Column 7
                    case 7:
                        sub = lineString.Substring(5, 3);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(4, 4);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(3, 5);
                        possibleRowWords.Add(sub);
                        sub = lineString.Substring(2, 6);
                        possibleRowWords.Add(sub);
                        break;

                }
            }
            catch (Exception ex)
            {
                //game.endGame();
                isGameOver = true;
                explosion = 27;
            }

            int maxPoints = -100;
            string rowWord = "";
            foreach (string s in possibleRowWords)
            {
                // Check all substring to see if they exist in Dictionary
                if (dict.ContainsKey(s.Replace("-", "")))
                {
                    if (calcPoints(s.Replace("-", "")) > maxPoints)
                    {
                        maxPoints = calcPoints(s.Replace("-", ""));

                        // Store the word that gives the highest point value
                        rowWord = s.Replace("-", "");
                    }
                }
            }

            // If point value is not default (-100), call method to destroy word.
            if (maxPoints != -100)
            {
                destroyRowWord(rowWord);
            }
        }

        /// <summary>
        /// Finds word in a column that consists of the letter that was fired.
        /// Words can be of length 4 to 6.  Words can only be formed from the bottom of the column.
        /// Compares each possible word against Dictionary.  If it is found, calls method to destroy word.
        /// </summary>
        public void findColumnWords()
        {
            string lineString = "";
            string sub = "";

            // Store letters in the column
            lineString = getColumnLetters();

            // Substring of letters based on possible words of length 6.
            if (lineString.Length >= 6)
            {
                sub = lineString.Substring(lineString.Length - 6, 6);
                possibleColumnWords.Add(sub);
                sub = lineString.Substring(lineString.Length - 5, 5);
                possibleColumnWords.Add(sub);
                sub = lineString.Substring(lineString.Length - 4, 4);
                possibleColumnWords.Add(sub);
                sub = lineString.Substring(lineString.Length - 3, 3);
                possibleColumnWords.Add(sub);
            }
            // Length 5
            else if (lineString.Length == 5)
            {
                sub = lineString.Substring(lineString.Length - 5, 5);
                possibleColumnWords.Add(sub);
                sub = lineString.Substring(lineString.Length - 4, 4);
                possibleColumnWords.Add(sub);
                sub = lineString.Substring(lineString.Length - 3, 3);
                possibleColumnWords.Add(sub);
            }
            // Length 5
            else if (lineString.Length == 4)
            {
                sub = lineString.Substring(lineString.Length - 4, 4);
                possibleColumnWords.Add(sub);
                sub = lineString.Substring(lineString.Length - 3, 3);
                possibleColumnWords.Add(sub);

            }
            // Length 3
            else if (lineString.Length == 3)
            {
                sub = lineString.Substring(lineString.Length - 3, 3);
                possibleColumnWords.Add(sub);
            }

            // Checks dictionary to see if word exists.
            foreach (string s in possibleColumnWords)
            {
                if (dict.ContainsKey(s.Replace("-", "")))
                {
                    destroyColumnWord(s.Replace("-", ""));
                }
            }

            possibleColumnWords.Clear();
        }

        /// <summary>
        /// Checks to see if word still exists in column.  Destroys word in a column if yes.
        /// </summary>
        /// <param name="word">Word to destroy</param>
        public void destroyColumnWord(string word)
        {
            int length = word.Length;

            // Gets column letters.
            string columnLetters = getColumnLetters();
            int index = -1;

            // Finds index of word to destroy in the column letters
            index = columnLetters.IndexOf(word);

            if (index != -1)
            {
                // Increase score and save word that was formed.  Sets explosion sprite.
                MainGame.seExplode.Play();
                toDestroy = true;
                currScore += calcPoints(word);
                formedWord = word;
                //for (int i = index; i < index + word.Length; i++)
                //{
                //    GameBoard[i, letterColumn] = 0;
                //}
                coordToDestroy.Add(new objToDel(index, letterColumn, word.Length, 1));
                explosion = 27;
                //ColumnMoveUp(index, letterColumn, word.Length);
            }
        }

        /// <summary>
        /// Checks to see if word still exists in a row.  Destroys it if it does.
        /// </summary>
        /// <param name="word">Word to destroy</param>
        public void destroyRowWord(string word)
        {
            string rowLetters = getRowLetters();
            int index = -1;
            index = rowLetters.IndexOf(word);

            if (index != -1)
            {
                MainGame.seExplode.Play();
                toDestroy = true;
                currScore += calcPoints(word);
                formedWord = word;
                //for (int j = index; j < index + word.Length; j++)
                //{
                //    GameBoard[tempRow, j] = 0;
                //}
                coordToDestroy.Add(new objToDel(tempRow, index, word.Length, 0));
                explosion = 27;
                //MoveUp(tempRow, index, word.Length);
            }
            possibleRowWords.Clear();
        }

        /// <summary>
        /// Pushes all horizontal blocks as far up as they can go.  
        /// This is called after a word is destroyed.
        /// </summary>
        /// <param name="x">X-coord of block</param>
        /// <param name="y">Y-coord of block</param>
        /// <param name="length">Length of word that was destroyed</param>
        public void MoveUp(int x, int y, int length)
        {
            int i = 0;
            for (; x < 11; x++)
                System.Buffer.BlockCopy(GameBoard, (x + 1) * 32 + y * 4, GameBoard, x * 32 + y * 4, length * 4);
            for (i = y; i < y + length; i++)
            {
                GameBoard[11, i] = 0;
            }
        }

        /// <summary>
        /// Pushes all blocks in a column as far up as they can go.
        /// This is called after a word is destroyed.
        /// </summary>
        /// <param name="x">X-coord of block</param>
        /// <param name="y">Y-coord of block</param>
        /// <param name="length">Length of word that was destroyed</param>
        public void ColumnMoveUp(int x, int y, int length)
        {
            for (; x < (12 - length); x++)
                System.Buffer.BlockCopy(GameBoard, (x + length) * 32 + y * 4, GameBoard, x * 32 + y * 4, 4);
            for (int i = 11; i > 11 - length; i--)
                GameBoard[i, y] = 0;
        }

        /// <summary>
        /// Sets the block sprite that was shot as teleport sprites
        /// </summary>
        private void teleportAnimation()
        {
            if (toFind && teleport != 26)
            {
                toFall = false;
                GameBoard[tempRow, letterColumn] = teleport;
                teleport--;
            }
            else if (toFind && teleport == 26)
            {
                GameBoard[tempRow, letterColumn] = playerLetter;
                toFind = false;
                toFall = true;
                findRowWords();
                findColumnWords();
            }
        }

        /// <summary>
        /// Sets the all the block sprites that formed a word as
        /// the explosion sprites if a valid word is formed
        /// </summary>
        private void destroyAnimation()
        {
            if (toDestroy && explosion != 30)
            {
                toFall = false;
                int i = 0;
                while (i != coordToDestroy.Count())
                {
                    if (coordToDestroy[i].rowCol == 0)//set row to be destroyed as explosion sprites
                    {
                        for (int j = coordToDestroy[i].y; j < coordToDestroy[i].y + coordToDestroy[i].value; j++)
                        {
                            GameBoard[coordToDestroy[i].x, j] = explosion;
                        }
                    }
                    else //set column to be destroyed as explosion sprites
                    {
                        for (int j = coordToDestroy[i].x; j < coordToDestroy[i].x + coordToDestroy[i].value; j++)
                        {
                            GameBoard[j, coordToDestroy[i].y] = explosion;
                        }
                    }
                    i++;
                }
                explosion++;
            }
        }

        /// <summary>
        /// Sets the block sprites that formed a word and turned into
        /// explosion sprites as an empty sprites if a valid word is formed
        /// </summary>
        private void destrowClear()
        {
            if (toDestroy && explosion == 30)
            {
                int i = 0;
                while (i != coordToDestroy.Count())
                {
                    if (coordToDestroy[i].rowCol == 0)//delete row
                    {
                        for (int j = coordToDestroy[i].y; j < coordToDestroy[i].y + coordToDestroy[i].value; j++)
                        {
                            GameBoard[coordToDestroy[i].x, j] = 0;
                        }
                        MoveUp(coordToDestroy[i].x, coordToDestroy[i].y, coordToDestroy[i].value);
                    }
                    else //delete column
                    {
                        for (int j = coordToDestroy[i].x; j < coordToDestroy[i].x + coordToDestroy[i].value; j++)
                        {
                            GameBoard[j, coordToDestroy[i].y] = 0;
                        }
                        ColumnMoveUp(coordToDestroy[i].x, coordToDestroy[i].y, coordToDestroy[i].value);
                    }
                    i++;
                }
                coordToDestroy.Clear();
                explosion++;
                toDestroy = false;
                toFall = true;
            }
        }

        /// <summary>
        /// Sets all the block sprites currently on the screen as the
        /// explosion sprites if game is set to over
        /// </summary>
        private void gameOverAnimation()
        {
            if (isGameOver && explosion != 30 && explosion != 31)
            {
                MainGame.seExplode.Play();
                toFall = false;
                toFind = false;
                toDestroy = false;
                for (int i = 0; i < GameBoard.GetLength(0); i++)
                {
                    for (int j = 0; j < GameBoard.GetLength(1); j++)
                    {
                        if (GameBoard[i, j] != 0)
                        {
                            GameBoard[i, j] = explosion;
                        }
                    }
                }
                explosion++;
                timeToUpdate = 0.30f;
            }
        }

        /// <summary>
        /// Sets all the block sprites currently on the screen and turned
        /// explosion sprites as empty sprites then calls the game over screen
        /// if game is set to over
        /// </summary>
        private void gameOverClear()
        {
            if (isGameOver && explosion == 30)
            {
                for (int i = 0; i < GameBoard.GetLength(0); i++)
                {
                    for (int j = 0; j < GameBoard.GetLength(1); j++)
                    {
                        GameBoard[i, j] = 0;
                    }
                }
                explosion++;
            }
            else if (isGameOver && explosion == 31)
            {
                System.Threading.Thread.Sleep(1000);
                game.endGame();
            }
        }

        /// <summary>
        /// Controls display of sprites when blocks are set and destroyed.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            timeElapsed += (float)
                gameTime.ElapsedGameTime.TotalSeconds;
            timeElapesedToFall += (float)
                gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;
                teleportAnimation();
                destroyAnimation();
                destrowClear();
                if (toFall && timeElapesedToFall > 10)
                {
                    timeElapesedToFall = 0;
                    FallDown();
                }
                gameOverAnimation();
                gameOverClear();
            }
        }
    }
}
