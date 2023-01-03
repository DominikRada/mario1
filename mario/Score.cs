using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace mario
{
    public partial class Score : Form
    {
        int score;
        public Score(int score)
        {
            InitializeComponent();
            this.score = score;
            label2.Text += score;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter writer = new StreamWriter("..\\score\\tabulka.txt", append: true);
            writer.WriteLine(textBox1.Text + ": " + score.ToString());
            writer.Close();
            Menu f2 = new Menu();
            f2.ShowDialog();
            Close();
        }
    }
}
