using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Thesaurus;

namespace Thesaurus_Application
{
    /// <summary>
    /// Too simple form application for demo of thesaurus work only.
    /// </summary>
    public partial class Form1 : Form
    {
        IThesaurus thesaurus = new Thesaurus.Thesaurus();

        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            tb2_viewRes.Text = "";
            IEnumerable<string> words = thesaurus.GetWords();
            foreach (var item in words)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    tb2_viewRes.Text += item + Environment.NewLine;
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Write your word, please.");
            }
            else
            {
                tb2_viewRes.Text = "";
                IEnumerable<string> synonyms = thesaurus.GetSynonyms(textBox1.Text.Trim());

                foreach (var item in synonyms)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        tb2_viewRes.Text += item + Environment.NewLine;
                    }
                }

                if (string.IsNullOrWhiteSpace(tb2_viewRes.Text.Trim().Replace(Environment.NewLine, "")))
                {
                    tb2_viewRes.Text = $"{Environment.NewLine}= SYNONYMS ={Environment.NewLine}= NOT FOUND =";
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tb3_addNew.Text))
            {
                MessageBox.Show("Write your word, please.");
            }
            else if (string.IsNullOrWhiteSpace(tb2_viewRes.Text))
            {
                MessageBox.Show("Write synonyms for presented word, please.");
            }
            else
            {
                List<string> newWords = new List<string> { tb3_addNew.Text };
                int tb2lines = tb2_viewRes.Lines.Length;
                for (int i = 0; i < tb2lines; i++)
                {
                    newWords.Add(tb2_viewRes.Lines[i]);
                }

                thesaurus.AddSynonyms(newWords);

                tb3_addNew.Text = "";
                tb2_viewRes.Text = "";
                System.Threading.Thread.Sleep(500);

                IEnumerable<string> words = thesaurus.GetWords();
                foreach (var item in words)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        tb2_viewRes.Text += item + Environment.NewLine;
                    }
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            tb2_viewRes.Text = "";
        }
    }
}
