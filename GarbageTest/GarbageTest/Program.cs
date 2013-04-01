using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarbageTest
{
    class Program
    {
        static void Main(string[] args)
        {
            GameArea board = new GameArea();
            int currentScore = 0;

            //board.generateStartBlock();
            Console.WriteLine(board.display());
          
            while (true)
            {
               
                Console.WriteLine("Player Letter: " + board.generatePlayerLetter());

                Console.Write("Enter column number: ");
                int column = int.Parse(Console.ReadLine());
                board.setPiece(column);

                string rowLetters = board.getRowLetters(column);
                string winRow = board.findWord(rowLetters);
                string columnLetters = board.getColumnLetters();
                string winColumn = board.findWord(columnLetters);
                
                //Console.WriteLine("COLUMN LETTERS: " + rowLetters);
                //Console.WriteLine("ROW LETTERS: " + columnLetters);
                Console.WriteLine("WIN ROW: " + winRow);
                Console.WriteLine("WIN COLUMN: " + winColumn);
               
                string finalWord = "";

                finalWord = board.calcPoints(winColumn) > board.calcPoints(winRow) ? winColumn : winRow;
                currentScore += board.calcPoints(finalWord);
                Console.WriteLine("WORD IS: " + finalWord + " HIGHEST POINT VALUE IS: " + currentScore); 
                Console.WriteLine(board.display());

                board.destroyWord();
            }   
        }
    }
}
