using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FindWord
{   
    /// <summary>
    /// Finds a word that exists in a dictionary from a line of letters
    /// </summary>
    public partial class FindLetter : Form
    {
        // Create a Dictionary object to store possible words
        Dictionary<string, int> wordMap = new Dictionary<string, int>();

        public FindLetter()
        {
            InitializeComponent();
           
            // Add words to Dictionary.  These are only temporary words.
            wordMap.Add("best", 0);
            wordMap.Add("beer", 0);
            wordMap.Add("bear", 0);
            wordMap.Add("beat", 0);
            wordMap.Add("beet", 0);
        }

        private void findWord()
        {
            String line = textBox1.Text;

            // Index of letter that is being used.
		    int findex = 2;

            // List that holds all possible 4 letter words that use the letter that was fired.
		    List<string> words = new List<string>();

		    String testWord = "";
		    
            // Add possible words to list.
		    switch(findex){
		    case 0:
			    testWord = line.Substring(0, 4);
			    words.Add(testWord);
			    break;
		    case 1:
			    testWord = line.Substring(1, 4);
			    words.Add(testWord);
			    testWord = line.Substring(2, 4);
			    words.Add(testWord); 
			    break;
		    case 2:
			    testWord = line.Substring(0, 4);
			    words.Add(testWord);
			    testWord = line.Substring(1, 4);
			    words.Add(testWord);
			    testWord = line.Substring(2, 4);
			    words.Add(testWord);
			    break;
		    case 3:
			    testWord = line.Substring(0, 4);
			    words.Add(testWord);
			    testWord = line.Substring(1, 4);
			    words.Add(testWord);
			    testWord = line.Substring(2, 4);
			    words.Add(testWord);
			    break;
		    case 4:
			    testWord = line.Substring(1, 4);
			    words.Add(testWord);
			    testWord = line.Substring(2, 4);
			    words.Add(testWord);
			    break;
		    case 5: 
			    testWord = line.Substring(3, 4);
			    words.Add(testWord);
			    break;
		    } 
            
            // For each possible word in list, compare against words in Dictionary.
            // If it exists, display it on Label.
          foreach(string s in words){
              if (wordMap.ContainsKey(s))
              {
                  label2.Text = s;
              }
		    }
        }

        // Call findWord method.
        private void button1_Click(object sender, EventArgs e)
        {
            findWord();
        }
    }
}
