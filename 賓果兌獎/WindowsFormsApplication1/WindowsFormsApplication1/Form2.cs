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
    public partial class Form2 : Form
    {
        int[] numbers = new int[25];
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        public int[] GetNumbers()
        {
            return numbers;
        }

        public string GetName()
        {
            return textBox26.Text;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            foreach (TextBox tb in this.Controls.OfType<TextBox>())
            {
                int v;
                if (int.TryParse(tb.Text, out v))
                {
                    string loc = tb.Tag.ToString();
                    numbers[int.Parse(loc)] = v;
                 
                }
                else
                {
                    tb.Focus();
                    MessageBox.Show("Number Error");
                    return;
                }
            }
            this.DialogResult = DialogResult.OK;
        }

        private void textBox27_TextChanged(object sender, EventArgs e)
        {
            string[] ss = textBox27.Text.Split(',');
            try
            {
                textBox1.Text = ss[0];
                textBox2.Text = ss[1];
                textBox3.Text = ss[2];
                textBox4.Text = ss[3];
                textBox5.Text = ss[4];
                textBox6.Text = ss[5];
                textBox7.Text = ss[6];
                textBox8.Text = ss[7];
                textBox9.Text = ss[8];
                textBox10.Text = ss[9];
                textBox11.Text = ss[10];
                textBox12.Text = ss[11];
                textBox13.Text = ss[12];
                textBox14.Text = ss[13];
                textBox15.Text = ss[14];
                textBox16.Text = ss[15];
                textBox17.Text = ss[16];
                textBox18.Text = ss[17];
                textBox19.Text = ss[18];
                textBox20.Text = ss[19];
                textBox21.Text = ss[20];
                textBox22.Text = ss[21];
                textBox23.Text = ss[22];
                textBox24.Text = ss[23];
                textBox25.Text = ss[24];
            }
            catch
            {
            }
        }
    }
}
