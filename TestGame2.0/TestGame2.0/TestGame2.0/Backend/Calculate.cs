﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame2._0.Backend
{

    class Calculate
    {
        private List<char> randList;
        private List<char> letterList;
        private Random random = new Random();
        private int result1;
        private int result2;
        private int result3;
        private int result4;
        private int result5;
        private int result6;
        private int result7;

        public Calculate()
        {
            randList = new List<char>();
            letterList = new List<char>();

            letterList.Add('-');
            letterList.Add('A');
            letterList.Add('B');
            letterList.Add('C');
            letterList.Add('D');
            letterList.Add('E');
            letterList.Add('F');
            letterList.Add('G');
            letterList.Add('H');
            letterList.Add('I');
            letterList.Add('J');
            letterList.Add('K');
            letterList.Add('L');
            letterList.Add('M');
            letterList.Add('N');
            letterList.Add('O');
            letterList.Add('P');
            letterList.Add('Q');
            letterList.Add('R');
            letterList.Add('S');
            letterList.Add('T');
            letterList.Add('U');
            letterList.Add('V');
            letterList.Add('W');
            letterList.Add('X');
            letterList.Add('Y');
            letterList.Add('Z');

            // Adds each letter a certain amount of times based on how many are in a Scrabble set.
            for (int i = 0; i < 9; i++)
            {
                randList.Add('E');
                randList.Add('A');
            }
            for (int i = 0; i < 8; i++)
            {
                randList.Add('S');
            }
            for (int i = 0; i < 7; i++)
            {
                randList.Add('O');
            }
            for (int i = 0; i < 6; i++)
            {
                randList.Add('I');
            }
            for (int i = 0; i < 5; i++)
            {
                randList.Add('R');
                randList.Add('L');
                randList.Add('T');
            }
            for (int i = 0; i < 4; i++)
            {
                randList.Add('N');
                randList.Add('U');
                randList.Add('D');
                randList.Add('P');
            }
            for (int i = 0; i < 3; i++)
            {
                randList.Add('M');
                randList.Add('H');
                randList.Add('C');
                randList.Add('K');
                randList.Add('B');
                randList.Add('G');
                randList.Add('Y');
            }
            for (int i = 0; i < 2; i++)
            {
                randList.Add('W');
                randList.Add('F');
            }
            randList.Add('V');
            randList.Add('J');
            randList.Add('Z');
            randList.Add('X');
            randList.Add('Q');
        }
        public List<char> getLetterList()
        {
            return letterList;
        }
        public int generateLetter()
        {
            // Generate a random number
            result1 = random.Next(randList.Count);
            char letter = randList[result1];
            int index = letterList.IndexOf(letter);
            // Use random number as index and retrieve letter from randList.
            return index;
        }

        public List<char> generateQueue()
        {
            List<char> queue = new List<char>();
            for (int i = 0; i < 4; i++)
            {
                char letter = letterList[generateLetter()];
                queue.Add(letter);
            }
            return queue;
        }

        public void addtoQueue(List<char> queue)
        {
            char letter = letterList[generateLetter()];
            queue.Add(letter);
        }

        public void getLetter(List<char> queue)
        {
            Random rand = new Random();
            int num = rand.Next(4);
            queue.RemoveAt(num);
        }
    }
}
