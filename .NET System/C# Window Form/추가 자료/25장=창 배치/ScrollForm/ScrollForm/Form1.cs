using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ScrollForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                e.Graphics.DrawString(i + "번째 줄이다.", Font, 
                    Brushes.Black, 5, 20 * i + AutoScrollPosition.Y);
            }
        }
    }
}