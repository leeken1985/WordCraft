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
    public partial class FindLetter : Form
    {
        Dictionary<string, int> wordMap = new Dictionary<string, int>();

        public FindLetter()
        {
            InitializeComponent();
           
            wordMap.Add("best", 0);
            wordMap.Add("beer", 0);
            wordMap.Add("bear", 0);
            wordMap.Add("beat", 0);
            wordMap.Add("beet", 0);
        }

        private void findWord()
        {
            String line = textBox1.Text;

            // index of letter that was fired
		    int findex = 2;

		    List<string> words = new List<string>();

		    String erb = "";
		
		    switch(findex){
		    case 0:
			    erb = line.Substring(0, 4);
			    words.Add(erb);
			    break;
		    case 1:
			    erb = line.Substring(1, 4);
			    words.Add(erb);
			    erb = line.Substring(2, 4);
			    words.Add(erb); 
			    break;
		    case 2:
			    erb = line.Substring(0, 4);
			    words.Add(erb);
			    erb = line.Substring(1, 4);
			    words.Add(erb);
			    erb = line.Substring(2, 4);
			    words.Add(erb);
			    break;
		    case 3:
			    erb = line.Substring(0, 4);
			    words.Add(erb);
			    erb = line.Substring(1, 4);
			    words.Add(erb);
			    erb = line.Substring(2, 4);
			    words.Add(erb);
			    break;
		    case 4:
			    erb = line.Substring(1, 4);
			    words.Add(erb);
			    erb = line.Substring(2, 4);
			    words.Add(erb);
			    break;
		    case 5: 
			    erb = line.Substring(3, 4);
			    words.Add(erb);
			    break;
		    } 

          foreach(string s in words){
              if (wordMap.ContainsKey(s))
              {
                  label2.Text = s;
              }
		    }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            findWord();
        }
    }
}
