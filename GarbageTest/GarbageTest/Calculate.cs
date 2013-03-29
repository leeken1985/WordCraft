using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarbageTest
{
    class Calculate
    {
        private List<char> randList;
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
            //List<char> letterRack = new List<char>();
            // Generate a random number
            
            result1 = random.Next(randList.Count);
            //letterRack.Add(randList[result1]);
            //randList.Remove(randList[result1]);

            //result2 = random.Next(randList.Count);
            //letterRack.Add(randList[result2]);
            //randList.Remove(randList[result2]);

            //result3 = random.Next(randList.Count);
            //letterRack.Add(randList[result3]);
            //randList.Remove(randList[result3]);

            //result4 = random.Next(randList.Count);
            //letterRack.Add(randList[result4]);
            //randList.Remove(randList[result4]);

            //result5 = random.Next(randList.Count);
            //letterRack.Add(randList[result5]);
            //randList.Remove(randList[result5]);
            
            //result6 = random.Next(randList.Count);
            //letterRack.Add(randList[result6]);
            //randList.Remove(randList[result6]);

            //result7 = random.Next(randList.Count);
            //letterRack.Add(randList[result7]);
            //randList.Remove(randList[result7]);

            //Random randInt = new Random();
            //int num = randInt.Next(0, 8);

            //foreach(char letter in letterRack){
            //    randList.Add(letter);
            //}
            char letter = randList[result1];
            randList.Remove(letter);
            // Use random number as index and retrieve letter from randList.
            return letter;
        }
    }
}
