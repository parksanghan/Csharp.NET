using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MultiContext
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Green, 0, 0, Width / 2, Bottom);
            e.Graphics.FillRectangle(Brushes.Blue, Width / 2, 0, Right, Bottom);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point pt = new Point(e.X, e.Y);
                pt = PointToScreen(pt);
                if (e.X < Width / 2)
                {
                    contextMenuStrip1.Show(pt.X, pt.Y);
                }
                else
                {
                    contextMenuStrip2.Show(pt.X, pt.Y);
                }
            }
        }
    }
}