using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace GarbageTest
{
    //TODO: Implement gamePiece class
    class GameArea
    {
        private const int column = 8; //8 columns
        private const int row = 14; //14 rows
        private GamePiece[,] boardSquares = new GamePiece[row, column]; //board squares
        private Calculate c;
        private Calculate cPlayer;
        private char playerLetter;
        private int letterIndex;
        private int tempRow;
        HashSet<string> dict;
        Dictionary<char, int> pointList;


        public GameArea()
        {
            c = new Calculate();
            cPlayer = new Calculate();
            dict = new HashSet<string>();
            pointList = new Dictionary<char, int>();

            CreatePointList();
            CreateDictionary();
            //clear game area.
            ClearArea();
        }

        public void ClearArea()
        {
            for (int x = 0; x < row; x++)
            {
                for (int y = 0; y < column; y++)
                {
                    //initialize entire board area to nothing
                    boardSquares[x, y] = new GamePiece(' ');
                }
            }
        }

        /// <summary>
        /// Returns a gamepiece on the specified co-ordinate.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public GamePiece getPiece(int userColumn, int userRow)
        {
            return boardSquares[userColumn, userRow];
        }


        /// <summary>
        /// Sets a game piece on the specified co-ordinate.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="piece"></param>
        public void setPiece(int userColumn)
        {   
            int min = 100;
            for (int i = row - 1; i >= 0 ; i--)
            {
                if (boardSquares[i, userColumn].getValue() == ' ')
                {
                    if (i < min)
                    {
                        min = i;
                    }
                }
                else
                {
                    Console.WriteLine("ERB");
                    break;
                }
            }
            tempRow = min;
            boardSquares[tempRow, userColumn].setValue(playerLetter);
            letterIndex = userColumn;
        }


        /// <summary>
        /// Returns letters in a row.
        /// </summary>
        /// <param name="y"></param>
        public string getRowLetters(int userRow)
        {
            string lineString = "";
            for (int i = 0; i < row; i++)
            {
                GamePiece temp = boardSquares[i, userRow];
                lineString += temp.getValue();
            }
            return lineString.ToUpper();
        }

        /// <summary>
        /// Returns letters in a column.
        /// </summary>
        /// <param name="y"></param>
        public string getColumnLetters(int tempRow)
        {
            string lineString = "";
            for (int j = 0; j < column; j++)
            {
                GamePiece temp = boardSquares[this.tempRow, j];
                lineString += temp.getValue();
            }
            return lineString.ToUpper();
        }

        ///// <summary>
        ///// Returns the string in the Y direction (top down)
        ///// </summary>
        ///// <param name="userColumn"></param>
        ///// <returns></returns>
        //public string getColumn(int userColumn)
        //{
        //    string lineString = "";
        //    for (int i = 0; i < row; i++)
        //    {
        //        GamePiece temp = boardSquares[userColumn, row];
        //        lineString += temp.getValue();
        //    }
        //    return lineString;
        //}

        public void generateStartBlock()
        {
            for (int j = 0; j < column; j += 2)
            {
                boardSquares[0, j].setValue(c.generateLetter());
                boardSquares[1, j+1].setValue(c.generateLetter());
            }
        }

        public char generatePlayerLetter()
        {
            playerLetter = cPlayer.generateLetter();
            return playerLetter;
        }

        public string display()
        {
            string line = "";
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    line += "[" + boardSquares[i, j].getValue() + "]";
                }

                line += "\n";
            }
            return line;
        }

        public void CreateDictionary()
        {
            try
            {
                using (StreamReader sr = new StreamReader("words.txt"))
                {
                    string line;
                    while((line = sr.ReadLine())!= null){
                    string word;
                    word = sr.ReadLine();
                    dict.Add(word);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }            
        }

        public void CreatePointList()
        {
            pointList.Add('a', 1);
            pointList.Add('b', 3);
            pointList.Add('c', 3);
            pointList.Add('d', 2);
            pointList.Add('e', 1);
            pointList.Add('f', 4);
            pointList.Add('g', 2);
            pointList.Add('h', 4);
            pointList.Add('i', 1);
            pointList.Add('j', 8);
            pointList.Add('k', 5);
            pointList.Add('l', 1);
            pointList.Add('m', 3);
            pointList.Add('n', 1);
            pointList.Add('o', 1);
            pointList.Add('p', 3);
            pointList.Add('q', 10);
            pointList.Add('r', 1);
            pointList.Add('s', 1);
            pointList.Add('t', 1);
            pointList.Add('u', 1);
            pointList.Add('v', 4);
            pointList.Add('w', 4);
            pointList.Add('x', 8);
            pointList.Add('y', 4);
            pointList.Add('z', 10);
        }

        public int calcPoints(string word)
        {
            char[] chars = new char[word.Length];

            int sum = 0;

            // Add values of each letter
            for (int i = 0; i < word.Length; i++)
            {
                sum += pointList[word[i]];
            }
            return sum;
        }

        public string findWord(string line)
        {
            // List that holds all possible 4 letter words that use the letter that was fired.
            HashSet<string> possibleWords = new HashSet<string>();
            HashSet<string> realWords = new HashSet<string>();

            string testWord = "";

            // Add possible words to list.
            switch (letterIndex)
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
                if (dict.Contains(s))
                {
                    if (calcPoints(s) > minPoints)
                    {
                        minPoints = calcPoints(s);
                        winningWord = s;
                    }
                }
            }

            return winningWord;
        }
    }
}
