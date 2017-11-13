using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Thesaurus;

namespace Thesaurus_Application
{
    /// <summary>
    /// Too simple form application. Only for demo of thesaurus work.
    /// </summary>
    public partial class Form1 : Form
    {
        IThesaurus thesaurus = new Thesaurus.Thesaurus();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            IEnumerable<string> words = thesaurus.GetWords();
            foreach (var item in words)
            {
                textBox2.Text += item + Environment.NewLine;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Write your word, please.");
            }
            else
            {
                textBox2.Text = "";
                IEnumerable<string> synonyms = thesaurus.GetSynonyms(textBox1.Text.Trim());
                foreach (var item in synonyms)
                {
                    textBox2.Text += item + Environment.NewLine;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Write your word, please.");
            }
            else if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Write your synonyms, please.");
            }
            else
            {
                List<string> newWords = new List<string> { textBox1.Text };
                int tb2lines = textBox2.Lines.Length;
                for (int i = 0; i < tb2lines; i++)
                {
                    newWords.Add(textBox2.Lines[i]);
                }
                thesaurus.AddSynonyms(newWords);
            }
        }
    }
}
