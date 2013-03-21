using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RandomLetter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void generateLetter()
        {
            List<char> list = new List<char>();

            for (int i = 0; i < 12; i++)
            {
                list.Add('e');
            }
            for (int i = 0; i < 9; i++)
            {
                list.Add('a');
                list.Add('i');
            }
            for (int i = 0; i < 8; i++)
            {
                list.Add('o');
            }
            for (int i = 0; i < 6; i++)
            {
                list.Add('n');
                list.Add('r');
                list.Add('t');
            }
            for (int i = 0; i < 4; i++)
            {
                list.Add('l');
                list.Add('s');
                list.Add('u');
                list.Add('d');
            }
            for (int i = 0; i < 3; i++)
            {
                list.Add('g');
            }
            for (int i = 0; i < 2; i++)
            {
                list.Add('b');
                list.Add('c');
                list.Add('m');
                list.Add('p');
                list.Add('f');
                list.Add('h');
                list.Add('v');
                list.Add('w');
                list.Add('y');
            }

            list.Add('k');
            list.Add('j');
            list.Add('x');
            list.Add('q');
            list.Add('z');

            Random random = new Random();
            int result = random.Next(list.Count);

            textBox1.Text = list[result] + "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            generateLetter();
        }

    }
}
