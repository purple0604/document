using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            Bitmap bmp = (Bitmap)Image.FromFile(@"C:\Users\purple-chang\Desktop\Desert.jpg");

//            g.FillRectangle(Brushes.Blue, new Rectangle(0, 0, 400, 400));
            g.DrawImage(bmp, 100, 100);
        }
    }
}
