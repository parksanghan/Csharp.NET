using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace PathCurve
{
    public partial class Form1 : Form
    {
        GraphicsPath Curve = new GraphicsPath();
        private int x, y;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                x = e.X;
                y = e.Y;
                Curve.StartFigure();
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Capture && e.Button == MouseButtons.Left)
            {
                Curve.AddLine(x, y, e.X, e.Y);
                Graphics G = CreateGraphics();
                G.DrawLine(Pens.Black, x, y, e.X, e.Y);
                x = e.X;
                y = e.Y;
                G.Dispose();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawPath(Pens.Black, Curve);

        }
    }
}