using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ImageListTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Image I = Properties.Resources.Image1;
            imageList1.Images.AddStrip(I);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            imageList1.Draw(e.Graphics, 10, 10, 0);
            imageList1.Draw(e.Graphics, 30, 10, 1);

            imageList1.Draw(e.Graphics, 10, 30, 2);
            imageList1.Draw(e.Graphics, 30, 30, 3);
            imageList1.Draw(e.Graphics, 50, 30, 4);
        }
    }
}