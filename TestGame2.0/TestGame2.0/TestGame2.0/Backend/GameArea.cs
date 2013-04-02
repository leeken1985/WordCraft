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
        private int currScore = 0;
        Dictionary<char, int> pointList;
        Dictionary<string, int> dict;
        List<char> letterList;
        private int tempRow;
        private int letterColumn;
        List<string> winList = new List<string>();
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
                for (int y = 0; y < GameBoard.GetLength(1); y++)
                    SetGameBoard(x, y, c.generateLetter());
            Timer timer = new Timer(20000);
            timer.Elapsed += new ElapsedEventHandler(FallDown);
            timer.Start();
        }

        public void CreateGameArea(SpriteBatch spriteBatch, Block b)
        {
            int sWidth = 50, sHeight = 50;
            for(int i = 0; i < GameBoard.GetLength(1); i++)
            {
                for(int j = 0; j < GameBoard.GetLength(0);j++)
                {
                    spriteBatch.Draw(b.Texture, new Rectangle(i * sWidth, j * sHeight, sWidth, sHeight), b.Rectangles[GameBoard[j, i]], Color.White);
                }
            }
        }

        public void CreateDictionary()
        {
            //try
            //{
                using (StreamReader sr = new StreamReader("words.txt"))
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
                SetGameBoard(0, y, c.generateLetter());
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
            string col = findWord(getColumnLetters(letterColumn));
            string row = findWord(getRowLetters());
            string finalWord = calcPoints(col) > calcPoints(row) ? col : row;
            currScore += calcPoints(finalWord);
            return currScore;
        }

        public string winningWord()
        {
            string col = findWord(getColumnLetters(letterColumn));
            string row = findWord(getRowLetters());
            string finalWord = calcPoints(col) > calcPoints(row) ? col : row;
            destroyWord();
            return finalWord;
        }

        public void setPlayerLetter(int player)
        {
            playerLetter = player;
        }
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
        public string getColumnLetters(int userRow)
        {
            string lineString = "";
            for (int i = 0; i < row; i++)
            {
                int temp = GameBoard[i, userRow];
                lineString += letterList[temp];
            }
            return lineString;
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

        public string findWord(string line)
        {
            // List that holds all possible 4 letter words that use the letter that was fired.
            HashSet<string> possibleWords = new HashSet<string>();

            string testWord = "";

            // Add possible words to list.
            switch (letterColumn)
            {
                case 0:
                    testWord = line.Substring(0, 4);
                    possibleWords.Add(testWord);
                    break;
                case 1:
                    testWord = line.Substring(0, 4);
                    possibleWords.Add(testWord);
                    testWord = line.Substring(1, 4);
                    possibleWords.Add(testWord);
                    break;
                case 2:
                    testWord = line.Substring(0, 4);
                    possibleWords.Add(testWord);
                    testWord = line.Substring(1, 4);
                    possibleWords.Add(testWord);
                    testWord = line.Substring(2, 4);
                    possibleWords.Add(testWord);
                    break;
                case 3:
                    testWord = line.Substring(0, 4);
                    possibleWords.Add(testWord);
                    testWord = line.Substring(1, 4);
                    possibleWords.Add(testWord);
                    testWord = line.Substring(2, 4);
                    possibleWords.Add(testWord);
                    testWord = line.Substring(3, 4);
                    possibleWords.Add(testWord);
                    break;
                case 4:
                    testWord = line.Substring(1, 4);
                    possibleWords.Add(testWord);
                    testWord = line.Substring(2, 4);
                    possibleWords.Add(testWord);
                    testWord = line.Substring(3, 4);
                    possibleWords.Add(testWord);
                    testWord = line.Substring(4, 4);
                    possibleWords.Add(testWord);
                    break;
                case 5:
                    testWord = line.Substring(2, 4);
                    possibleWords.Add(testWord);
                    testWord = line.Substring(3, 4);
                    possibleWords.Add(testWord);
                    testWord = line.Substring(4, 4);
                    possibleWords.Add(testWord);
                    break;
                case 6:
                    testWord = line.Substring(3, 4);
                    possibleWords.Add(testWord);
                    testWord = line.Substring(4, 4);
                    possibleWords.Add(testWord);
                    break;
                case 7:
                    testWord = line.Substring(4, 4);
                    possibleWords.Add(testWord);
                    break;
            }

            // For each possible word in list, compare against words in Dictionary.
            // If it exists, display it on Label.
            int minPoints = -100;
            string winningWord = "";

            foreach (string s in possibleWords)
            {
                if (dict.ContainsKey(s))
                {
                    if (dict[s] > minPoints)
                    {
                        minPoints = dict[s];
                        winningWord = s;
                    }
                }
            }

            if (winningWord.Length != 0)
            {
                winList.Clear();
                winList.Add(winningWord);
            }
            return winningWord;
        }

        public void destroyWord()
        {
            int index = -1;
            string formedWord = "";

            if (winList.Count != 0)
            {
                formedWord = winList[0];
                index = getColumnLetters(letterColumn).IndexOf(formedWord);

                if (index != -1)
                {
                    for (int i = index; i < index + 4; i++)
                    {
                        GameBoard[i, letterColumn] = 0;
                    }
                    //Console.WriteLine("DESTROYEDDDDDDDDDDDDDDDD");
                }

                index = getRowLetters().IndexOf(formedWord);

                if (index != -1)
                {
                    for (int j = index; j < index + 4; j++)
                    {
                        GameBoard[tempRow, j] = 0;
                    }
                    //Console.WriteLine("DESTROYEDDDDDDDDDDDDDDDD");
                }
            }
        }

    }
}
