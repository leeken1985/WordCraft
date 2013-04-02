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

namespace TestGame2._0
{
    class ScoreBoard
    {
        Dictionary<char, int> hash = new Dictionary<char, int>();
        
        public ScoreBoard() {
           
            hash.Add('a', 1);
            hash.Add('b', 3);
            hash.Add('c', 3);
            hash.Add('d', 2);
            hash.Add('e', 1);
            hash.Add('f', 4);
            hash.Add('g', 2);
            hash.Add('h', 4);
            hash.Add('i', 1);
            hash.Add('j', 8);
            hash.Add('k', 5);
            hash.Add('l', 1);
            hash.Add('m', 3);
            hash.Add('n', 1);
            hash.Add('o', 1);
            hash.Add('p', 3);
            hash.Add('q', 10);
            hash.Add('r', 1);
            hash.Add('s', 1);
            hash.Add('t', 1);
            hash.Add('u', 1);
            hash.Add('v', 4);
            hash.Add('w', 4);
            hash.Add('x', 8);
            hash.Add('y', 4);
            hash.Add('z', 10); 
        }

        public string calcPoints(string word)
        {
            char[] chars = new char[word.Length];

            int sum = 0;

            // Add values of each letter
            for (int i = 0; i < word.Length; i++)
            {
                sum += hash[word[i]];
            }

            return sum.ToString();
        }

    }
}
