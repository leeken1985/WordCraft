using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarbageTest
{
    class Calculate
    {
        private List<char> randList;
        private int result1;
        private int result2;
        private int result3;
        private int result4;
        private int result5;
        private int result6;
        private int result7;
    
        public Calculate()
        {
            /// randList for storing all available letters.
            randList = new List<char>(); 

            // Adds each letter a certain amount of times based on how many are in a Scrabble set.
            for (int i = 0; i < 12; i++)
            {
                randList.Add('e');
            }
            for (int i = 0; i < 9; i++)
            {
                randList.Add('a');
                randList.Add('i');
            }
            for (int i = 0; i < 8; i++)
            {
                randList.Add('o');
            }
            for (int i = 0; i < 6; i++)
            {
                randList.Add('n');
                randList.Add('r');
                randList.Add('t');
            }
            for (int i = 0; i < 4; i++)
            {
                randList.Add('l');
                randList.Add('s');
                randList.Add('u');
                randList.Add('d');
            }
            for (int i = 0; i < 3; i++)
            {
                randList.Add('g');
            }
            for (int i = 0; i < 2; i++)
            {
                randList.Add('b');
                randList.Add('c');
                randList.Add('m');
                randList.Add('p');
                randList.Add('f');
                randList.Add('h');
                randList.Add('v');
                randList.Add('w');
                randList.Add('y');
            }

            randList.Add('k');
            randList.Add('j');
            randList.Add('x');
            randList.Add('q');
            randList.Add('z');
        }

        public char generateLetter()
        {
            // Generate a random number
            Random random = new Random();
            result1 = random.Next(randList.Count);
 
            char letter = randList[result1];
            randList.Remove(letter);
            // Use random number as index and retrieve letter from randList.
            return letter;
        }
    }
}
