using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TrackBarTest
{
    public partial class Form1 : Form
    {
        private int FontSize = 10;
        public Form1()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            FontSize = trackBar1.Value;
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString("대한민국", new Font("궁서", FontSize), 
                Brushes.Black, 10, 10);
        }
    }
}