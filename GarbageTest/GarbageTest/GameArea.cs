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
        Dictionary<string, int> dict;
        Dictionary<char, int> pointList;
        private int count = 0;
        List<string> winList = new List<string>();

        public GameArea()
        {
            c = new Calculate();
            cPlayer = new Calculate();
            dict = new Dictionary<string, int>();
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
        public string getColumnLetters()
        {
            string lineString = "";
            for (int j = 0; j < column; j++)
            {
                GamePiece temp = boardSquares[tempRow, j];
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
            boardSquares[0, 2].setValue(c.generateLetter());
            boardSquares[0, 5].setValue(c.generateLetter());
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
                        int points = calcPoints(line);
                        dict.Add(line, points);
                        count++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERRRORRRRRRR: " + e.Message);
            }            
        }

        public void CreatePointList()
        {
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
            pointList.Add(' ', 0);
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

        public string findWord(string line)
        {
            // List that holds all possible 4 letter words that use the letter that was fired.
            HashSet<string> possibleWords = new HashSet<string>();

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
                index = getRowLetters(letterIndex).IndexOf(formedWord);

                if (index != -1)
                {
                    for (int i = index; i < index + 4; i++)
                    {
                        boardSquares[i, letterIndex].setValue(' ');
                    }
                    Console.WriteLine("DESTROYEDDDDDDDDDDDDDDDD");
                }

                index = getColumnLetters().IndexOf(formedWord);

                if (index != -1)
                {
                    for (int j = index; j < index + 4; j++)
                    {
                        boardSquares[tempRow, j].setValue(' ');
                    }
                    Console.WriteLine("DESTROYEDDDDDDDDDDDDDDDD");
                }
            }
        }

    }
}
