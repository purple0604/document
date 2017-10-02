using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        List<Card> cards = new List<Card>();
        int cw = 150;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            if (f.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            cards.Add(new Card(f.GetName(), f.GetNumbers()));

            pictureBox1.Height = cards.Count * (cw + 30);
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            int t = 10,l = 10;
            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].Refresh(e.Graphics, new Point(l, t), cw);
                if (i % 2 == 0)
                {
                    l += cw + 20;
                }
                else
                {
                    t += cw + 30;
                    l = 10;
                }

            }
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string[] nums = textBox1.Text.Split('\r', '\n');
            List<int> numbers = new List<int>();

            foreach (string num in nums)
            {
                int v;
                if (int.TryParse(num, out v))
                {
                    numbers.Add(v);
                }
            }
            foreach (Card card in cards)
            {
                card.SetSelNum(numbers.ToArray());
            }
            pictureBox1.Invalidate();
        }
    }
}
