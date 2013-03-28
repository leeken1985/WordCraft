using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace CalcPointsTool
{   
    /// <summary>
    /// CalcPointsTool creates a Dictionary with all possible letters.  
    /// Each letter is a key and its value is assigned its respective value in the game of Scrabble.
    /// </summary>
    public partial class Form1 : Form
       {

        //Create Dictionary object
        Dictionary<char, int> hash = new Dictionary<char, int>();

        public Form1()
        {
            InitializeComponent();

            // Add all letters and their values to Dictionary
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

            label4.MaximumSize = new Size(200, 500);
            label4.Text = "1 point: E, A, I, O, N, R, T, L, S, U" + 
                            "\n2 points: D, G"+
                            "\n3 points: B, C, M, P" + 
                            "\n4 points: F, H, V, W, Y" + 
                            "\n5 points: K" + 
                            "\n8 points: J, X" + 
                            "\n10 points: Q, Z";
        }

        /// <summary>
        /// Takes a word and breaks it down into individual letters.  
        /// Adds the corresponding values of each letter from the dictionary to produce a sum.
        /// </summary>
        private void calcPoints()
        {   
            string word = textBox1.Text.ToLower();

            char[] chars = new char[word.Length];

            int sum = 0;

            // Add values of each letter
            for (int i = 0; i < word.Length; i++)
            {
                sum += hash[word[i]];
            }

            // Display value of sum in textbox
            textBox2.Text = sum.ToString();
        }

        /// <summary>
        /// Calls CalcPointsMethod.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            calcPoints();
        }
    }
}
