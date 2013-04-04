using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame2._0.Backend
{

    class Calculate
    {
        private List<char> vowelList;
        private List<char> randList;
        private List<char> letterList;
        private Random random = new Random();
        private int result1;

        public Calculate()
        {
            vowelList = new List<char>();
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
            letterList.Add(',');
            letterList.Add('.');
            letterList.Add('/');

            // Adds each letter a certain amount of times based on how many are in a Scrabble set.
            for (int i = 0; i < 8; i++)
            {
                randList.Add('S');
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

            vowelList.Add('A');
            vowelList.Add('E');
            vowelList.Add('I');
            vowelList.Add('O');
            vowelList.Add('U');

            //this.randomizeList();

        }


        //method to randomize the list order
        private void randomizeList()
        {
            Random rnd = new Random();
            //randList.OrderBy<char, int>((item) => rnd.Next());
        }

        public List<char> getLetterList()
        {
            return letterList;
        }
        public int generateLetter()
        {
            int index = 0;
            //choose between consonant or vowel

            int num = random.Next(2);
            // Generate a random number
            if (num == 0)
            {
                result1 = random.Next(randList.Count);
                char letter = randList[result1];
                index = letterList.IndexOf(letter);
                // Use random number as index and retrieve letter from randList.
            }
            else if (num == 1)
            {
                result1 = random.Next(vowelList.Count);
                char letter = vowelList[result1];
                index = letterList.IndexOf(letter);
            }
            return index;
        }

        public char generateVowel()
        {
            // Generate a random number
            result1 = random.Next(vowelList.Count);
            char letter = vowelList[result1];
            return letter;
        }

        public List<char> generateQueue()
        {
            List<char> queue = new List<char>();
            for (int i = 0; i < 2; i++)
            {
                char letter = letterList[generateLetter()];
                queue.Add(letter);
            }
            for (int i = 0; i < 2; i++)
            {
                char vowel = generateVowel();
                queue.Add(vowel);
            }
            return queue;
        }

        /*
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
        }*/
    }
}
