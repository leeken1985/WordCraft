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

namespace TestGame2._0.Backend
{
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
    //TODO: Implement gamePiece class
    class GameArea 
    {
        private Calculate c = new Calculate();
        private const int column = 8; //8 columns
        private const int row = 12; //12 row
        private static int playerLetter;
        private Dictionary<char, int> pointList;
        private Dictionary<string, int> dict;
        private List<char> letterList;
        private int tempRow;
        private int letterColumn;
        private int currScore = 0;
        private string formedWord = "";
        private Dictionary<string, int> words3 = new Dictionary<string, int>();
        private Dictionary<string, int> words4 = new Dictionary<string, int>();
        private Dictionary<string, int> words5 = new Dictionary<string, int>();
        private Dictionary<string, int> words6 = new Dictionary<string, int>();
        //Dictionary<string, int> words7 = new Dictionary<string, int>();
        private List<string> possibleColumnWords = new List<string>();
        private List<string> possibleRowWords = new List<string>();
        private List<string> winList = new List<string>();
        bool leftCheck = false, rightCheck = false, leftUpCheck = false, rightUpCheck = false,
                leftDownCheck = false, rightDownCheck = false;
        private List<objToDel> coordToDestroy = new List<objToDel>();
        private int explosion = 27; //which explosion sprite to use
        private float timeElapsed;
        protected int FrameIndex = 0;
        private float timeToUpdate = 0.20f;
        private bool toDestroy = false;
        private bool toFind = false;

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


        public GameArea()
        {
            pointList = new Dictionary<char, int>();
            dict = new Dictionary<string, int>();
            CreatePointList();
            CreateDictionary();
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < GameBoard.GetLength(1); y++)
                {
                    SetGameBoard(x, y, c.generateLetter());
                }
            }
            Timer timer = new Timer(3000);
            timer.Elapsed += new ElapsedEventHandler(FallDown);
            timer.Start();
        }

        public void CreateGameArea(SpriteBatch spriteBatch, Block b)
        {
            int sWidth = 50, sHeight = 50;
            for(int i = 0; i < GameBoard.GetLength(0); i++)
            {
                for(int j = 0; j < GameBoard.GetLength(1);j++)
                {
                    spriteBatch.Draw(b.Texture, new Rectangle(j * sWidth, i * sHeight, sWidth, sHeight), 
                        b.Rectangles[GameBoard[i, j]], Color.White);
                }
            }
        }

        public void CreateDictionary()
        {
            using (StreamReader sr = new StreamReader("allWords.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    int points = calcPoints(line);
                    dict.Add(line, points);
                }
            }
        }

        public void FallDown(object sender, ElapsedEventArgs e)
        {
            System.Buffer.BlockCopy(GameBoard, 0, GameBoard, 32, 352);
            for (int y = 0; y < GameBoard.GetLength(1); y++)
            {
                SetGameBoard(0, y, c.generateLetter());
            }
            //findRowWords();
            //findColumnWords();
        }

        public void GameOver()
        {
        }

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
        //fire thing
        public void setPiece(int userColumn)
        {
            int min = 100;
            for (int i = row - 1; i >= 0; i--)
            {
                if (GameBoard[i, userColumn] == 0)
                {
                    toFind = true;
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
            GameBoard[tempRow, userColumn] = playerLetter;
            letterColumn = userColumn;
        }

        public int getScore()
        {
            return currScore;
        }

        public string getFormedWord()
        {
            return formedWord;
        }

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

        public void setLetterList(Calculate calc)
        {
            letterList = calc.getLetterList();
        }

        public List<char> getLetterList() {
            return letterList;
        }

        /// <summary>
        /// Returns letters in a column.
        /// </summary>
        /// <param name="y"></param>
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
        /// Returns letters in a row.
        /// </summary>
        /// <param name="y"></param>
        public string getRowLetters()
        {
            string lineString = "";

            for (int j = 0; j < column; j++)
            {
                int temp = GameBoard[tempRow, j];
                lineString += letterList[temp];
            }
            return lineString;
        }

        /// <summary>
        /// Finds all existing words in a row.
        /// </summary>
        public void findRowWords()
        {
            string lineString = "";
            string sub = "";

            //if (leftCheck)   // CHECK FOR LETTERCOLUMN 0  AND CHECK FOR LETTERCOLUMN ON RIGHTSIDE FAR.  CHECK WORD LENGTH
            //{
            //    for (int j = 0; j < letterColumn; j++)
            //    {
            //        int temp = GameBoard[tempRow, j];
            //        lineString += letterList[temp];
            //    }
            //    leftCheck = false;
            //}
            //else if (rightCheck)
            //{
            //    for (int j = letterColumn + 1; j < column; j++)
            //    {
            //        int temp = GameBoard[tempRow, j];
            //        lineString += letterList[temp];
            //    }
            //    rightCheck = false;

            //}
            //else
            //{
                lineString = getRowLetters();
            //}

            switch (letterColumn)
            {
                case 0:
                    for (int j = 3; j <= 6; j++)
                    {
                        sub = lineString.Substring(0, j);
                        possibleRowWords.Add(sub);
                    }
                    break;
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

            int maxPoints = -100;
            string rowWord = "";
            foreach (string s in possibleRowWords)
            {
                if(dict.ContainsKey(s.Replace("-", ""))){
                    if(dict[s.Replace("-", "")] > maxPoints){
                        maxPoints = dict[s.Replace("-", "")];
                        rowWord = s.Replace("-", "");
                    }
                }
            }

            if (maxPoints != -100)
            {
                destroyRowWord(rowWord);
            }
    }

        /// <summary>
        /// Finds all existing words in all columns of the board.
        /// </summary>
        public void findColumnWords()
        {
            string lineString = "";
            string sub = "";

            lineString = getColumnLetters();

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
            else if (lineString.Length == 5)
            {
                sub = lineString.Substring(lineString.Length - 5, 5);
                possibleColumnWords.Add(sub);
                sub = lineString.Substring(lineString.Length - 4, 4);
                possibleColumnWords.Add(sub);
                sub = lineString.Substring(lineString.Length - 3, 3);
                possibleColumnWords.Add(sub);
            }
            else if (lineString.Length == 4)
            {
                sub = lineString.Substring(lineString.Length - 4, 4);
                possibleColumnWords.Add(sub);
                sub = lineString.Substring(lineString.Length - 3, 3);
                possibleColumnWords.Add(sub);

            }
            else if (lineString.Length == 3)
            {
                sub = lineString.Substring(lineString.Length - 3, 3);
                possibleColumnWords.Add(sub);
            }


            foreach (string s in possibleColumnWords)
            {
                if (dict.ContainsKey(s.Replace("-", "")))
                {
                    destroyColumnWord(s.Replace("-", ""));
                }
            }
            possibleColumnWords.Clear();
        }

        public void destroyColumnWord(string word)
        {
            int length = word.Length;

            string columnLetters = getColumnLetters();
            int index = -1;

            index = columnLetters.IndexOf(word);

            if (index != -1)
            {
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

        public void destroyRowWord(string word)
        {
            string rowLetters = getRowLetters();

            int index = -1;

            index = rowLetters.IndexOf(word);

            if (index != -1)
            {
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

        public void secondCheck()
        {
            string leftString = "";
            string sub = "";
            int letterIndex = letterColumn - 1;

            // left check
            for(int j = 0; j < letterColumn; j++){
                int temp = GameBoard[tempRow, j];
                leftString += letterList[temp];
            }

            switch (letterIndex)
            {
                case 3:
                    break;
            }

        }

        public void MoveUp(int x, int y, int length)
        {
            int i = 0;
            for (; x < 11; x++)
                System.Buffer.BlockCopy(GameBoard, (x + 1) * 32 + y * 4, GameBoard, x * 32 + y * 4, length * 4);
            for (i = y; i < y + length; i++)
            {
                GameBoard[11, i] = 0;
            }

            //leftCheck = true;
            //rightCheck = true;
            //leftUpCheck = true;
            //rightUpCheck = true;
            //leftDownCheck = true;
            //rightDownCheck = true;
        }
        public void ColumnMoveUp(int x, int y, int length)
        {
            for (; x < (12-length); x++)
                System.Buffer.BlockCopy(GameBoard, (x + length) * 32 + y * 4, GameBoard, x * 32 + y * 4, 4);
            for (int i = 11; i > 11 - length; i--)
                GameBoard[i, y] = 0;
        }

        public void Update(GameTime gameTime)
        {
            timeElapsed += (float)
                gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;
                //if (toFind)
                //{
                //    findColumnWords();
                //    findRowWords();
                //    toFind = false;
                //}
                if (toDestroy && explosion != 30)
                {
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
                else if (toDestroy && explosion == 30)
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
                    toDestroy = false;
                    coordToDestroy.Clear();
                    explosion++;
                }
            }
        }
    }
}
