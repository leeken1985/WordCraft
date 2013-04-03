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
    //TODO: Implement gamePiece class
    class GameArea 
    {
        private Calculate c = new Calculate();
        private const int column = 8; //8 columns
        private const int row = 12; //12 row
        private static int playerLetter;
        Dictionary<char, int> pointList;
        Dictionary<string, int> dict;
        List<char> letterList;
        private int tempRow;
        private int letterColumn;
        private int currScore = 0;
        private string formedWord = "";
        Dictionary<string, int> words3 = new Dictionary<string, int>();
        Dictionary<string, int> words4 = new Dictionary<string, int>();
        Dictionary<string, int> words5 = new Dictionary<string, int>();
        List<string> winList = new List<string>();
        private static int[,] GameBoard = {{4, 0, 0, 0, 0, 0, 0, 0},
                                {1, 0, 0, 0, 0, 0, 0, 0},
                                {4, 0, 0, 0, 0, 0, 0, 0},
                                {4, 0, 0, 0, 0, 0, 0, 0},
                                {25, 0, 0, 0, 0, 0, 0, 0},
                                {23, 0, 0, 0, 0, 0, 0, 0},
                                {22, 0, 0, 0, 0, 0, 0, 0},
                                {21, 0, 0, 0, 0, 0, 0, 0},
                                {10, 0, 0, 0, 0, 0, 0, 0},
                                {11, 0, 0, 0, 0, 0, 0, 0},
                                {12, 0, 0, 0, 0, 0, 0, 0},
                                {13, 0, 0, 0, 0, 0, 0, 0}};


        public GameArea()
        {
            pointList = new Dictionary<char, int>();
            dict = new Dictionary<string, int>();
            CreatePointList();
            CreateDictionary();
            //for (int x = 0; x < 2; x++){
            //    for (int y = 0; y < GameBoard.GetLength(1); y++)
            //    {
            //        SetGameBoard(x, y, c.generateLetter());
            //    }
            //}
            //Timer timer = new Timer(20000);
            //timer.Elapsed += new ElapsedEventHandler(FallDown);
            //timer.Start();
        }

        public void CreateGameArea(SpriteBatch spriteBatch, Block b)
        {
            int sWidth = 50, sHeight = 50;
            for(int i = 0; i < GameBoard.GetLength(0); i++)
            {
                for(int j = 0; j < GameBoard.GetLength(1);j++)
                {
                    spriteBatch.Draw(b.Texture, new Rectangle(j * sWidth, i * sHeight, sWidth, sHeight), b.Rectangles[GameBoard[i, j]], Color.White);
                }
            }
        }

        public void CreateDictionary()
        {
            //try
            //{
                using (StreamReader sr = new StreamReader("allWords.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        int points = calcPoints(line);
                        dict.Add(line, points);
                    }
                }
            //}
            //catch (Exception e)
            //{
            //    //Console.WriteLine("ERRRORRRRRRR: " + e.Message);
            //}
        }

        public void FallDown(object sender, ElapsedEventArgs e)
        {
            System.Buffer.BlockCopy(GameBoard, 0, GameBoard, 32, 352);
            for (int y = 0; y < GameBoard.GetLength(1); y++)
            {
                SetGameBoard(0, y, c.generateLetter());
            }
            findRowWords();
            findColumnWords();
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
        }
        //fire thing
        public void setPiece(int userColumn)
        {
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
        public string getColumnLetters(int userColumn)
        {
            string lineString = "";
            for (int i = 0; i < row; i++)
            {
                int temp = GameBoard[i, userColumn];
                lineString += letterList[temp];
            }
            return lineString;
        }

        /// <summary>
        /// Returns letters in a row.
        /// </summary>
        /// <param name="y"></param>
        public string getRowLetters(int userRow)
        {
            string lineString = "";

            for (int j = 0; j < column; j++)
            {
                int temp = GameBoard[userRow, j];
                lineString += letterList[temp];
            }
            return lineString;
        }

        /// <summary>
        /// Finds all existing words in all rows of the board.
        /// </summary>
        public void findRowWords()
        {
            string lineString = "";
            int index = -1;

            // For each row of letters, search for word in Dictinoary within row.
            for (int i = 0; i < row; i++)
            {
                lineString = getRowLetters(i);

                foreach (KeyValuePair<string, int> entry in dict)
                {
                    index = lineString.IndexOf(entry.Key);

                    if (index != -1)
                    {
                        if (entry.Key.Length == 5)
                        {
                            words5.Add(entry.Key, i);
                        }
                        else if (entry.Key.Length == 4)
                        {
                            words4.Add(entry.Key, i);
                        }
                        else if (entry.Key.Length == 3)
                        {
                            words3.Add(entry.Key, i);
                        }
                    }
                }
                destroyRowWord(i);
            }
        }

        /// <summary>
        /// Finds all existing words in all columns of the board.
        /// </summary>
        public void findColumnWords()
        {
            string lineString = "";
            int index = -1;

            for (int j = 0; j < column; j++)
            {
                lineString = getColumnLetters(j);

                foreach (KeyValuePair<string, int> entry in dict)
                {
                    index = lineString.IndexOf(entry.Key);

                    if (index != -1)
                    {
                        if (entry.Key.Length == 5)
                        {
                            words5.Add(entry.Key, j);
                        }
                        else if (entry.Key.Length == 4)
                        {
                            words4.Add(entry.Key, j);
                        }
                        else if (entry.Key.Length == 3)
                        {
                            words3.Add(entry.Key, j);
                        }
                    }
                }
                destroyColumnWord(j);
            }
        }

        /// <summary>
        /// Destroys existing words in a row
        /// </summary>
        /// <param name="destroyRow">Row number</param>
        public void destroyRowWord(int destroyRow)
        {
            string lineString = "";
            int index = -1;

            lineString = getRowLetters(destroyRow);

            foreach (KeyValuePair<string, int> entry in words5)
            {
                index = lineString.IndexOf(entry.Key);

                if (index != -1)
                {
                    currScore += entry.Value;
                    formedWord = entry.Key;

                    for (int i = index; i < index + entry.Key.Length; i++)
                    {
                        GameBoard[destroyRow, i] = 0;
                    }
                    MoveUp(destroyRow, index, entry.Key.Length);
                }

            }
            words5.Clear();
            lineString = getRowLetters(destroyRow);

            foreach (KeyValuePair<string, int> entry in words4)
            {
                index = lineString.IndexOf(entry.Key);

                if (index != -1)
                {
                    currScore += entry.Value;
                    formedWord = entry.Key;

                    for (int i = index; i < index + entry.Key.Length; i++)
                    {
                        GameBoard[destroyRow, i] = 0;
                    }
                    MoveUp(destroyRow, index, entry.Key.Length);
                }

            }
            words4.Clear();
            lineString = getRowLetters(destroyRow);

            foreach (KeyValuePair<string, int> entry in words3)
            {
                index = lineString.IndexOf(entry.Key);

                if (index != -1)
                {
                    currScore += entry.Value;
                    formedWord = entry.Key;

                    for (int i = index; i < index + entry.Key.Length; i++)
                    {
                        GameBoard[destroyRow, i] = 0;
                    }
                    MoveUp(destroyRow, index, entry.Key.Length);
                }
                
            }
            words3.Clear();
        }

        /// <summary>
        /// Destroys existing words in a column.
        /// </summary>
        /// <param name="destroyColumn">Column number</param>
        public void destroyColumnWord(int destroyColumn)
        {
            string lineString = "";
            int index = -1;

            lineString = getColumnLetters(destroyColumn);

            foreach (KeyValuePair<string, int> entry in words5)
            {
                index = lineString.IndexOf(entry.Key);

                if (index != -1)
                {
                    currScore += entry.Value;
                    formedWord = entry.Key;

                    for (int j = index; j < index + entry.Key.Length; j++)
                    {
                        GameBoard[j, destroyColumn] = 0;
                    }
                    ColumnMoveUp(index, destroyColumn, entry.Key.Length);
                }
                
            }
            words5.Clear();
            lineString = getColumnLetters(destroyColumn);

            foreach (KeyValuePair<string, int> entry in words4)
            {
                index = lineString.IndexOf(entry.Key);

                if (index != -1)
                {
                    currScore += entry.Value;
                    formedWord = entry.Key;

                    for (int j = index; j < index + entry.Key.Length; j++)
                    {
                        GameBoard[j, destroyColumn] = 0;
                    }
                    ColumnMoveUp(index, destroyColumn, entry.Key.Length);
                }
                
            }
            words4.Clear();
            lineString = getColumnLetters(destroyColumn);

            foreach (KeyValuePair<string, int> entry in words3)
            {
                index = lineString.IndexOf(entry.Key);

                if (index != -1)
                {
                    currScore += entry.Value;
                    formedWord = entry.Key;

                    for (int j = index; j < index + entry.Key.Length; j++)
                    {
                        GameBoard[j, destroyColumn] = 0;
                    }
                    ColumnMoveUp(index, destroyColumn, entry.Key.Length);
                }
                
            }
            words3.Clear();
        }
        public void MoveUp(int x, int y, int length)
        {
            int i = 0;
            for (; x < 11; x++)
                System.Buffer.BlockCopy(GameBoard, (x + 1) * 32 + y * 4, GameBoard, x * 32 + y * 4, length * 4);
            for (i = y; i < y+length;i++)
                GameBoard[11, i] = 0;
        }
        public void ColumnMoveUp(int x, int y, int length)
        {
            for (; x < (12-length); x++)
                System.Buffer.BlockCopy(GameBoard, (x + length) * 32 + y * 4, GameBoard, x * 32 + y * 4, 4);
            for (int i = 11; i > 11 - length; i--)
                GameBoard[i, y] = 0;
        }
    }
}
